using InSimDotNet.Out;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LFS_CSharp.track;
namespace LFS_CSharp
{
    public partial class TrackViewer : Form
    {

        private Graphics g;
        private PointF[] posCar = new PointF[20];
        private int height;
        //OutSim outsim;
        private OutSimThread _outsimThread;
        private TrackViewerReceiver trackViewerReceiver;
        public static TrackViewer _trackViewer;
        private PointF lastPosition;
        private int width;
        //private Thread outsimThread;
        private PTH pth;
        private Pen penLimit;
        private Pen penRoad;
        private Pen penFinishLine;

        [STAThread]
        static void Main2(object state)
        {
            Application.Run((Form)state);
        }

        public static TrackViewer Create()
        {
            _trackViewer = new TrackViewer();
            Thread thread = new Thread(Main2);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(_trackViewer);
            return _trackViewer;
        }

        public TrackViewer()
        {
            InitializeComponent();
        }

        private void btn_open_track_Click(object sender, EventArgs e)
        {
            TrackParser trackParser = new TrackParser("AS4");
            if((pth = trackParser.ParsePTHTrack()) != null)
            {
                drawTrack(pth);
            }

        }

        public void drawTrack(PTH track)        
        {

            penLimit = new Pen(Color.Brown, 1.0f);
            penRoad = new Pen(Color.Orange, 1.0f);
            penFinishLine = new Pen(Color.Black, 2.0f);
            PointF[] points = new PointF[track.GetNumNodes()];
            PointF[] pointsDir = new PointF[track.GetNumNodes()];
            PointF[] pointsRoadLeft = new PointF[track.GetNumNodes()];
            PointF[] pointsRoadRight = new PointF[track.GetNumNodes()];
            PointF[] pointsLimitLeft = new PointF[track.GetNumNodes()];
            PointF[] pointsLimitRight = new PointF[track.GetNumNodes()];
            for (int i = 0; i < track.GetNumNodes(); i++)
            {
                pointsDir[i] = new PointF(track.GetPTHNodes()[i].GetDirY(), track.GetPTHNodes()[i].GetDirX());
                points[i] = new PointF(convertToFitPictureBox(width, track.GetPTHNodes()[i].GetCenterY(), track.minimumCenterY, track.maximumCenterY) +50, convertToFitPictureBox(height, track.GetPTHNodes()[i].GetCenterX(), track.minimumCenterX, track.maximumCenterX)+50);
                pointsRoadLeft[i] = computeDriveLeftPoint(points[i], track.GetPTHNodes()[i].GetDriveLeft(), pointsDir[i]);
                pointsRoadRight[i] = computeDriveRightPoint(points[i], track.GetPTHNodes()[i].GetDriveRight(), pointsDir[i]);
                pointsLimitLeft[i] = computeDriveLeftPoint(points[i], track.GetPTHNodes()[i].GetLimitLeft(), pointsDir[i]);
                pointsLimitRight[i] = computeDriveRightPoint(points[i], track.GetPTHNodes()[i].GetLimitRight(), pointsDir[i]);
            }

            for(int i = 0; i < points.Length-1; i++)
            {
                // Draw Physic Limit of the track
                g.DrawLine(Pens.Green, pointsLimitLeft[i], pointsLimitLeft[i + 1]);
                if (i == points.Length - 2) g.DrawLine(Pens.Green, pointsLimitLeft[i + 1], pointsLimitLeft[0]);
                g.DrawLine(Pens.Purple, pointsLimitRight[i], pointsLimitRight[i + 1]);
                if (i == points.Length - 2) g.DrawLine(Pens.Purple, pointsLimitRight[i + 1], pointsLimitRight[0]);
                g.DrawLine(penLimit, pointsLimitLeft[i], pointsLimitRight[i]);
                if (i == points.Length - 2) g.DrawLine(penLimit, pointsLimitLeft[i + 1], pointsLimitRight[i + 1]);

                g.DrawLine(Pens.Green, points[i].X, points[i].Y, points[i].X + pointsDir[i].X, points[i].Y + pointsDir[i].Y);
                Console.WriteLine("Longueur du vecteur tangantiel");
                Console.WriteLine(Math.Sqrt(Math.Pow(pointsDir[i].X - points[i].X, 2) + Math.Pow(pointsDir[i].Y - points[i].Y, 2)));

                VectorUtils.Equation med_a = VectorUtils.computeMediatrice(points[i], points[i + 1]);
                PointF vecDir = new PointF(points[i].X + pointsDir[i].X, points[i].Y + pointsDir[i].Y);
                PointF vecDir_ = new PointF(points[i+1].X + pointsDir[i+1].X, points[i+1].Y + pointsDir[i+1].Y);
                VectorUtils.Equation med_b = VectorUtils.computeMediatrice(vecDir, vecDir_);

                PointF intersection = VectorUtils.computeIntersection(med_a, med_b);
                double radius = VectorUtils.computeDistance(intersection, points[i]);
                double radians = VectorUtils.computeAngularDisplacement(radius, points[i], points[i + 1]);
                double degrees = VectorUtils.radiansToDegrees(radians);
                Console.WriteLine("Point: {0} - x: {1} y: {2}, PointDir: x: {3} y: {4}, Degrés: {5}", i, points[i].X, points[i].Y, points[i].X + pointsDir[i].X, points[i].Y + pointsDir[i].Y, degrees);


                if (i != track.GetFinishLine() && degrees <= 4.5f)
                {
                    //Draw Limit of the track
                    g.DrawLine(penRoad, pointsRoadLeft[i], pointsRoadRight[i]);
                    if (i == points.Length - 2) g.DrawLine(penRoad, pointsRoadLeft[i + 1], pointsRoadRight[i + 1]);
                    g.DrawLine(Pens.Red, points[i], points[i + 1]);
                    if (i == points.Length - 2) g.DrawLine(Pens.Red, points[i + 1], points[0]);
                }
                else
                {
                    //Draw Limit of the track
                    g.DrawLine(penFinishLine, pointsRoadLeft[i], pointsRoadRight[i]);
                    if (i == points.Length - 2) g.DrawLine(penRoad, pointsRoadLeft[i + 1], pointsRoadRight[i + 1]);
                    g.DrawLine(penFinishLine, points[i], points[i + 1]);
                    if (i == points.Length - 2) g.DrawLine(Pens.Red, points[i + 1], points[0]);
                }
            }
            this._outsimThread = new OutSimThread();
            this._outsimThread.Start();
            trackViewerReceiver = new TrackViewerReceiver(this._outsimThread);
            //this.outsimThread = new Thread(new ThreadStart(CallOutsimThread));
            //this.outsimThread.Start();
        }
        
