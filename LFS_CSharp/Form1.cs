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

namespace LFS_CSharp
{
    public partial class Form1 : Form
    {

        public vJoy joystick;
        public vJoy.JoystickState iReport;
        //private Thread outgaugeThread;
        private OutgaugeThread _outgaugeThread;
        private static Form1 _form;
        public InSimInterface InSim;
        private double[,] ValsArray = new double[4, 250];
        public static Mutex mutexOutgauge;
        private int gear;

        private LP lp;

        private DateTime dt = new DateTime();

        public Form1()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            _form = new Form1();
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
            }
        }

        private void CallVJoyFeeder()
        {

            // Create one joystick object and a position structure.
            joystick = new vJoy();
            iReport = new vJoy.JoystickState();
            if (Form1.mutexOutgauge == null) Form1.mutexOutgauge = new Mutex();
            lp = new LFS_CSharp.LP(10, 5, 5);
            lp.computeConstants();
            uint id = 1;

            if (id <= 0 || id > 16)
            {
                Console.WriteLine("Illegal device ID {0}\nExit!", id);
                return;
            }

            // Get the driver attributes (Vendor ID, Product ID, Version Number)
            if (!joystick.vJoyEnabled())
            {
                Console.WriteLine("vJoy driver not enabled: Failed Getting vJoy attributes.\n");
                return;
            }
            else
                Console.WriteLine("Vendor: {0}\nProduct :{1}\nVersion Number:{2}\n", joystick.GetvJoyManufacturerString(), joystick.GetvJoyProductString(), joystick.GetvJoySerialNumberString());

            // Get the state of the requested device
            VjdStat status = joystick.GetVJDStatus(id);
            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                    Console.WriteLine("vJoy Device {0} is already owned by this feeder\n", id);
                    break;
                case VjdStat.VJD_STAT_FREE:
                    Console.WriteLine("vJoy Device {0} is free\n", id);
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    Console.WriteLine("vJoy Device {0} is already owned by another feeder\nCannot continue\n", id);
                    return;
                case VjdStat.VJD_STAT_MISS:
                    Console.WriteLine("vJoy Device {0} is not installed or disabled\nCannot continue\n", id);
                    return;
                default:
                    Console.WriteLine("vJoy Device {0} general error\nCannot continue\n", id);
                    return;
            };

            // Check which axes are supported
            bool AxisX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X);
            bool AxisY = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y);
            bool AxisZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z);
            bool AxisRX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RX);
            bool AxisRZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RZ);
            // Get the number of buttons and POV Hat switchessupported by this vJoy device
            int nButtons = joystick.GetVJDButtonNumber(id);
            int ContPovNumber = joystick.GetVJDContPovNumber(id);
            int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);

            // Print results
            Console.WriteLine("\nvJoy Device {0} capabilities:\n", id);
            Console.WriteLine("Numner of buttons\t\t{0}\n", nButtons);
            Console.WriteLine("Numner of Continuous POVs\t{0}\n", ContPovNumber);
            Console.WriteLine("Numner of Descrete POVs\t\t{0}\n", DiscPovNumber);
            Console.WriteLine("Axis X\t\t{0}\n", AxisX ? "Yes" : "No");
            Console.WriteLine("Axis Y\t\t{0}\n", AxisX ? "Yes" : "No");
            Console.WriteLine("Axis Z\t\t{0}\n", AxisX ? "Yes" : "No");
            Console.WriteLine("Axis Rx\t\t{0}\n", AxisRX ? "Yes" : "No");
            Console.WriteLine("Axis Rz\t\t{0}\n", AxisRZ ? "Yes" : "No");

            // Test if DLL matches the driver
            UInt32 DllVer = 0, DrvVer = 0;
            bool match = joystick.DriverMatch(ref DllVer, ref DrvVer);
            if (match)
                Console.WriteLine("Version of Driver Matches DLL Version ({0:X})\n", DllVer);
            else
                Console.WriteLine("Version of Driver ({0:X}) does NOT match DLL Version ({1:X})\n", DrvVer, DllVer);


            // Acquire the target
            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!joystick.AcquireVJD(id))))
            {
                Console.WriteLine("Failed to acquire vJoy device number {0}.\n", id);
                return;
            }
            else
                Console.WriteLine("Acquired: vJoy device number {0}.\n", id);

            int X, Y, Z, ZR, XR;
            uint count = 0;
            long maxval = 0;

            X = 20;
            Y = 30;
            Z = 40;
            XR = 60;
            ZR = 80;

            joystick.GetVJDAxisMax(id, HID_USAGES.HID_USAGE_X, ref maxval);

            bool res;
            // Reset this device to default values
            joystick.ResetVJD(id);

            // Feed the device in endless loop
            while (true)
            {
                if (gear == 1)
                {
                    lp.setpoint = -100;
                }
                else if (gear == 0)
                {
                    lp.setpoint = 10;
                }
                else
                {
                    lp.setpoint = (double)num_accel_consigne.Value;
                }
                lp.direct = false;
                //this.mutex.WaitOne();
                lp.execute();
                // Set position of 4 axes
                res = joystick.SetAxis((int)adapteAxis(lp._out, maxval), id, HID_USAGES.HID_USAGE_X);
                //this.mutex.ReleaseMutex();

                System.Threading.Thread.Sleep(20);
                X += 150; if (X > maxval) X = 0;
                count++;

                if (count > 640)
                    count = 0;
            }
        }

        private double adapteAxis(double rawValue, long maxval)
        {
            return (((maxval - maxval / 2) / 100) * rawValue) + maxval / 2;
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

        private void btn_openVJoyControl_Click(object sender, EventArgs e)
        {
            /*if (VJoyForm == null || !VJoyForm.IsHandleCreated)
                VJoyForm = vJoyForm.Create();*/
            Thread vjoycontrolThread = new Thread(new ThreadStart(CallVJoyFeeder));
            vjoycontrolThread.Start();
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
            //TrackViewer trackViewer = TrackViewer.Create();
            TrackGL gl = new TrackGL(1024, 768);
        }
    }
}

