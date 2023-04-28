using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Bulb;
using System.Linq.Expressions;
using DmitryBrant.CustomControls;
using System.Threading;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace esd_a1_pc_app
{

    public partial class Form1 : Form
    {
        public SerialPort _SerialPort = new SerialPort();
        public SerialPort _debugPort = new SerialPort();
        private int _RightSevenSegDisp = 0;
        private int _LeftSevenSegDisp = 0;

        // Comms
        private const byte START_BYTE = 0x53;
        private const byte STOP_BYTE = 0xAA;
        private const byte TXCHECK = 0x00;
        private const byte READ_PINA = 0x01;
        private const byte READ_POT1 = 0x02;
        private const byte READ_POT2 = 0x03;
        private const byte READ_TEMP = 0X04;
        private const byte READ_LIGHT = 0x05;
        private const byte SET_PORTC = 0X0A;
        private const byte SET_HEATER = 0X0B;
        private const byte SET_LIGHT = 0X0C;
        private const byte SET_MOTOR = 0X0D;

        private System.Timers.Timer _timer = new System.Timers.Timer(5000); // 5 Second Timer

        public Form1()
        {
            InitializeComponent();
            RightSevenSegment.Value = this._RightSevenSegDisp.ToString();
            LeftSevenSegment.Value = this._LeftSevenSegDisp.ToString();
            LightPercentOn.Text = ((100 - LightScrollAdj.Value).ToString() + "%");

            this.InitComboBox();
            this.InitSerialPort();
            this.InitDebugSerialPort();

            this._timer.Elapsed += OnTimerElapsed;
        }

        //
        // Timer
        //
        private void OnTimerElapsed(object sender, EventArgs e)
        {
            if (this._SerialPort.IsOpen)
            {
                this.SetPortC();
                this.ReadPinA();
                this.ReadPotVoltage(READ_POT1);
                this.ReadPotVoltage(READ_POT2);
                this.ReadLdrLevel();
                this.SetLampLevel();
            }
        }

        //
        // Serial Communications
        //
        private void InitSerialPort()
        {
            this._SerialPort.BaudRate = 38400;
            this._SerialPort.Parity = Parity.None;
            this._SerialPort.StopBits = StopBits.One;
            this._SerialPort.Handshake = Handshake.None;
            this._SerialPort.WriteTimeout = 500;
            this._SerialPort.ReadTimeout = 500;
        }

        private void InitDebugSerialPort() // For testing purposes
        {
            this._debugPort.PortName = "COM8";
            this._debugPort.BaudRate = 38400;
            this._debugPort.Parity = Parity.None;
            this._debugPort.StopBits = StopBits.One;
            this._debugPort.Handshake = Handshake.None;
            this._debugPort.WriteTimeout = 500;
            this._debugPort.ReadTimeout = 500;
            this._debugPort.DataReceived += this.OnDataReceived_Debug;
            this._debugPort.Open();
        }

        private int SerialSendMcu(byte instruction)
        {
            byte[] output = { START_BYTE, instruction, STOP_BYTE };
            this._SerialPort.Write(output, 0, output.Length);
            while (_SerialPort.BytesToRead < 1) ;
            return this._SerialPort.ReadByte();
        }

        private int SerialSendMcu(byte instruction, byte b1)
        {
            byte[] output = { START_BYTE, instruction, b1, STOP_BYTE };
            this._SerialPort.Write(output, 0, output.Length);
            while (_SerialPort.BytesToRead < 1) ;
            return this._SerialPort.ReadByte();
        }

        private int SerialSendMcu(byte instruction, byte b1, byte b2)
        {
            byte[] output = { START_BYTE, instruction, b1, b2, STOP_BYTE };
            this._SerialPort.Write(output, 0, output.Length);
            while (this._SerialPort.BytesToRead < 1) ;
            return this._SerialPort.ReadByte();
        }

        private void OnDataReceived_Debug(object sender, EventArgs e) // Simulates MCU Response
        {
            List<byte> output = new List<byte>();
            byte rxByte;
            while (this._debugPort.BytesToRead > 0) // Adds all the bytes in buffer to output list
            {
                rxByte = (byte)this._debugPort.ReadByte();
                output.Add(rxByte);
            }

            if (output.Count == 3)
                System.Diagnostics.Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}");
            else if (output.Count == 4)
                System.Diagnostics.Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}, Data1: {output[2]}");
            else if(output.Count == 5)
                System.Diagnostics.Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}, Data1: {output[2]}, Data2: {output[3]}");

            if (output[0] == START_BYTE)
            {
                if (output[1] > 0x05 || output[1] == TXCHECK)
                {
                    byte[] address = { output[1] }; // Return instruction
                    this._debugPort.Write(address, 0, 1);
                }
                else
                {
                    byte rubbishVal = 0x0f;
                    byte[] adch = { rubbishVal }; // Arbitrary value
                    this._debugPort.Write(adch, 0, 1);
                }
            }
        }

        //
        // Tab 1: Top Section
        //

        private void InitComboBox()
        {
            string[] portnames = SerialPort.GetPortNames();
            foreach (string port in portnames)
            {
                ComPortBox.Items.Add(port);
            }
        }

        private void ComPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._SerialPort.PortName = Convert.ToString(ComPortBox.Text);
        }
        private void BaudBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._SerialPort.BaudRate = Convert.ToInt32(BaudBox.Text);
        }

        private int SendAndValidateSerialComms(bool validate, byte instruction, params byte[] data)
        {
            try
            {
                int response = 0;
                Stopwatch sw = Stopwatch.StartNew(); // Start timer
                while (response != instruction)
                {
                    if (data.Length > 1)
                        response = this.SerialSendMcu(instruction, data[0], data[1]); // Send data, check response
                    else if (data.Length > 0)
                        response = this.SerialSendMcu(instruction, data[0]); // Send data, check response
                    else
                        response = this.SerialSendMcu(instruction); // Send instruction, check response
                    if (sw.ElapsedMilliseconds > 5000) // Timeout if time-elapsed > 5 sec
                        throw new TimeoutException();
                    if (!validate) { break; }
                }
                System.Diagnostics.Debug.WriteLine($"<SerTx> Instruct: {instruction.ToString("X")}\n<SerRx> Response: {response.ToString("X")}\n");
                return response;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please connect a serial device", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine($"[TIMEOUT]: Response from MCU not received...");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occured: {ex.Message}");
            }
            return 0;
        }

        private void SerialConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this._SerialPort.Open();
                int response = 0xff;
                response = SendAndValidateSerialComms(true, TXCHECK);

                SerialConnectBtn.Enabled = false;
                SerialDisconnectBtn.Enabled = true;
                SerialStatusLed.On = true;
                
                this._timer.Start();
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine("No response from serial port");
            }
            catch (Exception ex)
            {
                this._SerialPort.Close();
                SerialConnectBtn.Enabled = true;
                SerialStatusLed.On = false;
                SerialDisconnectBtn.Enabled = false;
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void SerialDisconnectBtn_Click(object sender, EventArgs e)
        {
            this._SerialPort.Close();
            SerialConnectBtn.Enabled = true;
            SerialStatusLed.On = false;
            SerialDisconnectBtn.Enabled = false;
            this._timer.Stop();
        }

        //
        // Tab 1: Bottom Section
        //

        private void DatabaseConnectBtn_Click(object sender, EventArgs e)
        {
            // Parse: ServerName, Username, Password, Database
        }

        private void DatabaseDisconnectBtn_Click(object sender, EventArgs e)
        {

        }

        //
        // Tab 2
        //

        private void updateBothSevenSeg(CheckBox Pcn, int bit_n, ref SevenSegment seven_segement, ref int display_value)
        {
            if (Pcn.Checked)
                display_value |= (1 << bit_n);
            else
                display_value &= ~(1 << bit_n);

            if (display_value < 10)
                seven_segement.Value = display_value.ToString();
            else
            {
                switch (display_value)
                {
                    case 10: seven_segement.Value = "A"; break;
                    case 11: seven_segement.Value = "B"; break;
                    case 12: seven_segement.Value = "C"; break;
                    case 13: seven_segement.Value = "D"; break;
                    case 14: seven_segement.Value = "E"; break;
                    case 15: seven_segement.Value = "F"; break;
                }
            }
        }

        private void Pc0_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc0, 0, ref RightSevenSegment, ref this._RightSevenSegDisp);
        }

        private void Pc1_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc1, 1, ref RightSevenSegment, ref this._RightSevenSegDisp);
        }

        private void Pc2_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc2, 2, ref RightSevenSegment, ref this._RightSevenSegDisp);
        }

        private void Pc3_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc3, 3, ref RightSevenSegment, ref this._RightSevenSegDisp);
        }

        private void Pc4_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc4, 0, ref LeftSevenSegment, ref this._LeftSevenSegDisp);
        }

        private void Pc5_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc5, 1, ref LeftSevenSegment, ref this._LeftSevenSegDisp);
        }

        private void Pc6_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc6, 2, ref LeftSevenSegment, ref this._LeftSevenSegDisp);
        }

        private void Pc7_CheckedChanged(object sender, EventArgs e)
        {
            updateBothSevenSeg(Pc7, 3, ref LeftSevenSegment, ref this._LeftSevenSegDisp);
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            this.SetPortC();
        }

        private void ReadPinA()
        {
            int response = SendAndValidateSerialComms(false, READ_PINA);
            Bulb.LedBulb[] leds = { LedPa0, LedPa1, LedPa2, LedPa3, LedPa4, LedPa5, LedPa6, LedPa7 };
            for (int i = 0; i < 8; i++)
            {
                if ((response & (1 << i)) == (1 << i))
                    leds[i].On = true;
                else
                    leds[i].On = false;
            }
        }

        private void SetPortC()
        {
            byte setLeds = (byte)((this._LeftSevenSegDisp << 4) + this._RightSevenSegDisp);
            SendAndValidateSerialComms(true, SET_PORTC, setLeds);
        }

        //
        // Tab 3
        //

        private void LightScrollAdj_Scroll(object sender, ScrollEventArgs e)
        {
            int percentOn = (100 - this.LightScrollAdj.Value);
            string displayText = (percentOn.ToString() + "%");
            this.LightPercentOn.Text = displayText;
        }

        private void SetLampLevel()
        {
            int percentOn = (100 - this.LightScrollAdj.Value);
            byte lightVal = (byte)((percentOn * 255) / 100);
            SendAndValidateSerialComms(true, SET_LIGHT, lightVal);
        }


        delegate void SetLdrGaugeCallback(float value);

        private void ReadLdrLevel()
        {
            int ldrLevel = SendAndValidateSerialComms(false, READ_LIGHT);
            if (this.LightGauge.InvokeRequired)
            {
                SetLdrGaugeCallback d = new SetLdrGaugeCallback(SetLdrGauge);
                this.Invoke(d, new object[] { ldrLevel });
            }
            else
            {
                this.LightGauge.Value = ldrLevel;
            }
        }

        private void SetLdrGauge(float value)
        {
            this.LightGauge.Value = value;
        }

        delegate void SetPot1GaugeCallback(float value);
        delegate void SetPot2GaugeCallback(float value);

        private void ReadPotVoltage(byte pot)
        {
            int potVal = SendAndValidateSerialComms(false, pot);
            float voltage = 0.02f * potVal;
            if (pot == READ_POT1)
            {
                if(this.Pot1Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot1GaugeCallback d = new SetPot1GaugeCallback(SetPot1Gauge);
                    this.Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    this.Pot1Gauge.Value = voltage;
                }
            } 
            else
            {
                if (this.Pot2Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot2GaugeCallback d = new SetPot2GaugeCallback(SetPot2Gauge);
                    this.Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    this.Pot2Gauge.Value = voltage;
                }
            }
        }

        private void SetPot1Gauge(float value)
        {
            this.Pot1Gauge.Value = value;
        }

        private void SetPot2Gauge(float value)
        {
            this.Pot2Gauge.Value = value;
        }

        //
        // Tab 4
        //

        private void SetTempBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void KpTuningBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void KiTuningBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void InsrtManualDataBtn_Click(object sender, EventArgs e)
        {

        }

        private void StartLoggingBtn_Click(object sender, EventArgs e)
        {
            StartLoggingBtn.Enabled = false;
            StopLoggingBtn.Enabled = true;
            LoggingInPrgTxt.Visible = true;
        }

        private void StopLoggingBtn_Click(object sender, EventArgs e)
        {
            StopLoggingBtn.Enabled = false;
            StartLoggingBtn.Enabled = true;
            LoggingInPrgTxt.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