        private PointF computeDriveLeftPoint(PointF center, float limitLeft, PointF dir)
        {
            PointF LimitLeftPoint = new PointF();
            LimitLeftPoint.X = (center.X - limitLeft * 0.5f * (float)Math.Cos(Math.Atan2(dir.X, dir.Y)));
            LimitLeftPoint.Y = (center.Y + limitLeft * 0.5f * (float)Math.Sin(Math.Atan2(dir.X, dir.Y)));
            return LimitLeftPoint;
        }

        private PointF computeDriveRightPoint(PointF center, float limitRight, PointF dir)
        {
            PointF LimitRightPoint = new PointF();
            LimitRightPoint.X = (center.X - limitRight * 0.5f * (float)Math.Cos(Math.Atan2(dir.X, dir.Y)));
            LimitRightPoint.Y = (center.Y + limitRight * 0.5f * (float)Math.Sin(Math.Atan2(dir.X, dir.Y)));
            return LimitRightPoint;
        }

        public float convertToFitPictureBox(int taille, int point, int minimum, int maximum)
        {
            int offset = -minimum;
            float retour = ((float)(taille-100)/(((float)maximum + (float)offset) - ((float)minimum + (float)offset)))* ((float)point+(float)offset);
            //Console.WriteLine(retour);
            return retour;
        }

        private void TrackViewer_Paint(object sender, PaintEventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            height = pictureBox1.Height;
            width = pictureBox1.Width;
        }

        private void CallOutsimThread()
        {
            try
            {
                Console.WriteLine("Outsim Thread Started!");
                //outsim = new OutSim();
                /*outsim.PacketReceived += (sender, e) =>
                {
                    posCar[posCar.Length - 1] = new PointF(convertToFitPictureBox(width, (int)e.Pos.Y, pth.minimumCenterY, pth.maximumCenterY), convertToFitPictureBox(height, (int)e.Pos.X, pth.minimumCenterX, pth.maximumCenterX));
                    Array.Copy(posCar, 1, posCar, 0, posCar.Length - 1);
                    ShowOutsimValues(e.Pos.X, e.Pos.Y);

                };*/
                //outsim.Connect("127.0.0.1", 29966);
            }
            catch(SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        delegate void ShowOutsimValuesDelegate(double posX, double posY);
        public static void ShowOutsimValues(double posX, double posY)
        {
            try
            {
                if (_trackViewer.InvokeRequired)
                {
                    _trackViewer.Invoke(new ShowOutsimValuesDelegate(ShowOutsimValues), new object[] { posX, posY});
                }
                else
                {
                    _trackViewer.posCar[_trackViewer.posCar.Length - 1] = new PointF(_trackViewer.convertToFitPictureBox(_trackViewer.width, (int)posY, _trackViewer.pth.minimumCenterY, _trackViewer.pth.maximumCenterY), _trackViewer.convertToFitPictureBox(_trackViewer.height, (int)posX, _trackViewer.pth.minimumCenterX, _trackViewer.pth.maximumCenterX));
                    Array.Copy(_trackViewer.posCar, 1, _trackViewer.posCar, 0, _trackViewer.posCar.Length - 1);
                    _trackViewer.updateCar();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateCar()
        {
            if (lastPosition.X != 0 && lastPosition.Y != 0) {
                g.DrawLine(Pens.Blue, posCar[posCar.Length - 1].X + 50, posCar[posCar.Length - 1].Y+50, lastPosition.X+50, lastPosition.Y+50);
            }
            lastPosition = posCar[posCar.Length - 1];
        }

        private void TrackViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
