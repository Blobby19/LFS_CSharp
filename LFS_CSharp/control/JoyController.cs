using LFS_CSharp.Outgauge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace LFS_CSharp
{
    class JoyController : OutGaugeObserver
    {

        public Thread _joyThread;
        public Thread _update;
        private Mutex _mutex;
        private bool _abort = false;
        private vJoy joystick;
        private vJoy.JoystickState iReport;
        private OutgaugeThread outgaugeThread;
        private LP lp;
        private uint id;
        private double throttle;
        private double rpm;
        private double brakes;
        private double clutch;
        private double speed;

        public JoyController (OutgaugeThread outgaugeThread)
        {
            this.outgaugeThread = outgaugeThread;
            outgaugeThread.attach(this);
            initializeJoystick();
        }

        public JoyController()
        {
            try
            {
                initializeJoystick();
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            _joyThread = new Thread(new ThreadStart(MainThread));
            _update = new Thread(new ThreadStart(update));
        }

        private void initializeJoystick()
        {
            joystick = new vJoy();
            iReport = new vJoy.JoystickState();
            lp = new LP(10, 5, 5);
            lp.computeConstants();
            id = 1;

            if(id<=0 || id > 16)
            {
                Console.WriteLine("Illegal device ID {0}\nExit!", id);
                throw new Exception("Illegal device ID\nExit!");
            }

            if (!joystick.vJoyEnabled())
            {
                Console.WriteLine("vJoy driver not enabled: Failed Getting vJoy attributes.\n");
                throw new Exception("vJoy driver not enabled: Failed Getting vJoy attributes.\n");
            }
            else Console.WriteLine("Vendor: {0} - Product :{1} - Version Number:{2}\n", joystick.GetvJoyManufacturerString(), joystick.GetvJoyProductString(), joystick.GetvJoySerialNumberString());

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
            }

            bool AxisX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X);
            bool AxisY = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y);
            bool AxisZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z);
            int nButtons = joystick.GetVJDButtonNumber(id);
            int ContPovNumber = joystick.GetVJDContPovNumber(id);
            int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);

            Console.WriteLine("\nvJoy Device {0} capabilities:", id);
            Console.WriteLine("Numner of buttons - {0}", nButtons);
            Console.WriteLine("Numner of Continuous POVs - {0}", ContPovNumber);
            Console.WriteLine("Numner of Descrete POVs - {0}", DiscPovNumber);
            Console.WriteLine("Axis X - {0}", AxisX ? "Yes" : "No");
            Console.WriteLine("Axis Y - {0}", AxisX ? "Yes" : "No");
            Console.WriteLine("Axis Z - {0}", AxisX ? "Yes" : "No");

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
        }

        private void MainThread()
        {
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
            // Reset this device to def
            joystick.ResetVJD(id);
            
            lp.setpoint = 30;
            lp.direct = false;

            lp.execute();
            // Set position of 4 axes
            res = joystick.SetAxis((int)adapteAxis(lp._out, maxval), id, HID_USAGES.HID_USAGE_X);
                
            X += 150; if (X > maxval) X = 0;
            count++;

            if (count > 640)
                count = 0;
        }

        public void setKp(double kp)
        {
            lp.kp = kp;
        }

        public void setConsigne(double consigne)
        {
            lp.setpoint = consigne;
        }

        public void refresh()
        {
            lp.computeConstants();
        }

        private double adapteAxis(double rawValue, long maxval)
        {
            return (((maxval - maxval / 2) / 100) * rawValue) + maxval / 2;
        }

        public override void update()
        {
            this.throttle = outgaugeThread.getThrottle();
            this.speed = outgaugeThread.getSpeed();
            lp.vitesse = this.speed;
            MainThread();
        }
    }
}
