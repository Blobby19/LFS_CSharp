using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp.Outsim
{
    abstract class OutSimObserver
    {
        protected OutSimThread outsimThread;
        public abstract void update();
    }
}
