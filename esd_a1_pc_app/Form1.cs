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
using MySql.Data.MySqlClient;
using Nito.Collections;

namespace esd_a1_pc_app
{

    public partial class Form1 : Form
    {
        // Comms
        public SerialPort _SerialPort = new SerialPort();
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

        // Database
        MySqlConnection _Conn;
        MySqlConnectionStringBuilder _ConnString = new MySqlConnectionStringBuilder();
        
        // Timer
        private bool _TimersEnabled;

        // Tab 2: LEDs
        private int _RightSevenSegDisp = 0;
        private int _LeftSevenSegDisp = 0;

        // PI Control
        private double _Kp;
        private double _Ki;
        private double _SetTemp;
        private int _MeasuredTemp;
        private double _Integral;
        Deque<double> _ErrTimesDtArray = new Deque<double>();
        private int _Interval = 0;
        private DateTime _PrevTime = DateTime.Now;
        private bool _LogToSql;
        private int _TopValue = 49;
        double _MeasuredInCelcius;

        public Form1()
        {
            // Form initialization
            InitializeComponent();
            InitComboBox();
            InitSerialPort();
            RightSevenSegment.Value = _RightSevenSegDisp.ToString();
            LeftSevenSegment.Value = _LeftSevenSegDisp.ToString();
            LightPercentOn.Text = ((100 - LightScrollAdj.Value).ToString() + "%");
            tabControl1.SelectedIndexChanged += UpdateTimersOnTabChanged;
            ComPortBox.Click += PortNamesBox_Clicked;
        }

        //
        // Timers
        //

