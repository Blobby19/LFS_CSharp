namespace LFS_CSharp
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btn_connect = new System.Windows.Forms.Button();
            this.pgb_throttle = new System.Windows.Forms.ProgressBar();
            this.pgb_brakes = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_RPM = new System.Windows.Forms.Label();
            this.lbl_speed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pgb_clutch = new System.Windows.Forms.ProgressBar();
            this.lbl_time = new System.Windows.Forms.Label();
            this.btn_openVJoyControl = new System.Windows.Forms.Button();
            this.speed_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.num_kp_accel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_ki_accel = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.num_kd_accel = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.num_accel_consigne = new System.Windows.Forms.NumericUpDown();
            this.gpb_acceleration_looppoint = new System.Windows.Forms.GroupBox();
            this.gpb_freinage_looppoint = new System.Windows.Forms.GroupBox();
            this.num_kp_frein = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.num_frein_consigne = new System.Windows.Forms.NumericUpDown();
            this.num_ki_frein = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.num_kd_frein = new System.Windows.Forms.NumericUpDown();
            this.rpm_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.accel_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.brake_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_track_viewer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.speed_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kp_accel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ki_accel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kd_accel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_accel_consigne)).BeginInit();
            this.gpb_acceleration_looppoint.SuspendLayout();
            this.gpb_freinage_looppoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_kp_frein)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_frein_consigne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ki_frein)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kd_frein)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpm_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accel_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brake_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(197, 12);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // pgb_throttle
            // 
            this.pgb_throttle.Location = new System.Drawing.Point(12, 53);
            this.pgb_throttle.MarqueeAnimationSpeed = 0;
            this.pgb_throttle.Name = "pgb_throttle";
            this.pgb_throttle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pgb_throttle.Size = new System.Drawing.Size(260, 22);
            this.pgb_throttle.TabIndex = 1;
            // 
            // pgb_brakes
            // 
            this.pgb_brakes.Location = new System.Drawing.Point(12, 81);
            this.pgb_brakes.MarqueeAnimationSpeed = 0;
            this.pgb_brakes.Name = "pgb_brakes";
            this.pgb_brakes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pgb_brakes.Size = new System.Drawing.Size(260, 22);
            this.pgb_brakes.Step = 0;
            this.pgb_brakes.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "RPM";
            // 
            // lbl_RPM
            // 
            this.lbl_RPM.Location = new System.Drawing.Point(49, 134);
            this.lbl_RPM.Name = "lbl_RPM";
            this.lbl_RPM.Size = new System.Drawing.Size(50, 13);
            this.lbl_RPM.TabIndex = 4;
            this.lbl_RPM.Text = "RPM";
            // 
            // lbl_speed
            // 
            this.lbl_speed.Location = new System.Drawing.Point(149, 134);
            this.lbl_speed.Name = "lbl_speed";
            this.lbl_speed.Size = new System.Drawing.Size(50, 13);
            this.lbl_speed.TabIndex = 6;
            this.lbl_speed.Text = "Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Speed";
            // 
            // pgb_clutch
            // 
            this.pgb_clutch.Location = new System.Drawing.Point(12, 109);
            this.pgb_clutch.MarqueeAnimationSpeed = 0;
            this.pgb_clutch.Name = "pgb_clutch";
            this.pgb_clutch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pgb_clutch.Size = new System.Drawing.Size(260, 22);
            this.pgb_clutch.Step = 0;
            this.pgb_clutch.TabIndex = 7;
            // 
            // lbl_time
            // 
            this.lbl_time.Location = new System.Drawing.Point(12, 158);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(187, 13);
            this.lbl_time.TabIndex = 8;
            this.lbl_time.Text = "Time";
            // 
            // btn_openVJoyControl
            // 
            this.btn_openVJoyControl.Location = new System.Drawing.Point(108, 12);
            this.btn_openVJoyControl.Name = "btn_openVJoyControl";
            this.btn_openVJoyControl.Size = new System.Drawing.Size(75, 23);
            this.btn_openVJoyControl.TabIndex = 9;
            this.btn_openVJoyControl.Text = "Open vJoy";
            this.btn_openVJoyControl.UseVisualStyleBackColor = true;
            // 
            // speed_chart
            // 
            chartArea1.Name = "ChartArea1";
            this.speed_chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.speed_chart.Legends.Add(legend1);
            this.speed_chart.Location = new System.Drawing.Point(297, 12);
            this.speed_chart.Name = "speed_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            this.speed_chart.Series.Add(series1);
            this.speed_chart.Size = new System.Drawing.Size(647, 105);
            this.speed_chart.TabIndex = 10;
            this.speed_chart.Text = "speed_chart";
            // 
            // num_kp_accel
            // 
            this.num_kp_accel.Location = new System.Drawing.Point(50, 19);
            this.num_kp_accel.Name = "num_kp_accel";
            this.num_kp_accel.Size = new System.Drawing.Size(120, 20);
            this.num_kp_accel.TabIndex = 11;
            this.num_kp_accel.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.num_kp_accel.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Kp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Ki";
            // 
            // num_ki_accel
            // 
            this.num_ki_accel.Location = new System.Drawing.Point(50, 43);
            this.num_ki_accel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_ki_accel.Name = "num_ki_accel";
            this.num_ki_accel.Size = new System.Drawing.Size(120, 20);
            this.num_ki_accel.TabIndex = 13;
            this.num_ki_accel.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_ki_accel.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Kd";
            // 
            // num_kd_accel
            // 
            this.num_kd_accel.Location = new System.Drawing.Point(50, 65);
            this.num_kd_accel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_kd_accel.Name = "num_kd_accel";
            this.num_kd_accel.Size = new System.Drawing.Size(120, 20);
            this.num_kd_accel.TabIndex = 15;
            this.num_kd_accel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_kd_accel.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Consigne";
            // 
            // num_accel_consigne
            // 
            this.num_accel_consigne.Location = new System.Drawing.Point(230, 21);
            this.num_accel_consigne.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.num_accel_consigne.Name = "num_accel_consigne";
            this.num_accel_consigne.Size = new System.Drawing.Size(60, 20);
            this.num_accel_consigne.TabIndex = 17;
            this.num_accel_consigne.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.num_accel_consigne.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // gpb_acceleration_looppoint
            // 
            this.gpb_acceleration_looppoint.Controls.Add(this.num_kp_accel);
            this.gpb_acceleration_looppoint.Controls.Add(this.label6);
            this.gpb_acceleration_looppoint.Controls.Add(this.label2);
            this.gpb_acceleration_looppoint.Controls.Add(this.num_accel_consigne);
            this.gpb_acceleration_looppoint.Controls.Add(this.num_ki_accel);
            this.gpb_acceleration_looppoint.Controls.Add(this.label5);
            this.gpb_acceleration_looppoint.Controls.Add(this.label4);
            this.gpb_acceleration_looppoint.Controls.Add(this.num_kd_accel);
            this.gpb_acceleration_looppoint.Location = new System.Drawing.Point(1, 220);
            this.gpb_acceleration_looppoint.Name = "gpb_acceleration_looppoint";
            this.gpb_acceleration_looppoint.Size = new System.Drawing.Size(295, 97);
            this.gpb_acceleration_looppoint.TabIndex = 19;
            this.gpb_acceleration_looppoint.TabStop = false;
            this.gpb_acceleration_looppoint.Text = "LP Acceleration";
            // 
            // gpb_freinage_looppoint
            // 
            this.gpb_freinage_looppoint.Controls.Add(this.num_kp_frein);
            this.gpb_freinage_looppoint.Controls.Add(this.label7);
            this.gpb_freinage_looppoint.Controls.Add(this.label8);
            this.gpb_freinage_looppoint.Controls.Add(this.num_frein_consigne);
            this.gpb_freinage_looppoint.Controls.Add(this.num_ki_frein);
            this.gpb_freinage_looppoint.Controls.Add(this.label9);
            this.gpb_freinage_looppoint.Controls.Add(this.label10);
            this.gpb_freinage_looppoint.Controls.Add(this.num_kd_frein);
            this.gpb_freinage_looppoint.Location = new System.Drawing.Point(1, 323);
            this.gpb_freinage_looppoint.Name = "gpb_freinage_looppoint";
            this.gpb_freinage_looppoint.Size = new System.Drawing.Size(295, 97);
            this.gpb_freinage_looppoint.TabIndex = 20;
            this.gpb_freinage_looppoint.TabStop = false;
            this.gpb_freinage_looppoint.Text = "LP Freinage";
            // 
            // num_kp_frein
            // 
            this.num_kp_frein.Location = new System.Drawing.Point(50, 19);
            this.num_kp_frein.Name = "num_kp_frein";
            this.num_kp_frein.Size = new System.Drawing.Size(120, 20);
            this.num_kp_frein.TabIndex = 11;
            this.num_kp_frein.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Consigne";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Kp";
            // 
            // num_frein_consigne
            // 
            this.num_frein_consigne.Location = new System.Drawing.Point(230, 21);
            this.num_frein_consigne.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.num_frein_consigne.Name = "num_frein_consigne";
            this.num_frein_consigne.Size = new System.Drawing.Size(60, 20);
            this.num_frein_consigne.TabIndex = 17;
            this.num_frein_consigne.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // num_ki_frein
            // 
            this.num_ki_frein.Location = new System.Drawing.Point(50, 43);
            this.num_ki_frein.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_ki_frein.Name = "num_ki_frein";
            this.num_ki_frein.Size = new System.Drawing.Size(120, 20);
            this.num_ki_frein.TabIndex = 13;
            this.num_ki_frein.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Kd";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Ki";
            // 
            // num_kd_frein
            // 
            this.num_kd_frein.Location = new System.Drawing.Point(50, 65);
            this.num_kd_frein.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_kd_frein.Name = "num_kd_frein";
            this.num_kd_frein.Size = new System.Drawing.Size(120, 20);
            this.num_kd_frein.TabIndex = 15;
            this.num_kd_frein.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // rpm_chart
            // 
            chartArea2.Name = "ChartArea1";
            this.rpm_chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.rpm_chart.Legends.Add(legend2);
            this.rpm_chart.Location = new System.Drawing.Point(297, 123);
            this.rpm_chart.Name = "rpm_chart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            this.rpm_chart.Series.Add(series2);
            this.rpm_chart.Size = new System.Drawing.Size(647, 108);
            this.rpm_chart.TabIndex = 21;
            this.rpm_chart.Text = "rpm_chart";
            // 
            // accel_chart
            // 
            chartArea3.Name = "ChartArea1";
            this.accel_chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.accel_chart.Legends.Add(legend3);
            this.accel_chart.Location = new System.Drawing.Point(297, 237);
            this.accel_chart.Name = "accel_chart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "Accel";
            this.accel_chart.Series.Add(series3);
            this.accel_chart.Size = new System.Drawing.Size(647, 108);
            this.accel_chart.TabIndex = 22;
            this.accel_chart.Text = "accel_chart";
            // 
            // brake_chart
            // 
            chartArea4.Name = "ChartArea1";
            this.brake_chart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.brake_chart.Legends.Add(legend4);
            this.brake_chart.Location = new System.Drawing.Point(297, 346);
            this.brake_chart.Name = "brake_chart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Legend = "Legend1";
            series4.Name = "Brake";
            this.brake_chart.Series.Add(series4);
            this.brake_chart.Size = new System.Drawing.Size(647, 108);
            this.brake_chart.TabIndex = 23;
            this.brake_chart.Text = "brake_chart";
            // 
            // btn_track_viewer
            // 
            this.btn_track_viewer.Location = new System.Drawing.Point(18, 191);
            this.btn_track_viewer.Name = "btn_track_viewer";
            this.btn_track_viewer.Size = new System.Drawing.Size(75, 23);
            this.btn_track_viewer.TabIndex = 24;
            this.btn_track_viewer.Text = "Open Track Viewer";
            this.btn_track_viewer.UseVisualStyleBackColor = true;
            this.btn_track_viewer.Click += new System.EventHandler(this.btn_track_viewer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 470);
            this.Controls.Add(this.btn_track_viewer);
            this.Controls.Add(this.brake_chart);
            this.Controls.Add(this.accel_chart);
            this.Controls.Add(this.rpm_chart);
            this.Controls.Add(this.gpb_freinage_looppoint);
            this.Controls.Add(this.gpb_acceleration_looppoint);
            this.Controls.Add(this.speed_chart);
            this.Controls.Add(this.btn_openVJoyControl);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.pgb_clutch);
            this.Controls.Add(this.lbl_speed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_RPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgb_brakes);
            this.Controls.Add(this.pgb_throttle);
            this.Controls.Add(this.btn_connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.speed_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kp_accel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ki_accel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kd_accel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_accel_consigne)).EndInit();
            this.gpb_acceleration_looppoint.ResumeLayout(false);
            this.gpb_acceleration_looppoint.PerformLayout();
            this.gpb_freinage_looppoint.ResumeLayout(false);
            this.gpb_freinage_looppoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_kp_frein)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_frein_consigne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ki_frein)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_kd_frein)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpm_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accel_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brake_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.ProgressBar pgb_throttle;
        private System.Windows.Forms.ProgressBar pgb_brakes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_RPM;
        private System.Windows.Forms.Label lbl_speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pgb_clutch;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Button btn_openVJoyControl;
        private System.Windows.Forms.DataVisualization.Charting.Chart speed_chart;
        private System.Windows.Forms.NumericUpDown num_kp_accel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_ki_accel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown num_kd_accel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gpb_acceleration_looppoint;
        private System.Windows.Forms.GroupBox gpb_freinage_looppoint;
        private System.Windows.Forms.NumericUpDown num_kp_frein;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_frein_consigne;
        private System.Windows.Forms.NumericUpDown num_ki_frein;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown num_kd_frein;
        private System.Windows.Forms.DataVisualization.Charting.Chart rpm_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart accel_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart brake_chart;
        private System.Windows.Forms.Button btn_track_viewer;
        public System.Windows.Forms.NumericUpDown num_accel_consigne;
    }
}

