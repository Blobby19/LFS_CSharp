using InSimDotNet.Out;
using LFS_External.InSim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vJoyInterfaceWrap;
using LFS_CSharp.track;

namespace LFS_CSharp
{
    public partial class Form1 : Form
    {

        public vJoy joystick;
        public vJoy.JoystickState iReport;
        //private Thread outgaugeThread;
        private OutgaugeThread _outgaugeThread;
        private Form1Receiver formReceiver;
        private static JoyController joyController;
        private static Form1 _form;
        public InSimInterface InSim;
        private double[,] ValsArray = new double[4, 250];
        public static Mutex mutexOutgauge;

        private LP lp;

        public Form1()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            _form = new Form1();
            joyController = JoyController.Create();
            Application.EnableVisualStyles();
            Application.Run(_form);
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (Form1.mutexOutgauge == null)
                Form1.mutexOutgauge = new Mutex();
            //if (this.outgaugeThread == null)
            if (_outgaugeThread == null)
            {
                this._outgaugeThread = new OutgaugeThread();
                this._outgaugeThread.Start();
                formReceiver = new Form1Receiver(this._outgaugeThread);
            }
        }
        

        #region Update View
        delegate void ShowValuesDelegate(double RPM,
            double Throttle,
            double Brakes,
            double Clutch,
            double Speed,
            TimeSpan Time);
        public static void ShowValues(double RPM,
            double Throttle,
            double Brakes,
            double Clutch,
            double Speed,
            TimeSpan Time)
        {
            try
            {
                if (!_form.IsDisposed)
                {
                    if (_form.InvokeRequired)
                        _form.Invoke(new ShowValuesDelegate(ShowValues), new object[] { RPM,
                    Throttle,
                    Brakes,
                    Clutch,
                    Speed,
                    Time});
                    else
                    {
                        _form.pgb_throttle.Value = (int)Throttle;
                        _form.pgb_brakes.Value = (int)Brakes;
                        _form.pgb_clutch.Value = (int)Clutch;
                        _form.lbl_RPM.Text = (Math.Truncate(RPM * 100) / 100).ToString();
                        _form.lbl_speed.Text = (Math.Truncate(Speed * 10) / 10).ToString();
                        _form.lbl_time.Text = Time.ToString();
                        //joyController.retrieveValues(RPM, Throttle, Brakes, Clutch, Speed, Time);
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public delegate void updateUIDelegate(double[,] array);
        public static void updateUI(double[,] array)
        {
            try
            {
                if (!_form.IsDisposed)
                {
                    if (_form.InvokeRequired)
                    {
                        _form.Invoke(new updateUIDelegate(updateUI), new object[] { array });
                    }
                    else
                    {
                        _form.speed_chart.Series[0].Points.Clear();
                        _form.accel_chart.Series[0].Points.Clear();
                        _form.brake_chart.Series[0].Points.Clear();
                        _form.rpm_chart.Series[0].Points.Clear();
                        for (int i = 0; i < array.GetLength(1) - 1; i++)
                        {
                            _form.speed_chart.Series[0].Points.AddY(array[0, i]);
                            _form.rpm_chart.Series[0].Points.AddY(array[1, i]);
                            _form.accel_chart.Series[0].Points.AddY(array[2, i]);
                            _form.brake_chart.Series[0].Points.AddY(array[3, i]);
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_outgaugeThread != null)
                _outgaugeThread.Abort();
        }
    

        private void updateChart()
        {
            speed_chart.Series["Speed"].Points.Clear();
            rpm_chart.Series["RPM"].Points.Clear();
            accel_chart.Series["Accel"].Points.Clear();
            brake_chart.Series["Brake"].Points.Clear();
            for (int i = 0; i < ValsArray.GetLength(1) - 1; i++)
            {
                speed_chart.Series["Speed"].Points.AddY(ValsArray[0, i]);
                rpm_chart.Series["RPM"].Points.AddY(ValsArray[1, i]);
                accel_chart.Series["Accel"].Points.AddY(ValsArray[2, i]);
                brake_chart.Series["Brake"].Points.AddY(ValsArray[3, i]);
            }
        }
        
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lp.kp = (double)num_kp_accel.Value;
            lp.computeConstants();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            lp.ki = (double)num_ki_accel.Value / 1000;
            lp.computeConstants();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            lp.kd = (double)num_kd_accel.Value / 10;
            lp.computeConstants();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            lp.setpoint = (double)num_accel_consigne.Value;
        }

        private void btn_track_viewer_Click(object sender, EventArgs e)
        {
            TrackViewer trackViewer = TrackViewer.Create();
            //TrackGL gl = new TrackGL(512, 512);
            //gl.Run();
        }
    }
}