        private void UpdateTimersOnTabChanged(object sender, EventArgs e)
        {
            // Disable all
            T2_LedTimer.Enabled = false;
            T3_GaugeTimer.Enabled = false;
            T4_TempTimer.Enabled = false;
            SendAndValidateSerialComms(true, SET_HEATER, 0);
            SendAndValidateSerialComms(true, SET_MOTOR, 0);

            // Enable currently selected tab
            if (_TimersEnabled) // Making sure you're connected to a serial port
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        T2_LedTimer.Enabled = true;
                        break;
                    case 2:
                        T3_GaugeTimer.Enabled = true;
                        break;
                    case 3:
                        InitTab4Values();
                        T4_TempTimer.Enabled = true;
                        break;
                }
            }
        }

        private void T2_LedTimer_Tick(object sender, EventArgs e)
        {
            SetPortC();
            ReadPinA();
        }

        private void T3_GaugeTimer_Tick(object sender, EventArgs e)
        {
            ReadPotVoltage(READ_POT1);
            ReadPotVoltage(READ_POT2);
            ReadLdrLevel();
            SetLampLevel();
        }

        private void T4_TempTimer_Tick(object sender, EventArgs e)
        {
            ReadTemperature();
            SetMotor();
            if (_LogToSql)
            {
                SendTempToSqlServer();
            }
        }

        //
        // Serial Communications
        //
        private void InitSerialPort()
        {
            _SerialPort.BaudRate = 38400;
            _SerialPort.Parity = Parity.None;
            _SerialPort.StopBits = StopBits.One;
            _SerialPort.Handshake = Handshake.None;
            _SerialPort.WriteTimeout = 500;
            _SerialPort.ReadTimeout = 500;
        }

        // Overloaded methods for sending different length byte arrays
        private int SerialSendMcu(byte instruction)
        {
            byte[] output = { START_BYTE, instruction, STOP_BYTE };
            _SerialPort.Write(output, 0, output.Length);
            Stopwatch sw = Stopwatch.StartNew(); // Start timer
            while (_SerialPort.BytesToRead < 1) // Wait for a reply
            {
                if (sw.ElapsedMilliseconds > 1000)
                    throw new TimeoutException(); //Throw timeout if time elapsed is greater than 1sec
            }
            return _SerialPort.ReadByte();
        }

        private int SerialSendMcu(byte instruction, byte b1)
        {
            byte[] output = { START_BYTE, instruction, b1, STOP_BYTE };
            _SerialPort.Write(output, 0, output.Length);
            Stopwatch sw = Stopwatch.StartNew(); // Start timer
            while (_SerialPort.BytesToRead < 1) // Wait for a reply
            {
                if (sw.ElapsedMilliseconds > 1000)
                    throw new TimeoutException(); //Throw timeout if time elapsed is greater than 1sec
            }
            return _SerialPort.ReadByte();
        }

        private int SerialSendMcu(byte instruction, byte b1, byte b2)
        {
            byte[] output = { START_BYTE, instruction, b1, b2, STOP_BYTE };
            _SerialPort.Write(output, 0, output.Length);
            Stopwatch sw = Stopwatch.StartNew(); // Start timer
            while (_SerialPort.BytesToRead < 1) // Wait for a reply
            {
                if (sw.ElapsedMilliseconds > 1000)
                    throw new TimeoutException(); //Throw timeout if time elapsed is greater than 1sec
            }
            return _SerialPort.ReadByte();
        }

        //
        // Tab 1: Top Section
        //

        private void PortNamesBox_Clicked(object sender, EventArgs e)
        {
            // Updates serial port options on click
            ComPortBox.Items.Clear();
            InitComboBox();
        }

        private void InitComboBox()
        {
            // Update combo box
            string[] portnames = SerialPort.GetPortNames();
            foreach (string port in portnames)
            {
                ComPortBox.Items.Add(port);
            }
        }

        private void ComPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SerialPort.PortName = Convert.ToString(ComPortBox.Text);
        }
        private void BaudBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SerialPort.BaudRate = Convert.ToInt32(BaudBox.Text);
        }

        private int SendAndValidateSerialComms(bool validate, byte instruction, params byte[] data)
        {
            try
            {
                int response = 0;
                Stopwatch sw = Stopwatch.StartNew(); // Start timer
                while (response != instruction)
                {
                    // Determine how many bytes have been recieved
                    if (data.Length > 1)
                    {
                        response = SerialSendMcu(instruction, data[0], data[1]);
                        Debug.WriteLine($"<SerTx> Instruct: {instruction:X}\n" +
                            $"<SerTx> MSB: {data[0]:X}\n" +
                            $"<SerTx> LSB: {data[1]:X}\n" +
                            $"<SerRx> Response: {response:X}\n");
                    }
                    else if (data.Length > 0)
                    {
                        response = SerialSendMcu(instruction, data[0]);
                        Debug.WriteLine($"<SerTx> Instruct: {instruction:X}\n" +
                            $"<SerTx> Data: {data[0]:X}\n" +
                            $"<SerRx> Response: {response:X}\n");
                    }
                    else
                    {
                        response = SerialSendMcu(instruction);
                        Debug.WriteLine($"<SerTx> Instruct: {instruction:X}\n" +
                            $"<SerRx> Response: {response:X}\n");
                    }
                    if (sw.ElapsedMilliseconds > 5000) // Timeout if time-elapsed > 5 sec
                        throw new TimeoutException();
                    if (!validate) { break; }
                }
                return response;
            }
            catch (InvalidOperationException) // Exception thrown by Serial class on error
            {
                Debug.WriteLine($"[DISCONECTED]: Please connect a serial device...");
                CloseSerial();
            }
            catch (TimeoutException)
            {
                Debug.WriteLine($"[TIMEOUT]: Response from MCU not received...");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured: {ex.Message}");
            }
            return 0;
        }

        private void SerialConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Open serial and send the serial check byte
                _SerialPort.Open();
                SendAndValidateSerialComms(true, TXCHECK);

                SerialConnectBtn.Enabled = false;
                SerialDisconnectBtn.Enabled = true;
                SerialStatusLed.On = true;
                BaudBox.Enabled = false;
                ComPortBox.Enabled = false;
                _TimersEnabled = true;
            }
            catch (TimeoutException)
            {
                Debug.WriteLine("No response from serial port");
            }
            catch (Exception ex)
            {
                CloseSerial();
                Debug.WriteLine(ex);
            }
        }

        private void CloseSerial()
        {
            _SerialPort.Close();
            SerialConnectBtn.Enabled = true;
            SerialStatusLed.On = false;
            SerialDisconnectBtn.Enabled = false;
            BaudBox.Enabled = true;
            ComPortBox.Enabled = true;
            _TimersEnabled = false;
        }

        private void SerialDisconnectBtn_Click(object sender, EventArgs e)
        {
            _SerialPort.Close();
            SerialConnectBtn.Enabled = true;
            SerialStatusLed.On = false;
            SerialDisconnectBtn.Enabled = false;
            BaudBox.Enabled = true;
            ComPortBox.Enabled = true;
            tabControl1.SelectedIndexChanged -= UpdateTimersOnTabChanged;
        }

        //
        // Tab 1: Bottom Section
        //

        private void DatabaseConnectBtn_Click(object sender, EventArgs e)
        {
            // Password = A)W2/]yZGPhBMl*u
            
            // Create connection string
            _ConnString.Server = ServerNameBox.Text;
            _ConnString.Port = 3306;
            _ConnString.Database = DatabaseBox.Text;
            _ConnString.UserID = UsernameBox.Text;
            _ConnString.Password = PasswordBox.Text;

            try
            {
                // Establish connection
                _Conn = new MySqlConnection(_ConnString.ToString());
                _Conn.Open();
                DatabaseStatusLed.On = true;
                DatabaseConnectBtn.Enabled = false;
                DatabaseDisconnectBtn.Enabled = true;
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"Database connection error: {ex.Message}");
                DatabaseStatusLed.On = false;
                DatabaseConnectBtn.Enabled = true;
                DatabaseDisconnectBtn.Enabled = false;
            }
        }

        private void DatabaseDisconnectBtn_Click(object sender, EventArgs e)
        {
            // Check if a MySQLConnection object has been instanciated
            if (_Conn != null)
            {
                _Conn.Close();
                _Conn = null;
                DatabaseStatusLed.On = false;
                DatabaseConnectBtn.Enabled = true;
                DatabaseDisconnectBtn.Enabled = false;
            }
        }

        //
        // Tab 2
        //

        private void UpdateBothSevenSeg(CheckBox Pcn, int bit_n, SevenSegment seven_segement, ref int display_value)
        {
            // Take a checkbox object and update the seven segment accordingly
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
            UpdateBothSevenSeg(Pc0, 0, RightSevenSegment, ref _RightSevenSegDisp);
        }

        private void Pc1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc1, 1, RightSevenSegment, ref _RightSevenSegDisp);
        }

        private void Pc2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc2, 2, RightSevenSegment, ref _RightSevenSegDisp);
        }

        private void Pc3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc3, 3, RightSevenSegment, ref _RightSevenSegDisp);
        }

        private void Pc4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc4, 0, LeftSevenSegment, ref _LeftSevenSegDisp);
        }

        private void Pc5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc5, 1, LeftSevenSegment, ref _LeftSevenSegDisp);
        }

        private void Pc6_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc6, 2, LeftSevenSegment, ref _LeftSevenSegDisp);
        }

        private void Pc7_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBothSevenSeg(Pc7, 3, LeftSevenSegment, ref _LeftSevenSegDisp);
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            SetPortC();
        }

        private void ReadPinA()
        {
            // Read PINA and update the corisponding LEDs
            int response = SendAndValidateSerialComms(false, READ_PINA);
            LedBulb[] leds = { LedPa0, LedPa1, LedPa2, LedPa3, LedPa4, LedPa5, LedPa6, LedPa7 };
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
            byte[] setLeds = { (byte)((this._LeftSevenSegDisp << 4) + this._RightSevenSegDisp) };
            SendAndValidateSerialComms(true, SET_PORTC, setLeds);
        }

        //
        // Tab 3
        //

        private void LightScrollAdj_Scroll(object sender, ScrollEventArgs e)
        {
            // Read Scroll bar and update text
            int percentOn = (100 - LightScrollAdj.Value);
            string displayText = (percentOn.ToString() + "%");
            LightPercentOn.Text = displayText;
        }

        private void SetLampLevel()
        {
            // Take user input from GUI and convert into lamps on/off range
            int scrollVal = (100 - LightScrollAdj.Value); // Flip scroll value
            int onAmount = (int) (scrollVal * ((double) _TopValue) / 100);

            // Convert int into bytes for serial protocol
            byte[] bytes = BitConverter.GetBytes(onAmount);
            byte msb = bytes[1];
            byte lsb = bytes[0];

            // Send the bytes
            SendAndValidateSerialComms(true, SET_LIGHT, msb, lsb);
            Debug.WriteLine($"Scroll value: {LightScrollAdj.Value}, txValue: {onAmount}");
        }


        delegate void SetLdrGaugeCallback(float value);

        private void ReadLdrLevel()
        {
            // Receive and update LDR values
            int ldrLevel = SendAndValidateSerialComms(false, READ_LIGHT);
            if (LightGauge.InvokeRequired)
            {
                SetLdrGaugeCallback d = new SetLdrGaugeCallback(SetLdrGauge);
                Invoke(d, new object[] { ldrLevel });
            }
            else
            {
                LightGauge.Value = ldrLevel;
            }
        }

        private void SetLdrGauge(float value)
        {
            LightGauge.Value = value;
        }

        delegate void SetPot1GaugeCallback(float value);
        delegate void SetPot2GaugeCallback(float value);

        private void ReadPotVoltage(byte pot)
        {
            // Receive and update values on Gauges
            int potVal = SendAndValidateSerialComms(false, pot);
            float voltage = 0.02f * potVal;
            if (pot == READ_POT1)
            {
                if (Pot1Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot1GaugeCallback d = new SetPot1GaugeCallback(SetPot1Gauge);
                    Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    Pot1Gauge.Value = voltage;
                }
            }
            else
            {
                if (Pot2Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot2GaugeCallback d = new SetPot2GaugeCallback(SetPot2Gauge);
                    Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    Pot2Gauge.Value = voltage;
                }
            }
        }

        private void SetPot1Gauge(float value)
        {
            Pot1Gauge.Value = value;
        }

        private void SetPot2Gauge(float value)
        {
            Pot2Gauge.Value = value;
        }

        //
        // Tab 4
        //

        private void SetTemp()
        {
            _SetTemp = Convert.ToDouble(SetTempBox.Value);
            _SetTemp *= 2.5; // Converting from degrees celcius to digital value (0 to 200)
        }

        private void InitTab4Values()
        {
            // Init values
            SetTemp();
            _Kp = Convert.ToDouble(KpTuningBox.Value);
            _Ki = Convert.ToDouble(KiTuningBox.Value);
            SendAndValidateSerialComms(true, SET_HEATER ,1); // Heater on
        }

        private void SetTempBox_ValueChanged(object sender, EventArgs e)
        {
            SetTemp();
        }

        private void KpTuningBox_ValueChanged(object sender, EventArgs e)
        {
            _Kp = Convert.ToDouble(KpTuningBox.Value);
        }

        private void KiTuningBox_ValueChanged(object sender, EventArgs e)
        {
            _Ki = Convert.ToDouble(KiTuningBox.Value);
        }

        private void ReadTemperature()
        {
            // Update measured temp and display it
            _MeasuredTemp = SendAndValidateSerialComms(false, READ_TEMP);

            _MeasuredInCelcius = _MeasuredTemp * 0.4;
            TempReadingTxtBox.Text = (_MeasuredInCelcius).ToString(); // Coverting to degrees celsius

            // Save timestamp
            DateTime currentTime = DateTime.Now;
            _Interval += (currentTime - _PrevTime).Milliseconds;
            string time = (_Interval / 1000).ToString("G"); // Convert to string

            // Plot data to graph
            chart1.Series["Series1"].Points.AddXY(time, _MeasuredInCelcius);
            _PrevTime = currentTime;
        }

        private void SetMotor()
        {
            /*  Pseudocode used:
                
                previous_error := 0
                integral := 0
                loop:
                    error := setpoint − measured_value
                    proportional := error;
                    integral := integral + error × dt
                    derivative := (error − previous_error) / dt
                    output := Kp × proportional + Ki × integral + Kd × derivative
                    previous_error := error
                    wait(dt)
                    goto loop
             */

            // PI calculation
            double dt = 0.05;
            ReadTemperature();
            double error = _MeasuredTemp - _SetTemp; // if measured > set, then we want a positive value
            double proportional = error;

            // Only sample 100 values
            if (_ErrTimesDtArray.Count > 100)
            {
                _ErrTimesDtArray.RemoveFromBack();
            }
            _ErrTimesDtArray.Prepend(error * dt);
            _Integral = _ErrTimesDtArray.Sum();

            double output = (_Kp * proportional + _Ki * _Integral);

            // Limit motor values between 0 to _TopValue
            if (output < 0)
            {
                output = 0;
            } 
            else if (output > _TopValue)
            {
                output = _TopValue;
            }

            // Send new motor speed
            byte[] bytes = BitConverter.GetBytes((int) output);
            byte lsb = bytes[0];
            byte msb = bytes[1];
            SendAndValidateSerialComms(false, SET_MOTOR , msb, lsb);

            // Display Motor Speed
            double percentOn = ((double) (output) / _TopValue);
            MotorSpdTxtBox.Text = $"{percentOn:P}";
        }

        private void InsrtManualDataBtn_Click(object sender, EventArgs e)
        {
            SendTempToSqlServer();
        }

        private void StartLoggingBtn_Click(object sender, EventArgs e)
        {
            StartLoggingBtn.Enabled = false;
            StopLoggingBtn.Enabled = true;
            LoggingInPrgTxt.Visible = true;
            _LogToSql = true;
        }

        private void StopLoggingBtn_Click(object sender, EventArgs e)
        {
            StopLogging();
        }

        private void SendTempToSqlServer()
        {
            try
            {
                if (_Conn != null) // Check if there is a current SQL connection object
                {
                    // SQL Query to insert data into table
                    string query = "INSERT INTO `temperature` (`temperature`, `timestamp`, `remark`) " +
                        $"VALUES ('{_MeasuredInCelcius}', '{DateTime.Now}', '{UsernameBox.Text}')";
                    MySqlCommand cmd = new MySqlCommand(query, _Conn);
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("Data added successfully!");
                }
                else
                {
                    Debug.WriteLine("[WARNING]: Not connected to database");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured: {ex.Message}");
                StopLogging();
            }
        }

        private void StopLogging()
        {
            StopLoggingBtn.Enabled = false;
            StartLoggingBtn.Enabled = true;
            LoggingInPrgTxt.Visible = false;
            _LogToSql = false;
        }
    }
}
