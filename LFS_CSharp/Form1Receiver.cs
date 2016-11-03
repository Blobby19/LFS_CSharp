using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class Form1Receiver : OutGaugeObserver
    {

        public Form1Receiver(OutgaugeThread outgaugeThread)
        {
            this.outgaugeThread = outgaugeThread;
            this.outgaugeThread.attach(this);
        }

        public override void update()
        {
            Form1.updateUI(this.outgaugeThread.getValues());
            Form1.ShowValues(this.outgaugeThread.getRpm(), 
                this.outgaugeThread.getThrottle(), 
                this.outgaugeThread.getBrakes(), 
                this.outgaugeThread.getClutch(),
                this.outgaugeThread.getSpeed(),
                this.outgaugeThread.getTime());
        }
    }
}
