using System.IO.Ports;

namespace esd_a1_pc_app
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.LoggingInPrgTxt = new System.Windows.Forms.Label();
            this.StartLoggingBtn = new System.Windows.Forms.Button();
            this.StopLoggingBtn = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ManualDataEntTxtBox = new System.Windows.Forms.TextBox();
            this.InsrtManualDataBtn = new System.Windows.Forms.Button();
            this.MotorSpdTxtBox = new System.Windows.Forms.TextBox();
            this.TempReadingTxtBox = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.KiTuningBox = new System.Windows.Forms.NumericUpDown();
            this.KpTuningBox = new System.Windows.Forms.NumericUpDown();
            this.SetTempBox = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LightPercentOn = new System.Windows.Forms.TextBox();
            this.LightScrollAdj = new System.Windows.Forms.VScrollBar();
            this.LightGauge = new AquaControls.AquaGauge();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Pot2Gauge = new AquaControls.AquaGauge();
            this.Pot1Gauge = new AquaControls.AquaGauge();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Pa0 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Pa7 = new System.Windows.Forms.Label();
            this.Pa6 = new System.Windows.Forms.Label();
            this.Pa5 = new System.Windows.Forms.Label();
            this.Pa4 = new System.Windows.Forms.Label();
            this.Pa3 = new System.Windows.Forms.Label();
            this.Pa2 = new System.Windows.Forms.Label();
            this.Pa1 = new System.Windows.Forms.Label();
            this.LedPa7 = new Bulb.LedBulb();
            this.LedPa6 = new Bulb.LedBulb();
            this.LedPa5 = new Bulb.LedBulb();
            this.LedPa4 = new Bulb.LedBulb();
            this.LedPa3 = new Bulb.LedBulb();
            this.LedPa2 = new Bulb.LedBulb();
            this.LedPa1 = new Bulb.LedBulb();
            this.LedPa0 = new Bulb.LedBulb();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RightSevenSegment = new DmitryBrant.CustomControls.SevenSegment();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.Pc7 = new System.Windows.Forms.CheckBox();
            this.LeftSevenSegment = new DmitryBrant.CustomControls.SevenSegment();
            this.Pc1 = new System.Windows.Forms.CheckBox();
            this.Pc6 = new System.Windows.Forms.CheckBox();
            this.Pc0 = new System.Windows.Forms.CheckBox();
            this.Pc5 = new System.Windows.Forms.CheckBox();
            this.Pc2 = new System.Windows.Forms.CheckBox();
            this.Pc4 = new System.Windows.Forms.CheckBox();
            this.Pc3 = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SerialStatusLed = new Bulb.LedBulb();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SerialConnectBtn = new System.Windows.Forms.Button();
            this.SerialDisconnectBtn = new System.Windows.Forms.Button();
            this.ComPortBox = new System.Windows.Forms.ComboBox();
            this.BaudBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DatabaseStatusLed = new Bulb.LedBulb();
            this.ServerNameBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.DatabaseBox = new System.Windows.Forms.TextBox();
            this.DatabaseConnectBtn = new System.Windows.Forms.Button();
            this.DatabaseDisconnectBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KiTuningBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KpTuningBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetTempBox)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.MotorSpdTxtBox);
            this.tabPage4.Controls.Add(this.TempReadingTxtBox);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.chart1);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.KiTuningBox);
            this.tabPage4.Controls.Add(this.KpTuningBox);
            this.tabPage4.Controls.Add(this.SetTempBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(40, 40, 60, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(582, 696);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Temp Control";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(13, 383);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(438, 155);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Cloud Data Logging";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.LoggingInPrgTxt);
            this.groupBox9.Controls.Add(this.StartLoggingBtn);
            this.groupBox9.Controls.Add(this.StopLoggingBtn);
            this.groupBox9.Location = new System.Drawing.Point(231, 26);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(194, 119);
            this.groupBox9.TabIndex = 15;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Auto Data Logging";
            // 
            // LoggingInPrgTxt
            // 
            this.LoggingInPrgTxt.AutoSize = true;
            this.LoggingInPrgTxt.Location = new System.Drawing.Point(33, 59);
            this.LoggingInPrgTxt.Margin = new System.Windows.Forms.Padding(10);
            this.LoggingInPrgTxt.Name = "LoggingInPrgTxt";
            this.LoggingInPrgTxt.Size = new System.Drawing.Size(128, 13);
            this.LoggingInPrgTxt.TabIndex = 3;
            this.LoggingInPrgTxt.Text = "data logging in progress...";
            this.LoggingInPrgTxt.Visible = false;
            // 
            // StartLoggingBtn
            // 
            this.StartLoggingBtn.Location = new System.Drawing.Point(8, 21);
            this.StartLoggingBtn.Margin = new System.Windows.Forms.Padding(5);
            this.StartLoggingBtn.Name = "StartLoggingBtn";
            this.StartLoggingBtn.Size = new System.Drawing.Size(179, 23);
            this.StartLoggingBtn.TabIndex = 2;
            this.StartLoggingBtn.Text = "Start Data Logging";
            this.StartLoggingBtn.UseVisualStyleBackColor = true;
            this.StartLoggingBtn.Click += new System.EventHandler(this.StartLoggingBtn_Click);
            // 
            // StopLoggingBtn
            // 
            this.StopLoggingBtn.Enabled = false;
            this.StopLoggingBtn.Location = new System.Drawing.Point(8, 87);
            this.StopLoggingBtn.Margin = new System.Windows.Forms.Padding(5);
            this.StopLoggingBtn.Name = "StopLoggingBtn";
            this.StopLoggingBtn.Size = new System.Drawing.Size(179, 23);
            this.StopLoggingBtn.TabIndex = 1;
            this.StopLoggingBtn.Text = "Stop Data Logging";
            this.StopLoggingBtn.UseVisualStyleBackColor = true;
            this.StopLoggingBtn.Click += new System.EventHandler(this.StopLoggingBtn_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ManualDataEntTxtBox);
            this.groupBox8.Controls.Add(this.InsrtManualDataBtn);
            this.groupBox8.Location = new System.Drawing.Point(13, 26);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 119);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Manual Data Logging";
            // 
            // ManualDataEntTxtBox
            // 
            this.ManualDataEntTxtBox.Location = new System.Drawing.Point(8, 56);
            this.ManualDataEntTxtBox.Name = "ManualDataEntTxtBox";
            this.ManualDataEntTxtBox.Size = new System.Drawing.Size(179, 20);
            this.ManualDataEntTxtBox.TabIndex = 1;
            // 
            // InsrtManualDataBtn
            // 
            this.InsrtManualDataBtn.Location = new System.Drawing.Point(8, 87);
            this.InsrtManualDataBtn.Margin = new System.Windows.Forms.Padding(5);
            this.InsrtManualDataBtn.Name = "InsrtManualDataBtn";
            this.InsrtManualDataBtn.Size = new System.Drawing.Size(179, 23);
            this.InsrtManualDataBtn.TabIndex = 0;
            this.InsrtManualDataBtn.Text = "Insert Data to Table";
            this.InsrtManualDataBtn.UseVisualStyleBackColor = true;
            this.InsrtManualDataBtn.Click += new System.EventHandler(this.InsrtManualDataBtn_Click);
            // 
            // MotorSpdTxtBox
            // 
            this.MotorSpdTxtBox.Location = new System.Drawing.Point(37, 316);
            this.MotorSpdTxtBox.Name = "MotorSpdTxtBox";
            this.MotorSpdTxtBox.ReadOnly = true;
            this.MotorSpdTxtBox.Size = new System.Drawing.Size(80, 20);
            this.MotorSpdTxtBox.TabIndex = 12;
            // 
            // TempReadingTxtBox
            // 
            this.TempReadingTxtBox.Location = new System.Drawing.Point(37, 266);
            this.TempReadingTxtBox.Name = "TempReadingTxtBox";
            this.TempReadingTxtBox.ReadOnly = true;
            this.TempReadingTxtBox.Size = new System.Drawing.Size(80, 20);
            this.TempReadingTxtBox.TabIndex = 8;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label24.Location = new System.Drawing.Point(35, 295);
            this.label24.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 13);
            this.label24.TabIndex = 11;
            this.label24.Text = "Motor Speed [%]";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label23.Location = new System.Drawing.Point(36, 245);
            this.label23.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(83, 13);
            this.label23.TabIndex = 10;
            this.label23.Text = "Actual Temp [C]";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Default;
            this.chart1.Location = new System.Drawing.Point(139, 64);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(300, 296);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title1.Name = "Title1";
            title1.Text = "Sample (s)";
            title2.Alignment = System.Drawing.ContentAlignment.BottomCenter;
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title2.Name = "Title2";
            title2.Text = "Temp [C]";
            title2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            this.chart1.Titles.Add(title1);
            this.chart1.Titles.Add(title2);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label22.Location = new System.Drawing.Point(10, 198);
            this.label22.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(16, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "Ki";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label21.Location = new System.Drawing.Point(10, 165);
            this.label21.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 13);
            this.label21.TabIndex = 5;
            this.label21.Text = "Kp";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label20.Location = new System.Drawing.Point(43, 138);
            this.label20.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 17);
            this.label20.TabIndex = 4;
            this.label20.Text = "PI Tuning";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label19.Location = new System.Drawing.Point(17, 64);
            this.label19.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 17);
            this.label19.TabIndex = 3;
            this.label19.Text = "Setpoint Temp [C]";
            // 
            // KiTuningBox
            // 
            this.KiTuningBox.DecimalPlaces = 2;
            this.KiTuningBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.KiTuningBox.Location = new System.Drawing.Point(37, 196);
            this.KiTuningBox.Margin = new System.Windows.Forms.Padding(50, 10, 3, 3);
            this.KiTuningBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.KiTuningBox.Name = "KiTuningBox";
            this.KiTuningBox.Size = new System.Drawing.Size(80, 20);
            this.KiTuningBox.TabIndex = 2;
            this.KiTuningBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KiTuningBox.ValueChanged += new System.EventHandler(this.KiTuningBox_ValueChanged);
            // 
            // KpTuningBox
            // 
            this.KpTuningBox.DecimalPlaces = 2;
            this.KpTuningBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.KpTuningBox.Location = new System.Drawing.Point(37, 163);
            this.KpTuningBox.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.KpTuningBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.KpTuningBox.Name = "KpTuningBox";
            this.KpTuningBox.Size = new System.Drawing.Size(80, 20);
            this.KpTuningBox.TabIndex = 1;
            this.KpTuningBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.KpTuningBox.ValueChanged += new System.EventHandler(this.KpTuningBox_ValueChanged);
            // 
            // SetTempBox
            // 
            this.SetTempBox.Location = new System.Drawing.Point(37, 89);
            this.SetTempBox.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.SetTempBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.SetTempBox.Name = "SetTempBox";
            this.SetTempBox.Size = new System.Drawing.Size(80, 20);
            this.SetTempBox.TabIndex = 0;
            this.SetTempBox.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SetTempBox.ValueChanged += new System.EventHandler(this.SetTempBox_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(582, 696);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Port-Lights";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LightPercentOn);
            this.groupBox6.Controls.Add(this.LightScrollAdj);
            this.groupBox6.Controls.Add(this.LightGauge);
            this.groupBox6.Location = new System.Drawing.Point(40, 290);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(40, 5, 60, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.groupBox6.Size = new System.Drawing.Size(358, 210);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Light";
            // 
            // LightPercentOn
            // 
            this.LightPercentOn.Location = new System.Drawing.Point(112, 166);
            this.LightPercentOn.Name = "LightPercentOn";
            this.LightPercentOn.Size = new System.Drawing.Size(47, 20);
            this.LightPercentOn.TabIndex = 3;
            // 
            // LightScrollAdj
            // 
            this.LightScrollAdj.Location = new System.Drawing.Point(61, 36);
            this.LightScrollAdj.Maximum = 109;
            this.LightScrollAdj.Name = "LightScrollAdj";
            this.LightScrollAdj.Size = new System.Drawing.Size(25, 150);
            this.LightScrollAdj.TabIndex = 2;
            this.LightScrollAdj.Value = 100;
            this.LightScrollAdj.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollAdj_Scroll);
            // 
            // LightGauge
            // 
            this.LightGauge.BackColor = System.Drawing.Color.Transparent;
            this.LightGauge.DialColor = System.Drawing.Color.Lavender;
            this.LightGauge.DialText = "Measured";
            this.LightGauge.Glossiness = 11.36364F;
            this.LightGauge.Location = new System.Drawing.Point(190, 36);
            this.LightGauge.Margin = new System.Windows.Forms.Padding(4);
            this.LightGauge.MaxValue = 255F;
            this.LightGauge.MinValue = 0F;
            this.LightGauge.Name = "LightGauge";
            this.LightGauge.NoOfDivisions = 5;
            this.LightGauge.RecommendedValue = 0F;
            this.LightGauge.Size = new System.Drawing.Size(150, 150);
            this.LightGauge.TabIndex = 1;
            this.LightGauge.ThresholdPercent = 0F;
            this.LightGauge.Value = 0F;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.Pot2Gauge);
            this.groupBox5.Controls.Add(this.Pot1Gauge);
            this.groupBox5.Location = new System.Drawing.Point(40, 43);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(40, 40, 60, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.groupBox5.Size = new System.Drawing.Size(358, 227);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ports";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(221, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 17);
            this.label18.TabIndex = 3;
            this.label18.Text = "Pot2 Voltage";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(49, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 17);
            this.label17.TabIndex = 2;
            this.label17.Text = "Pot1 Voltage";
            // 
            // Pot2Gauge
            // 
            this.Pot2Gauge.BackColor = System.Drawing.Color.Transparent;
            this.Pot2Gauge.DialColor = System.Drawing.Color.Lavender;
            this.Pot2Gauge.DialText = "Potential Meter 2";
            this.Pot2Gauge.Glossiness = 11.36364F;
            this.Pot2Gauge.Location = new System.Drawing.Point(190, 54);
            this.Pot2Gauge.Margin = new System.Windows.Forms.Padding(4);
            this.Pot2Gauge.MaxValue = 5F;
            this.Pot2Gauge.MinValue = 0F;
            this.Pot2Gauge.Name = "Pot2Gauge";
            this.Pot2Gauge.RecommendedValue = 0F;
            this.Pot2Gauge.Size = new System.Drawing.Size(150, 150);
            this.Pot2Gauge.TabIndex = 1;
            this.Pot2Gauge.ThresholdPercent = 0F;
            this.Pot2Gauge.Value = 0F;
            // 
            // Pot1Gauge
            // 
            this.Pot1Gauge.BackColor = System.Drawing.Color.Transparent;
            this.Pot1Gauge.DialColor = System.Drawing.Color.Lavender;
            this.Pot1Gauge.DialText = "Potential Meter 1";
            this.Pot1Gauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pot1Gauge.Glossiness = 11.36364F;
            this.Pot1Gauge.Location = new System.Drawing.Point(18, 54);
            this.Pot1Gauge.Margin = new System.Windows.Forms.Padding(4);
            this.Pot1Gauge.MaxValue = 5F;
            this.Pot1Gauge.MinValue = 0F;
            this.Pot1Gauge.Name = "Pot1Gauge";
            this.Pot1Gauge.RecommendedValue = 0F;
            this.Pot1Gauge.Size = new System.Drawing.Size(150, 150);
            this.Pot1Gauge.TabIndex = 0;
            this.Pot1Gauge.Tag = "";
            this.Pot1Gauge.ThresholdPercent = 0F;
            this.Pot1Gauge.Value = 0F;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Pa0);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(582, 696);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Digital I/O";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Pa0
            // 
            this.Pa0.AutoSize = true;
            this.Pa0.Location = new System.Drawing.Point(96, 161);
            this.Pa0.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa0.Name = "Pa0";
            this.Pa0.Size = new System.Drawing.Size(27, 13);
            this.Pa0.TabIndex = 17;
            this.Pa0.Text = "PA0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Pa7);
            this.groupBox3.Controls.Add(this.Pa6);
            this.groupBox3.Controls.Add(this.Pa5);
            this.groupBox3.Controls.Add(this.Pa4);
            this.groupBox3.Controls.Add(this.Pa3);
            this.groupBox3.Controls.Add(this.Pa2);
            this.groupBox3.Controls.Add(this.Pa1);
            this.groupBox3.Controls.Add(this.LedPa7);
            this.groupBox3.Controls.Add(this.LedPa6);
            this.groupBox3.Controls.Add(this.LedPa5);
            this.groupBox3.Controls.Add(this.LedPa4);
            this.groupBox3.Controls.Add(this.LedPa3);
            this.groupBox3.Controls.Add(this.LedPa2);
            this.groupBox3.Controls.Add(this.LedPa1);
            this.groupBox3.Controls.Add(this.LedPa0);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox3.Location = new System.Drawing.Point(43, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(109, 286);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PINA";
            // 
            // Pa7
            // 
            this.Pa7.AutoSize = true;
            this.Pa7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa7.Location = new System.Drawing.Point(51, 250);
            this.Pa7.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa7.Name = "Pa7";
            this.Pa7.Size = new System.Drawing.Size(27, 13);
            this.Pa7.TabIndex = 26;
            this.Pa7.Text = "PA7";
            // 
            // Pa6
            // 
            this.Pa6.AutoSize = true;
            this.Pa6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa6.Location = new System.Drawing.Point(51, 219);
            this.Pa6.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa6.Name = "Pa6";
            this.Pa6.Size = new System.Drawing.Size(27, 13);
            this.Pa6.TabIndex = 25;
            this.Pa6.Text = "PA6";
            // 
            // Pa5
            // 
            this.Pa5.AutoSize = true;
            this.Pa5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa5.Location = new System.Drawing.Point(51, 188);
            this.Pa5.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa5.Name = "Pa5";
            this.Pa5.Size = new System.Drawing.Size(27, 13);
            this.Pa5.TabIndex = 24;
            this.Pa5.Text = "PA5";
            // 
            // Pa4
            // 
            this.Pa4.AutoSize = true;
            this.Pa4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa4.Location = new System.Drawing.Point(51, 157);
            this.Pa4.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa4.Name = "Pa4";
            this.Pa4.Size = new System.Drawing.Size(27, 13);
            this.Pa4.TabIndex = 23;
            this.Pa4.Text = "PA4";
            // 
            // Pa3
            // 
            this.Pa3.AutoSize = true;
            this.Pa3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa3.Location = new System.Drawing.Point(51, 126);
            this.Pa3.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa3.Name = "Pa3";
            this.Pa3.Size = new System.Drawing.Size(27, 13);
            this.Pa3.TabIndex = 22;
            this.Pa3.Text = "PA3";
            // 
            // Pa2
            // 
            this.Pa2.AutoSize = true;
            this.Pa2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa2.Location = new System.Drawing.Point(51, 95);
            this.Pa2.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa2.Name = "Pa2";
            this.Pa2.Size = new System.Drawing.Size(27, 13);
            this.Pa2.TabIndex = 21;
            this.Pa2.Text = "PA2";
            // 
            // Pa1
            // 
            this.Pa1.AutoSize = true;
            this.Pa1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pa1.Location = new System.Drawing.Point(51, 64);
            this.Pa1.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.Pa1.Name = "Pa1";
            this.Pa1.Size = new System.Drawing.Size(27, 13);
            this.Pa1.TabIndex = 20;
            this.Pa1.Text = "PA1";
            // 
            // LedPa7
            // 
            this.LedPa7.Color = System.Drawing.Color.Red;
            this.LedPa7.Location = new System.Drawing.Point(19, 246);
            this.LedPa7.Name = "LedPa7";
            this.LedPa7.On = false;
            this.LedPa7.Size = new System.Drawing.Size(75, 23);
            this.LedPa7.TabIndex = 34;
            this.LedPa7.Text = "ledBulb10";
            // 
            // LedPa6
            // 
            this.LedPa6.Color = System.Drawing.Color.Red;
            this.LedPa6.Location = new System.Drawing.Point(19, 215);
            this.LedPa6.Name = "LedPa6";
            this.LedPa6.On = false;
            this.LedPa6.Size = new System.Drawing.Size(75, 23);
            this.LedPa6.TabIndex = 33;
            this.LedPa6.Text = "ledBulb9";
            // 
            // LedPa5
            // 
            this.LedPa5.Color = System.Drawing.Color.Red;
            this.LedPa5.Location = new System.Drawing.Point(19, 184);
            this.LedPa5.Name = "LedPa5";
            this.LedPa5.On = false;
            this.LedPa5.Size = new System.Drawing.Size(75, 23);
            this.LedPa5.TabIndex = 32;
            this.LedPa5.Text = "ledBulb8";
            // 
            // LedPa4
            // 
            this.LedPa4.Color = System.Drawing.Color.Red;
            this.LedPa4.Location = new System.Drawing.Point(19, 153);
            this.LedPa4.Name = "LedPa4";
            this.LedPa4.On = false;
            this.LedPa4.Size = new System.Drawing.Size(75, 23);
            this.LedPa4.TabIndex = 31;
            this.LedPa4.Text = "ledBulb7";
            // 
            // LedPa3
            // 
            this.LedPa3.Color = System.Drawing.Color.Red;
            this.LedPa3.Location = new System.Drawing.Point(19, 122);
            this.LedPa3.Name = "LedPa3";
            this.LedPa3.On = false;
            this.LedPa3.Size = new System.Drawing.Size(75, 23);
            this.LedPa3.TabIndex = 30;
            this.LedPa3.Text = "ledBulb6";
            // 
            // LedPa2
            // 
            this.LedPa2.Color = System.Drawing.Color.Red;
            this.LedPa2.Location = new System.Drawing.Point(19, 91);
            this.LedPa2.Name = "LedPa2";
            this.LedPa2.On = false;
            this.LedPa2.Size = new System.Drawing.Size(75, 23);
            this.LedPa2.TabIndex = 29;
            this.LedPa2.Text = "ledBulb5";
            // 
            // LedPa1
            // 
            this.LedPa1.Color = System.Drawing.Color.Red;
            this.LedPa1.Location = new System.Drawing.Point(19, 60);
            this.LedPa1.Name = "LedPa1";
            this.LedPa1.On = false;
            this.LedPa1.Size = new System.Drawing.Size(75, 23);
            this.LedPa1.TabIndex = 28;
            this.LedPa1.Text = "ledBulb4";
            // 
            // LedPa0
            // 
            this.LedPa0.Color = System.Drawing.Color.Red;
            this.LedPa0.Location = new System.Drawing.Point(19, 29);
            this.LedPa0.Name = "LedPa0";
            this.LedPa0.On = false;
            this.LedPa0.Size = new System.Drawing.Size(75, 23);
            this.LedPa0.TabIndex = 27;
            this.LedPa0.Text = "ledBulb3";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RightSevenSegment);
            this.groupBox4.Controls.Add(this.RefreshBtn);
            this.groupBox4.Controls.Add(this.Pc7);
            this.groupBox4.Controls.Add(this.LeftSevenSegment);
            this.groupBox4.Controls.Add(this.Pc1);
            this.groupBox4.Controls.Add(this.Pc6);
            this.groupBox4.Controls.Add(this.Pc0);
            this.groupBox4.Controls.Add(this.Pc5);
            this.groupBox4.Controls.Add(this.Pc2);
            this.groupBox4.Controls.Add(this.Pc4);
            this.groupBox4.Controls.Add(this.Pc3);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox4.Location = new System.Drawing.Point(185, 126);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(30, 3, 60, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox4.Size = new System.Drawing.Size(216, 286);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PORTC";
            // 
            // RightSevenSegment
            // 
            this.RightSevenSegment.ColonOn = false;
            this.RightSevenSegment.ColonShow = false;
            this.RightSevenSegment.ColorBackground = System.Drawing.Color.Black;
            this.RightSevenSegment.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RightSevenSegment.ColorLight = System.Drawing.Color.Red;
            this.RightSevenSegment.CustomPattern = 0;
            this.RightSevenSegment.DecimalOn = false;
            this.RightSevenSegment.DecimalShow = true;
            this.RightSevenSegment.ElementWidth = 10;
            this.RightSevenSegment.ItalicFactor = 0F;
            this.RightSevenSegment.Location = new System.Drawing.Point(152, 29);
            this.RightSevenSegment.Name = "RightSevenSegment";
            this.RightSevenSegment.Padding = new System.Windows.Forms.Padding(4);
            this.RightSevenSegment.Size = new System.Drawing.Size(41, 64);
            this.RightSevenSegment.TabIndex = 10;
            this.RightSevenSegment.TabStop = false;
            this.RightSevenSegment.Value = null;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RefreshBtn.Location = new System.Drawing.Point(105, 116);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(88, 23);
            this.RefreshBtn.TabIndex = 8;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // Pc7
            // 
            this.Pc7.AutoSize = true;
            this.Pc7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc7.Location = new System.Drawing.Point(23, 249);
            this.Pc7.Name = "Pc7";
            this.Pc7.Size = new System.Drawing.Size(46, 17);
            this.Pc7.TabIndex = 16;
            this.Pc7.Text = "PC7";
            this.Pc7.UseVisualStyleBackColor = true;
            this.Pc7.CheckedChanged += new System.EventHandler(this.Pc7_CheckedChanged);
            // 
            // LeftSevenSegment
            // 
            this.LeftSevenSegment.ColonOn = false;
            this.LeftSevenSegment.ColonShow = false;
            this.LeftSevenSegment.ColorBackground = System.Drawing.Color.Black;
            this.LeftSevenSegment.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LeftSevenSegment.ColorLight = System.Drawing.Color.Red;
            this.LeftSevenSegment.CustomPattern = 0;
            this.LeftSevenSegment.DecimalOn = false;
            this.LeftSevenSegment.DecimalShow = true;
            this.LeftSevenSegment.ElementWidth = 10;
            this.LeftSevenSegment.ItalicFactor = 0F;
            this.LeftSevenSegment.Location = new System.Drawing.Point(105, 29);
            this.LeftSevenSegment.Name = "LeftSevenSegment";
            this.LeftSevenSegment.Padding = new System.Windows.Forms.Padding(4);
            this.LeftSevenSegment.Size = new System.Drawing.Size(41, 64);
            this.LeftSevenSegment.TabIndex = 9;
            this.LeftSevenSegment.TabStop = false;
            this.LeftSevenSegment.Value = null;
            // 
            // Pc1
            // 
            this.Pc1.AutoSize = true;
            this.Pc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc1.Location = new System.Drawing.Point(23, 63);
            this.Pc1.Name = "Pc1";
            this.Pc1.Size = new System.Drawing.Size(46, 17);
            this.Pc1.TabIndex = 10;
            this.Pc1.Text = "PC1";
            this.Pc1.UseVisualStyleBackColor = true;
            this.Pc1.CheckedChanged += new System.EventHandler(this.Pc1_CheckedChanged);
            // 
            // Pc6
            // 
            this.Pc6.AutoSize = true;
            this.Pc6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc6.Location = new System.Drawing.Point(23, 218);
            this.Pc6.Name = "Pc6";
            this.Pc6.Size = new System.Drawing.Size(46, 17);
            this.Pc6.TabIndex = 15;
            this.Pc6.Text = "PC6";
            this.Pc6.UseVisualStyleBackColor = true;
            this.Pc6.CheckedChanged += new System.EventHandler(this.Pc6_CheckedChanged);
            // 
            // Pc0
            // 
            this.Pc0.AutoSize = true;
            this.Pc0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc0.Location = new System.Drawing.Point(23, 32);
            this.Pc0.Name = "Pc0";
            this.Pc0.Size = new System.Drawing.Size(46, 17);
            this.Pc0.TabIndex = 9;
            this.Pc0.Text = "PC0";
            this.Pc0.UseVisualStyleBackColor = true;
            this.Pc0.CheckedChanged += new System.EventHandler(this.Pc0_CheckedChanged);
            // 
            // Pc5
            // 
            this.Pc5.AutoSize = true;
            this.Pc5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc5.Location = new System.Drawing.Point(23, 187);
            this.Pc5.Name = "Pc5";
            this.Pc5.Size = new System.Drawing.Size(46, 17);
            this.Pc5.TabIndex = 14;
            this.Pc5.Text = "PC5";
            this.Pc5.UseVisualStyleBackColor = true;
            this.Pc5.CheckedChanged += new System.EventHandler(this.Pc5_CheckedChanged);
            // 
            // Pc2
            // 
            this.Pc2.AutoSize = true;
            this.Pc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc2.Location = new System.Drawing.Point(23, 94);
            this.Pc2.Name = "Pc2";
            this.Pc2.Size = new System.Drawing.Size(46, 17);
            this.Pc2.TabIndex = 11;
            this.Pc2.Text = "PC2";
            this.Pc2.UseVisualStyleBackColor = true;
            this.Pc2.CheckedChanged += new System.EventHandler(this.Pc2_CheckedChanged);
            // 
            // Pc4
            // 
            this.Pc4.AutoSize = true;
            this.Pc4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc4.Location = new System.Drawing.Point(23, 156);
            this.Pc4.Name = "Pc4";
            this.Pc4.Size = new System.Drawing.Size(46, 17);
            this.Pc4.TabIndex = 13;
            this.Pc4.Text = "PC4";
            this.Pc4.UseVisualStyleBackColor = true;
            this.Pc4.CheckedChanged += new System.EventHandler(this.Pc4_CheckedChanged);
            // 
            // Pc3
            // 
            this.Pc3.AutoSize = true;
            this.Pc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Pc3.Location = new System.Drawing.Point(23, 125);
            this.Pc3.Name = "Pc3";
            this.Pc3.Size = new System.Drawing.Size(46, 17);
            this.Pc3.TabIndex = 12;
            this.Pc3.Text = "PC3";
            this.Pc3.UseVisualStyleBackColor = true;
            this.Pc3.CheckedChanged += new System.EventHandler(this.Pc3_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(40, 40, 60, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(582, 696);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SerialStatusLed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SerialConnectBtn);
            this.groupBox1.Controls.Add(this.SerialDisconnectBtn);
            this.groupBox1.Controls.Add(this.ComPortBox);
            this.groupBox1.Controls.Add(this.BaudBox);
            this.groupBox1.Location = new System.Drawing.Point(43, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(40, 40, 60, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port Connection";
            // 
            // SerialStatusLed
            // 
            this.SerialStatusLed.Location = new System.Drawing.Point(150, 145);
            this.SerialStatusLed.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.SerialStatusLed.Name = "SerialStatusLed";
            this.SerialStatusLed.On = false;
            this.SerialStatusLed.Size = new System.Drawing.Size(32, 23);
            this.SerialStatusLed.TabIndex = 7;
            this.SerialStatusLed.Text = "ledBulb1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baudrate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 20, 25, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Serial Port Status";
            // 
            // SerialConnectBtn
            // 
            this.SerialConnectBtn.Location = new System.Drawing.Point(16, 104);
            this.SerialConnectBtn.Margin = new System.Windows.Forms.Padding(3, 15, 18, 3);
            this.SerialConnectBtn.Name = "SerialConnectBtn";
            this.SerialConnectBtn.Size = new System.Drawing.Size(160, 23);
            this.SerialConnectBtn.TabIndex = 4;
            this.SerialConnectBtn.Text = "Connect";
            this.SerialConnectBtn.UseVisualStyleBackColor = true;
            this.SerialConnectBtn.Click += new System.EventHandler(this.SerialConnectBtn_Click);
            // 
            // SerialDisconnectBtn
            // 
            this.SerialDisconnectBtn.Enabled = false;
            this.SerialDisconnectBtn.Location = new System.Drawing.Point(182, 104);
            this.SerialDisconnectBtn.Margin = new System.Windows.Forms.Padding(3, 15, 18, 3);
            this.SerialDisconnectBtn.Name = "SerialDisconnectBtn";
            this.SerialDisconnectBtn.Size = new System.Drawing.Size(160, 23);
            this.SerialDisconnectBtn.TabIndex = 5;
            this.SerialDisconnectBtn.Text = "Disconnect";
            this.SerialDisconnectBtn.UseVisualStyleBackColor = true;
            this.SerialDisconnectBtn.Click += new System.EventHandler(this.SerialDisconnectBtn_Click);
            // 
            // ComPortBox
            // 
            this.ComPortBox.FormattingEnabled = true;
            this.ComPortBox.Location = new System.Drawing.Point(109, 31);
            this.ComPortBox.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.ComPortBox.Name = "ComPortBox";
            this.ComPortBox.Size = new System.Drawing.Size(121, 21);
            this.ComPortBox.TabIndex = 0;
            this.ComPortBox.SelectedIndexChanged += new System.EventHandler(this.ComPortBox_SelectedIndexChanged);
            // 
            // BaudBox
            // 
            this.BaudBox.FormattingEnabled = true;
            this.BaudBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.BaudBox.Location = new System.Drawing.Point(109, 65);
            this.BaudBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(121, 21);
            this.BaudBox.TabIndex = 1;
            this.BaudBox.Text = "38400";
            this.BaudBox.SelectedIndexChanged += new System.EventHandler(this.BaudBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DatabaseStatusLed);
            this.groupBox2.Controls.Add(this.ServerNameBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.UsernameBox);
            this.groupBox2.Controls.Add(this.PasswordBox);
            this.groupBox2.Controls.Add(this.DatabaseBox);
            this.groupBox2.Controls.Add(this.DatabaseConnectBtn);
            this.groupBox2.Controls.Add(this.DatabaseDisconnectBtn);
            this.groupBox2.Location = new System.Drawing.Point(43, 247);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(40, 5, 60, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 257);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Server Connection";
            // 
            // DatabaseStatusLed
            // 
            this.DatabaseStatusLed.Location = new System.Drawing.Point(239, 214);
            this.DatabaseStatusLed.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.DatabaseStatusLed.Name = "DatabaseStatusLed";
            this.DatabaseStatusLed.On = false;
            this.DatabaseStatusLed.Size = new System.Drawing.Size(32, 23);
            this.DatabaseStatusLed.TabIndex = 8;
            this.DatabaseStatusLed.Text = "ledBulb2";
            // 
            // ServerNameBox
            // 
            this.ServerNameBox.FormattingEnabled = true;
            this.ServerNameBox.Location = new System.Drawing.Point(109, 35);
            this.ServerNameBox.Name = "ServerNameBox";
            this.ServerNameBox.Size = new System.Drawing.Size(175, 21);
            this.ServerNameBox.TabIndex = 3;
            this.ServerNameBox.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Server Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Username";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Database";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 219);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 20, 25, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Database Server Connection Status";
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(109, 69);
            this.UsernameBox.Margin = new System.Windows.Forms.Padding(3, 10, 6, 3);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(175, 20);
            this.UsernameBox.TabIndex = 4;
            this.UsernameBox.Text = "ST123456";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(109, 102);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(3, 10, 6, 3);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '•';
            this.PasswordBox.Size = new System.Drawing.Size(175, 20);
            this.PasswordBox.TabIndex = 5;
            this.PasswordBox.Text = "A)W2/]yZGPhBMl*u";
            // 
            // DatabaseBox
            // 
            this.DatabaseBox.Location = new System.Drawing.Point(109, 135);
            this.DatabaseBox.Margin = new System.Windows.Forms.Padding(3, 10, 6, 3);
            this.DatabaseBox.Name = "DatabaseBox";
            this.DatabaseBox.Size = new System.Drawing.Size(175, 20);
            this.DatabaseBox.TabIndex = 6;
            this.DatabaseBox.Text = "temperature_record";
            // 
            // DatabaseConnectBtn
            // 
            this.DatabaseConnectBtn.Location = new System.Drawing.Point(16, 173);
            this.DatabaseConnectBtn.Margin = new System.Windows.Forms.Padding(3, 15, 18, 3);
            this.DatabaseConnectBtn.Name = "DatabaseConnectBtn";
            this.DatabaseConnectBtn.Size = new System.Drawing.Size(160, 23);
            this.DatabaseConnectBtn.TabIndex = 1;
            this.DatabaseConnectBtn.Text = "Database Connect";
            this.DatabaseConnectBtn.UseVisualStyleBackColor = true;
            this.DatabaseConnectBtn.Click += new System.EventHandler(this.DatabaseConnectBtn_Click);
            // 
            // DatabaseDisconnectBtn
            // 
            this.DatabaseDisconnectBtn.Enabled = false;
            this.DatabaseDisconnectBtn.Location = new System.Drawing.Point(182, 173);
            this.DatabaseDisconnectBtn.Margin = new System.Windows.Forms.Padding(3, 15, 18, 3);
            this.DatabaseDisconnectBtn.Name = "DatabaseDisconnectBtn";
            this.DatabaseDisconnectBtn.Size = new System.Drawing.Size(160, 23);
            this.DatabaseDisconnectBtn.TabIndex = 0;
            this.DatabaseDisconnectBtn.Text = "Database Disconnect";
            this.DatabaseDisconnectBtn.UseVisualStyleBackColor = true;
            this.DatabaseDisconnectBtn.Click += new System.EventHandler(this.DatabaseDisconnectBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 722);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 611);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "AUT Application Board Control";
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KiTuningBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KpTuningBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetTempBox)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label LoggingInPrgTxt;
        private System.Windows.Forms.Button StartLoggingBtn;
        private System.Windows.Forms.Button StopLoggingBtn;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox ManualDataEntTxtBox;
        private System.Windows.Forms.Button InsrtManualDataBtn;
        private System.Windows.Forms.TextBox MotorSpdTxtBox;
        private System.Windows.Forms.TextBox TempReadingTxtBox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown KiTuningBox;
        private System.Windows.Forms.NumericUpDown KpTuningBox;
        private System.Windows.Forms.NumericUpDown SetTempBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox LightPercentOn;
        private System.Windows.Forms.VScrollBar LightScrollAdj;
        private AquaControls.AquaGauge LightGauge;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private AquaControls.AquaGauge Pot2Gauge;
        private AquaControls.AquaGauge Pot1Gauge;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label Pa0;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label Pa7;
        private System.Windows.Forms.Label Pa6;
        private System.Windows.Forms.Label Pa5;
        private System.Windows.Forms.Label Pa4;
        private System.Windows.Forms.Label Pa3;
        private System.Windows.Forms.Label Pa2;
        private System.Windows.Forms.Label Pa1;
        private Bulb.LedBulb LedPa7;
        private Bulb.LedBulb LedPa6;
        private Bulb.LedBulb LedPa5;
        private Bulb.LedBulb LedPa4;
        private Bulb.LedBulb LedPa3;
        private Bulb.LedBulb LedPa2;
        private Bulb.LedBulb LedPa1;
        private Bulb.LedBulb LedPa0;
        private System.Windows.Forms.GroupBox groupBox4;
        private DmitryBrant.CustomControls.SevenSegment RightSevenSegment;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.CheckBox Pc7;
        private DmitryBrant.CustomControls.SevenSegment LeftSevenSegment;
        private System.Windows.Forms.CheckBox Pc1;
        private System.Windows.Forms.CheckBox Pc6;
        private System.Windows.Forms.CheckBox Pc0;
        private System.Windows.Forms.CheckBox Pc5;
        private System.Windows.Forms.CheckBox Pc2;
        private System.Windows.Forms.CheckBox Pc4;
        private System.Windows.Forms.CheckBox Pc3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Bulb.LedBulb SerialStatusLed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SerialConnectBtn;
        private System.Windows.Forms.Button SerialDisconnectBtn;
        private System.Windows.Forms.ComboBox ComPortBox;
        private System.Windows.Forms.ComboBox BaudBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private Bulb.LedBulb DatabaseStatusLed;
        private System.Windows.Forms.ComboBox ServerNameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.TextBox DatabaseBox;
        private System.Windows.Forms.Button DatabaseConnectBtn;
        private System.Windows.Forms.Button DatabaseDisconnectBtn;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

