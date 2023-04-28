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
        private static AppBoard ab = new AppBoard();
        public static System.Timers.Timer fiveSecTimer = new System.Timers.Timer(5000);
        private static int _RightSevenSegDisp = 0;
        public int RightSevenSegDisp
        { get { return _RightSevenSegDisp; }}
        private static int _LeftSevenSegDisp = 0;
        public int LeftSevenSegDisp
        { get { return _LeftSevenSegDisp; }}

        // Constructor
        public Form1()
        {
            InitializeComponent();
            InitForm1();
            fiveSecTimer.Elapsed += OnTimerElapsed;
        }


        // Initialization
        private void InitForm1()
        {
            string[] portnames = SerialPort.GetPortNames();
            foreach (string port in portnames)
            {
                ComPortBox.Items.Add(port);
            }

            ab.InitSerialPort();
            ab.InitDebugSerialPort();

            RightSevenSegment.Value = RightSevenSegDisp.ToString();
            LeftSevenSegment.Value = LeftSevenSegDisp.ToString();
            LightPercentOn.Text = ((100 - LightScrollAdj.Value).ToString() + "%");
        }

        // Tab 2: Leds
        private void updateSevenSeg(CheckBox Pcn, int bit_n, SevenSegment seven_segement, int display_value)
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

        // Tab 3: Gauges
        public void SetLdrGauge(float value)
        {
            this.LightGauge.Value = value;
        }

        public void SetPot1Gauge(float value)
        {
            this.Pot1Gauge.Value = value;
        }

        public void SetPot2Gauge(float value)
        {
            this.Pot2Gauge.Value = value;
        }

        /************
         * 
         *  EVENTS  *
         * 
         ************/

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            if (AppBoard.SerialPort.IsOpen)
            {
                ab.SetPortC();
                ab.ReadPinA();
                ab.ReadPotVoltage(AppBoard.READ_POT1);
                ab.ReadPotVoltage(AppBoard.READ_POT2);
                ab.ReadLdrLevel();
                ab.SetLampLevel();
            }
        }


        // Tab 1: Top Section
        private void ComPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppBoard.SerialPort.PortName = Convert.ToString(ComPortBox.Text);
        }
        private void BaudBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppBoard.SerialPort.BaudRate = Convert.ToInt32(BaudBox.Text);
        }

        private void SerialConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AppBoard.SerialPort.Open();
                ab.SendAndValidateSerialComms(true, AppBoard.TXCHECK);
                
                // TxCheck Passed
                SerialConnectBtn.Enabled = false;
                SerialDisconnectBtn.Enabled = true;
                SerialStatusLed.On = true;
                fiveSecTimer.Start();
            }
            catch (TimeoutException)
            {
                Debug.WriteLine("No response from serial port...");
                throw new Exception();
            }
            catch (Exception ex)
            {
                fiveSecTimer.Stop();
                AppBoard.SerialPort.Close();
                SerialConnectBtn.Enabled = true;
                SerialStatusLed.On = false;
                SerialDisconnectBtn.Enabled = false;
                Debug.WriteLine(ex);
            }
        }

        private void SerialDisconnectBtn_Click(object sender, EventArgs e)
        {
            AppBoard.SerialPort.Close();
            SerialConnectBtn.Enabled = true;
            SerialStatusLed.On = false;
            SerialDisconnectBtn.Enabled = false;
            fiveSecTimer.Stop();
        }


        // Tab 1: Bottom Section
        private void DatabaseConnectBtn_Click(object sender, EventArgs e)
        {
            // Parse: ServerName, Username, Password, Database
        }

        private void DatabaseDisconnectBtn_Click(object sender, EventArgs e)
        {

        }


        // Tab 2: Seven Segment Display
        private void Pc0_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc0, 0, RightSevenSegment, RightSevenSegDisp);
        }

        private void Pc1_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc1, 1, RightSevenSegment, RightSevenSegDisp);
        }

        private void Pc2_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc2, 2, RightSevenSegment, RightSevenSegDisp);
        }

        private void Pc3_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc3, 3, RightSevenSegment, RightSevenSegDisp);
        }

        private void Pc4_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc4, 0, LeftSevenSegment, LeftSevenSegDisp);
        }

        private void Pc5_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc5, 1, LeftSevenSegment, LeftSevenSegDisp);
        }

        private void Pc6_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc6, 2, LeftSevenSegment, LeftSevenSegDisp);
        }

        private void Pc7_CheckedChanged(object sender, EventArgs e)
        {
            updateSevenSeg(Pc7, 3, LeftSevenSegment, LeftSevenSegDisp);
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            ab.SetPortC();
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
