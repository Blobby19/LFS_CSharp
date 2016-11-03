using InSimDotNet.Out;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class OutSimThread
    {
        public Thread _outsimThread;
        private OutSim _outsim;
        private PointF[] posCar = new PointF[20];
        private InSimDotNet.Vector accel, anglevelocity, vel;
        private InSimDotNet.Vec pos;
        private float heading, pitch, roll;
        private int id;
        private TimeSpan time;
        private OutSimPack packet;
        private List<OutSimObserver> observers = new List<OutSimObserver>();

        public OutSimThread()
        {
            Console.WriteLine("Outsim Thread Started!");
            _outsim = new OutSim();
            _outsim.PacketReceived += _outsim_PacketReceived;
            _outsimThread = new Thread(new ThreadStart(MainThread));
            Console.WriteLine(_outsimThread.Name);
            Console.WriteLine("OutsimThread created!");
        }

        public InSimDotNet.Vector getAccel()
        {
            return this.accel;
        }

        public InSimDotNet.Vector getAngVel()
        {
            return this.anglevelocity;
        }

        public InSimDotNet.Vector getVel()
        {
            return this.vel;
        }

        public InSimDotNet.Vec getPos()
        {
            return this.pos;
        }

        public float getHeading()
        {
            return this.heading;
        }

        public float getPitch()
        {
            return this.pitch;
        }

        public float getRoll()
        {
            return this.roll;
        }

        public int getId()
        {
            return this.id;
        } 

        public TimeSpan getTime()
        {
            return this.time;
        }

        public OutSimPack getPacket()
        {
            return this.packet;
        }

        public void attach(OutSimObserver observer)
        {
            this.observers.Add(observer);
        }

        public void notifyAllObservers()
        {
            foreach(OutSimObserver observer in observers)
            {
                observer.update();
            }
        }

        public void Start()
        {
            try
            {
                if (!_outsimThread.IsAlive)
                    _outsimThread.Start();
            }
            catch (ThreadStartException ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("OutgaugeThread started!");
        }

        public void Abort()
        {
            try
            {
                _outsim.Disconnect();
                _outsimThread.Abort();
                _outsimThread.Join();
            }
            catch (ThreadAbortException ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            finally
            {
                _outsim.Disconnect();
            }
        }

        private void MainThread()
        {
            if (_outsim.IsConnected) _outsim.Connect("127.0.0.1", 29966);
            else Console.WriteLine("connected!");
        }

        private void _outsim_PacketReceived(object sender, OutSimEventArgs e)
        {
            accel = e.Accel;
            anglevelocity = e.AngVel;
            heading = e.Heading;
            id = e.ID;
            packet = e.Packet;
            pitch = e.Pitch;
            pos = e.Pos;
            roll = e.Roll;
            time = e.Time;
            vel = e.Vel;
            notifyAllObservers();
        }
    }
}
