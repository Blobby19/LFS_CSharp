using InSimDotNet.Out;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace LFS_CSharp
{
    class OutgaugeThread
    {

        public Thread _outgaugeThread;
        private OutGauge _outgauge;
        private double[,] values = new double[6, 250];
        private bool _abort = false;

        public OutgaugeThread()
        {
            _outgauge = new OutGauge();
            _outgauge.PacketReceived += _outgauge_PacketReceived;
            _outgaugeThread = new Thread(new ThreadStart(MainThread));
            Console.WriteLine(_outgaugeThread.Name);
            Console.WriteLine("OutgaugeThread created!");
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
            if (_abort) return;
            Form1.mutexOutgauge.WaitOne();
            values[0, values.GetLength(1) - 1] = Math.Truncate(e.Speed * 36) / 10;
            values[1, values.GetLength(1) - 1] = e.RPM;
            values[2, values.GetLength(1) - 1] = e.Throttle * 100;
            values[3, values.GetLength(1) - 1] = e.Brake * 100;
            values[4, values.GetLength(1) - 1] = e.Clutch * 100;
            values[5, values.GetLength(1) - 1] = e.Turbo;
            Form1.mutexOutgauge.ReleaseMutex();
            Form1.ShowValues(e.RPM, e.Throttle * 100, e.Brake * 100, e.Clutch * 100, Math.Truncate(e.Speed * 36) / 10, e.Time);
            Array.Copy(values, 1, values, 0, values.Length - 1);
            Form1.updateUI(values);
        }
    }
}
