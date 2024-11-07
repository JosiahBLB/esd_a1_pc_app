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
using K4os.Compression.LZ4;
using System.Timers;

namespace esd_a1_pc_app
{

    public partial class Form1 : Form
    {
        private LabBoard _labBoard = new LabBoard();

        delegate void SetLdrGaugeCallback();
        delegate void SetPot1GaugeCallback();
        delegate void SetPot2GaugeCallback();

        // Database
        MySqlConnection _Conn;
        MySqlConnectionStringBuilder _ConnString = new MySqlConnectionStringBuilder();
        private System.Timers.Timer T2_LedTimer = new System.Timers.Timer();
        private System.Timers.Timer T3_GaugeTimer = new System.Timers.Timer();
        private System.Timers.Timer T4_TempTimer = new System.Timers.Timer();

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
        private const int TOP_VALUE = 49;
        double _MeasuredInCelcius;

        public Form1()
        {
            // Form initialization
            InitializeComponent();
            UpdatePortNamesBox();
            RightSevenSegment.Value = _RightSevenSegDisp.ToString();
            LeftSevenSegment.Value = _LeftSevenSegDisp.ToString();
            LightPercentOn.Text = ((100 - LightScrollAdj.Value).ToString() + "%");
            tabControl1.SelectedIndexChanged += UpdateTimersOnTabChanged;
            ComPortBox.Click += PortNamesBox_Clicked;
            T2_LedTimer.Interval = LabBoard.TIMEOUT;
            T2_LedTimer.Elapsed += T2_LedTimer_Tick;
            T3_GaugeTimer.Interval = LabBoard.TIMEOUT;
            T3_GaugeTimer.Elapsed += T3_GaugeTimer_Tick;
            T4_TempTimer.Interval = LabBoard.TIMEOUT;
            T4_TempTimer.Elapsed += T4_TempTimer_Tick;
        }

        //
        // Timers
        //

        private async void UpdateTimersOnTabChanged(object sender, EventArgs e)
        {
            // Disable all
            T2_LedTimer.Enabled = false;
            T3_GaugeTimer.Enabled = false;
            T4_TempTimer.Enabled = false;
            await _labBoard.enableHeater(false);
            await _labBoard.writeMotor(0);

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

        private async void T2_LedTimer_Tick(object sender, EventArgs e)
        {
            await _labBoard.writePortC((byte)((_LeftSevenSegDisp << 4) + _RightSevenSegDisp));
            int pinAValue = await _labBoard.readPinA();
            LedBulb[] leds = { LedPa0, LedPa1, LedPa2, LedPa3, LedPa4, LedPa5, LedPa6, LedPa7 };
            for (int i = 0; i < 8; i++)
            {
                if ((pinAValue & (1 << i)) != 0)
                    leds[i].On = true;
                else
                    leds[i].On = false;
            }
        }

        private async void T3_GaugeTimer_Tick(object sender, EventArgs e)
        {
            // read scroll bar position
            int scrollVal = (100 - LightScrollAdj.Value); // flip value
            UInt16 onPercent = (UInt16)(scrollVal * ((double)TOP_VALUE) / 100); // on percent of 49 (top value)
            await _labBoard.writeLamp(onPercent);

            // update gauges
            (float pot1Voltage, float pot2Voltage) = await _labBoard.readPotV();
            int ldrLevel = await _labBoard.readLamp();
            // thread safe assignment
            SetLdrGaugeCallback setLdrGauge = new SetLdrGaugeCallback(() => { LightGauge.Value = ldrLevel; });
            SetPot1GaugeCallback setPot1Gauge = new SetPot1GaugeCallback(() => { Pot1Gauge.Value = pot1Voltage; });
            SetPot2GaugeCallback setPot2Gauge = new SetPot2GaugeCallback(() => { Pot2Gauge.Value = pot2Voltage; });
            Invoke(setLdrGauge);
            Invoke(setPot1Gauge); 
            Invoke(setPot2Gauge);
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
        // Tab 1: Top Section
        //

        private void UpdatePortNamesBox()
        {
            // Updates serial port options on click
            ComPortBox.Items.Clear();
            string[] portnames = SerialPort.GetPortNames();
            foreach (string port in portnames)
            {
                ComPortBox.Items.Add(port);
            }
        }

        private void PortNamesBox_Clicked(object sender, EventArgs e)
        {
            UpdatePortNamesBox();
        }

        private void ComPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           _labBoard.setComPort(ComPortBox.Text);
        }
        private void BaudBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _labBoard.setBaudrate(Convert.ToInt32(BaudBox.Text));
        }

