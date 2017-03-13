using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp.Outgauge
{
    abstract class OutGaugeObserver
    {
        protected OutgaugeThread outgaugeThread;
        public abstract void update();
    }
}
