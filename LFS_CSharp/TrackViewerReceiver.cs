using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class TrackViewerReceiver : OutSimObserver
    {

        private OutSimThread outsimThread;

        public TrackViewerReceiver(OutSimThread outsimThread)
        {
            this.outsimThread = outsimThread;
            this.outsimThread.attach(this);
        }
        public override void update()
        {
            TrackViewer.ShowOutsimValues(this.outsimThread.getPos().X, this.outsimThread.getPos().Y);
        }
    }
}