        private async void SerialConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Open serial and send the serial check byte
                await _labBoard.connect();

                SerialConnectBtn.Enabled = false;
                SerialDisconnectBtn.Enabled = true;
                SerialStatusLed.On = true;
                BaudBox.Enabled = false;
                ComPortBox.Enabled = false;
                _TimersEnabled = true;
            }
            catch (TimeoutException)
            {
                SerialDisconnect();
                Debug.WriteLine("No response from serial port");
            }
            catch (Exception ex)
            {
                SerialDisconnect();
                Debug.WriteLine(ex);
            }
        }

            
        private void SerialDisconnectBtn_Click(object sender, EventArgs e)
        {
            SerialDisconnect();
        }

        private void SerialDisconnect()
        {
            _labBoard.disconnect();
            SerialConnectBtn.Enabled = true;
            SerialStatusLed.On = false;
            SerialDisconnectBtn.Enabled = false;
            BaudBox.Enabled = true;
            ComPortBox.Enabled = true;
            _TimersEnabled = false;
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

        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            await _labBoard.writePortC((byte)((_LeftSevenSegDisp << 4) + _RightSevenSegDisp));
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

        private async void SetLampLevel()
        {
            // Take user input from GUI and convert into lamps on/off range
            int scrollVal = (100 - LightScrollAdj.Value); // Flip scroll value
            UInt16 onAmount = (UInt16) (scrollVal * ((double) TOP_VALUE) / 100);

            // Convert int into bytes for serial protocol
            byte[] bytes = BitConverter.GetBytes(onAmount);
            byte msb = bytes[1];
            byte lsb = bytes[0];

            // Send the bytes
            await _labBoard.writeLamp(onAmount);
        }

        //
        // Tab 4
        //

        private void SetTemp()
        {
            _SetTemp = Convert.ToDouble(SetTempBox.Value);
            _SetTemp *= 2.5; // Converting from degrees celcius to digital value (0 to 200)
        }

        private async void InitTab4Values()
        {
            // Init values
            SetTemp();
            _Kp = Convert.ToDouble(KpTuningBox.Value);
            _Ki = Convert.ToDouble(KiTuningBox.Value);
            await _labBoard.enableHeater(true);
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
            _MeasuredTemp = _labBoard.readTemp().Result;

            _MeasuredInCelcius = _MeasuredTemp * 0.4;
            TempReadingTxtBox.Invoke(new Action(() => TempReadingTxtBox.Text = _MeasuredInCelcius.ToString())); // Coverting to degrees celsius

            // Save timestamp
            DateTime currentTime = DateTime.Now;
            _Interval += (currentTime - _PrevTime).Milliseconds;
            string time = (_Interval / 1000).ToString("G"); // Convert to string

            // Plot data to graph
            chart1.Invoke(new Action(() => chart1.Series["Series1"].Points.AddXY(time, _MeasuredInCelcius)));
            _PrevTime = currentTime;
        }

        private async void SetMotor()
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
            else if (output > TOP_VALUE)
            {
                output = TOP_VALUE;
            }

            // Send new motor speed
            await _labBoard.writeMotor((UInt16) output);

            // Display Motor Speed
            double percentOn = ((double) (output) / TOP_VALUE);
            MotorSpdTxtBox.Invoke(new Action(() => MotorSpdTxtBox.Text = $"{percentOn:P}"));
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
