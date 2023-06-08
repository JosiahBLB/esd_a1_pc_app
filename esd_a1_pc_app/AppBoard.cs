using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace esd_a1_pc_app
{
    public class AppBoard
    {
        public static SerialPort SerialPort = new SerialPort();
        public static SerialPort DebugPort = new SerialPort();
        delegate void SetLdrGaugeCallback(float value);
        delegate void SetPot1GaugeCallback(float value);
        delegate void SetPot2GaugeCallback(float value);

        public const byte START_BYTE = 0x53;
        public const byte STOP_BYTE = 0xAA;
        public const byte TXCHECK = 0x00;
        public const byte READ_PINA = 0x01;
        public const byte READ_POT1 = 0x02;
        public const byte READ_POT2 = 0x03;
        public const byte READ_TEMP = 0X04;
        public const byte READ_LIGHT = 0x05;
        public const byte SET_PORTC = 0X0A;
        public const byte SET_HEATER = 0X0B;
        public const byte SET_LIGHT = 0X0C;
        public const byte SET_MOTOR = 0X0D;

        public int SerialSendMcu(byte instruction)
        {
            byte[] output = { START_BYTE, instruction, STOP_BYTE };
            SerialPort.Write(output, 0, output.Length);
            while (SerialPort.BytesToRead < 1) ;
            return SerialPort.ReadByte();
        }

        public int SerialSendMcu(byte instruction, byte b1)
        {
            byte[] output = { START_BYTE, instruction, b1, STOP_BYTE };
            SerialPort.Write(output, 0, output.Length);
            while (SerialPort.BytesToRead < 1) ;
            return SerialPort.ReadByte();
        }

        public int SerialSendMcu(byte instruction, byte b1, byte b2)
        {
            byte[] output = { START_BYTE, instruction, b1, b2, STOP_BYTE };
            SerialPort.Write(output, 0, output.Length);
            while (SerialPort.BytesToRead < 1) ;
            return SerialPort.ReadByte();
        }
        public int SendAndValidateSerialComms(bool validate, byte instruction, params byte[] data)
        {
            try
            {
                int response = 0;
                Stopwatch sw = Stopwatch.StartNew(); // Start timer
                while (response != instruction)
                {
                    if (data.Length > 1)
                        response = SerialSendMcu(instruction, data[0], data[1]); // Send data, check response
                    else if (data.Length > 0)
                        response = SerialSendMcu(instruction, data[0]); // Send data, check response
                    else
                        response = SerialSendMcu(instruction); // Send instruction, check response
                    if (sw.ElapsedMilliseconds > 5000) // Timeout if time-elapsed > 5 sec
                        throw new TimeoutException();
                    if (!validate) { break; }
                }
                Debug.WriteLine($"<SerTx> Instruct: {instruction.ToString("X")}\n<SerRx> Response: {response.ToString("X")}\n");
                return response;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please connect a serial device", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool SerialConnect()
        {
            try
            {
                SerialPort.Open();
                int response = 0xff;
                response = SendAndValidateSerialComms(true, TXCHECK);
                Form1.fiveSecTimer.Start();
                return true;
            }
            catch (TimeoutException)
            {
                Debug.WriteLine("No response from serial port");
                throw new Exception();
            }
            catch (Exception ex)
            {
                SerialPort.Close();
                Debug.WriteLine(ex);
                return false;
            }
        }

        public void OnDataReceived_Debug(object sender, EventArgs e) // Simulates MCU Response
        {
            List<byte> output = new List<byte>();
            byte rxByte;
            while (DebugPort.BytesToRead > 0) // Adds all the bytes in buffer to output list
            {
                rxByte = (byte) DebugPort.ReadByte();
                output.Add(rxByte);
            }

            if (output.Count == 3)
                Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}");
            else if (output.Count == 4)
                Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}, Data1: {output[2]}");
            else if (output.Count == 5)
                Debug.WriteLine($"<MCU> Instruct: {output[1].ToString("X")}, Data1: {output[2]}, Data2: {output[3]}");

            if (output[0] == START_BYTE)
            {
                if (output[1] > 0x05 || output[1] == TXCHECK)
                {
                    byte[] address = { output[1] }; // Return instruction
                    DebugPort.Write(address, 0, 1);
                }
                else
                {
                    byte rubbishVal = 0x0f;
                    byte[] adch = { rubbishVal }; // Arbitrary value
                    DebugPort.Write(adch, 0, 1);
                }
            }
        }

        //
        // Input and Ouputs
        //
        public void SetPortC()
        {
            byte setLeds = (byte)((Program.f1.LeftSevenSegDisp << 4) + Program.f1.RightSevenSegDisp);
            SendAndValidateSerialComms(true, SET_PORTC, setLeds);
        }

        public void ReadPinA()
        {
            int response = SendAndValidateSerialComms(false, READ_PINA);
            Bulb.LedBulb[] leds = { 
                Program.f1.LedPa0, 
                Program.f1.LedPa1, 
                Program.f1.LedPa2, 
                Program.f1.LedPa3, 
                Program.f1.LedPa4, 
                Program.f1.LedPa5, 
                Program.f1.LedPa6, 
                Program.f1.LedPa7 };
            for (int i = 0; i < 8; i++)
            {
                if ((response & (1 << i)) == (1 << i))
                    leds[i].On = true;
                else
                    leds[i].On = false;
            }
        }

        public void SetLampLevel()
        {
            int percentOn = (100 - Program.f1.LightScrollAdj.Value);
            byte lightVal = (byte)((percentOn * 255) / 100);
            SendAndValidateSerialComms(true, SET_LIGHT, lightVal);
        }

        public void ReadLdrLevel()
        {
            int ldrLevel = SendAndValidateSerialComms(false, READ_LIGHT);
            if (Program.f1.LightGauge.InvokeRequired)
            {
                SetLdrGaugeCallback d = new SetLdrGaugeCallback(Program.f1.SetLdrGauge);
                Program.f1.Invoke(d, new object[] { ldrLevel });
            }
            else
            {
                Program.f1.LightGauge.Value = ldrLevel;
            }
        }

        public void ReadPotVoltage(byte pot)
        {
            int potVal = SendAndValidateSerialComms(false, pot);
            float voltage = 0.02f * potVal;
            if (pot == READ_POT1) // Either pot1 or pot2
            {
                if (Program.f1.Pot1Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot1GaugeCallback d = new SetPot1GaugeCallback(Program.f1.SetPot1Gauge);
                    Program.f1.Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    Program.f1.Pot1Gauge.Value = voltage;
                }
            }
            else
            {
                if (Program.f1.Pot2Gauge.InvokeRequired) // Cross thread safe
                {
                    SetPot2GaugeCallback d = new SetPot2GaugeCallback(Program.f1.SetPot1Gauge);
                    Program.f1.Invoke(d, new object[] { voltage });
                }
                else // Standard
                {
                    Program.f1.Pot2Gauge.Value = voltage;
                }
            }
        }

        public void InitSerialPort()
        {
            SerialPort.BaudRate = 38400;
            SerialPort.Parity = Parity.None;
            SerialPort.StopBits = StopBits.One;
            SerialPort.Handshake = Handshake.None;
            SerialPort.WriteTimeout = 500;
            SerialPort.ReadTimeout = 500;
        }

        public void InitDebugSerialPort() // For testing purposes
        {
            DebugPort = new SerialPort();
            DebugPort.PortName = "COM8";
            DebugPort.BaudRate = 38400;
            DebugPort.Parity = Parity.None;
            DebugPort.StopBits = StopBits.One;
            DebugPort.Handshake = Handshake.None;
            DebugPort.WriteTimeout = 500;
            DebugPort.ReadTimeout = 500;
            DebugPort.DataReceived += this.OnDataReceived_Debug;
            DebugPort.Open();
        }
    }
}
