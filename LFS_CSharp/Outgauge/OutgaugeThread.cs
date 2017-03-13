using InSimDotNet.Out;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LFS_CSharp.Outgauge
{
    class OutgaugeThread
    {

        public Thread _outgaugeThread;
        private OutGauge _outgauge;
        private double[,] values = new double[6, 250];
        private float rpm;
        private double throttle, brake, clutch, speed;
        private TimeSpan time;
        private DashLightFlags dashlights, showlights;
        private OutGaugeFlags flags;
        private string car;
        private string display1, display2;
        private float engtemp, fuel, oilpressure, oiltemp, turbo;
        private OutGaugePack packet;
        private int id;
        private byte gear, plid;
        private bool _abort = false;
        private List<OutGaugeObserver> observers = new List<OutGaugeObserver>();

        public OutgaugeThread()
        {
            _outgauge = new OutGauge();
            _outgauge.PacketReceived += _outgauge_PacketReceived;
            _outgaugeThread = new Thread(new ThreadStart(MainThread));
            Console.WriteLine(_outgaugeThread.Name);
            Console.WriteLine("OutgaugeThread created!");
        }

        public double[,] getValues()
        {
            return this.values;
        }

        public double getRpm()
        {
            return this.rpm;
        }

        public double getSpeed()
        {
            return this.speed;
        }

        public double getClutch()
        {
            return this.clutch;
        }

        public double getThrottle()
        {
            return this.throttle;
        }

        public double getBrakes()
        {
            return this.brake;
        }

        public TimeSpan getTime()
        {
            return this.time;
        }

        public string getCar()
        {
            return this.car;
        }

        public string getDisplay1()
        {
            return this.display1;
        }

        public string getDisplay2()
        {
            return this.display2;
        }

        public DashLightFlags getDashLights()
        {
            return this.dashlights;
        }

        public float getEngTemp()
        {
            return this.engtemp;
        }

        public float getOilPressure()
        {
            return this.oilpressure;
        }

        public float getOilTemp()
        {
            return this.oiltemp;
        }

        public OutGaugeFlags getFlags()
        {
            return this.flags;
        }

        public float getFuel()
        {
            return this.fuel;
        }

        public byte getGear()
        {
            return this.gear;
        }

        public int getId()
        {
            return this.id;
        }

        public OutGaugePack getPacket()
        {
            return this.packet;
        }

        public byte getPlid()
        {
            return this.plid;
        }

        public DashLightFlags getShowLights()
        {
            return this.showlights;
        }

        public float getTurbo()
        {
            return this.turbo;
        }

        public void attach(OutGaugeObserver observer)
        {
            this.observers.Add(observer);
        }

        public void notifyAllObservers()
        {
            foreach(OutGaugeObserver observer in observers)
            {
                observer.update();
            }
        }

        public void Start()
        {
            if (_abort) _abort = false;
            try
            {
                if(!_outgaugeThread.IsAlive)
                    _outgaugeThread.Start();
            }
            catch(ThreadStartException ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("OutgaugeThread started!");
        }

        public void Abort()
        {
            if (!_abort) _abort = true;
            try
            {
                _outgauge.Disconnect();
                _outgaugeThread.Abort();
                _outgaugeThread.Join();
            }
            catch(ThreadAbortException ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            finally
            {
                _outgauge.Disconnect();
            }
        }

        private void MainThread()
        {
            if (_outgauge.IsConnected) _outgauge.Connect("127.0.0.1", 29967);
            else Console.WriteLine("connected!");
        }

        private void _outgauge_PacketReceived(object sender, OutGaugeEventArgs e)
        {
            //Form1.mutexOutgauge.WaitOne();
            values[0, values.GetLength(1) - 1] = Math.Truncate(e.Speed * 36) / 10;
            values[1, values.GetLength(1) - 1] = e.RPM;
            values[2, values.GetLength(1) - 1] = e.Throttle * 100;
            values[3, values.GetLength(1) - 1] = e.Brake * 100;
            values[4, values.GetLength(1) - 1] = e.Clutch * 100;
            values[5, values.GetLength(1) - 1] = e.Turbo;
            //Form1.mutexOutgauge.ReleaseMutex();
            speed = Math.Truncate(e.Speed * 36) / 10;
            brake = e.Brake * 100;
            clutch = e.Clutch * 100;
            throttle = e.Throttle * 100;
            rpm = e.RPM;
            time = e.Time;
            car = e.Car;
            dashlights = e.DashLights;
            display1 = e.Display1;
            display2 = e.Display2;
            engtemp = e.EngTemp;
            flags = e.Flags;
            fuel = e.Fuel;
            gear = e.Gear;
            id = e.ID;
            oilpressure = e.OilPressure;
            oiltemp = e.OilTemp;
            packet = e.Packet;
            plid = e.PLID;
            showlights = e.ShowLights;
            turbo = e.Turbo;
            notifyAllObservers();
            Array.Copy(values, 1, values, 0, values.Length - 1);            
        }
    }
}
