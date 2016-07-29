using InSimDotNet.Out;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFS_CSharp
{
    public partial class TrackViewer : Form
    {

        private Graphics g;
        private PointF[] posCar = new PointF[20];
        private int height;
        private int width;
        private Thread outsimThread;
        private PTH pth;

        static void Main2(object state)
        {
            Application.Run((Form)state);
        }

        public static TrackViewer Create()
        {
            TrackViewer form = new TrackViewer();
            Thread thread = new Thread(Main2);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(form);
            return form;
        }

        public TrackViewer()
        {
            InitializeComponent();
        }

        private void btn_open_track_Click(object sender, EventArgs e)
        {
            byte[] data;
            FileStream reader = new FileStream("H:\\LFS\\data\\smx\\AS4.pth", FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)reader.Length;
                data = new byte[length];
                int count, sum=0;
                pth = new PTH();

                //while ((count = reader.Read(data, 0, length)) > 0) ;

                using(BinaryReader binReader = new BinaryReader(reader))
                {
                    pth.SetLFSPTH(binReader.ReadChars(6));
                    pth.SetVersion(binReader.ReadByte());
                    pth.SetRevision(binReader.ReadByte());
                    pth.SetNumNodes(binReader.ReadInt32());
                    pth.SetFinishLine(binReader.ReadInt32());
                    for(int i = 0; i < pth.GetNumNodes(); i++)
                    {
                        PTH_Nodes node = new PTH_Nodes();
                        node.SetCenterX(binReader.ReadInt32());
                        if (node.GetCenterX() < pth.minimumCenterX) pth.minimumCenterX = node.GetCenterX();
                        if (node.GetCenterX() > pth.maximumCenterX) pth.maximumCenterX = node.GetCenterX();
                        node.SetCenterY(binReader.ReadInt32());
                        if (node.GetCenterY() < pth.minimumCenterY) pth.minimumCenterY = node.GetCenterY();
                        if (node.GetCenterY() > pth.maximumCenterY) pth.maximumCenterY = node.GetCenterY();
                        node.SetCenterZ(binReader.ReadInt32());
                        if (node.GetCenterZ() < pth.minimumCenterZ) pth.minimumCenterZ = node.GetCenterZ();
                        if (node.GetCenterZ() > pth.maximumCenterZ) pth.maximumCenterZ = node.GetCenterZ();
                        node.SetDirX(binReader.ReadSingle());
                        node.SetDirY(binReader.ReadSingle());
                        node.SetDirZ(binReader.ReadSingle());
                        node.SetLimitLeft(binReader.ReadSingle());
                        node.SetLimitRight(binReader.ReadSingle());
                        node.SetDriveLeft(binReader.ReadSingle());
                        node.SetDriveRight(binReader.ReadSingle());
                        pth.addPTHNodes(node, i);
                    }
                }
                drawTrack(pth);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void drawTrack(PTH track)
        {
            PointF[] points = new PointF[track.GetNumNodes()];
            for (int i = 0; i < track.GetNumNodes(); i++)
            {
                points[i] = new PointF(convertToFitPictureBox(width, track.GetPTHNodes()[i].GetCenterY(), track.minimumCenterY, track.maximumCenterY), convertToFitPictureBox(height, track.GetPTHNodes()[i].GetCenterX(), track.minimumCenterX, track.maximumCenterX));
            }

            for(int i = 1; i < points.Length; i++)
            {
                g.DrawLine(Pens.Red, points[i-1], points[i]);
            }
            this.outsimThread = new Thread(new ThreadStart(CallOutsimThread));
            this.outsimThread.Start();
        }

        public float convertToFitPictureBox(int taille, int point, int minimum, int maximum)
        {
            int offset = -minimum;
            float retour = ((float)taille/(((float)maximum + (float)offset) - ((float)minimum + (float)offset)))* ((float)point+(float)offset);
            Console.WriteLine(retour);
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
            Console.WriteLine("Outsim Thread Started!");
            OutSim outsim = new OutSim();
            outsim.PacketReceived += (sender, e) =>
            {
                posCar[posCar.Length - 1] = new PointF(convertToFitPictureBox(width, (int)e.Pos.Y, pth.minimumCenterY, pth.maximumCenterY), convertToFitPictureBox(height, (int)e.Pos.X, pth.minimumCenterX, pth.maximumCenterX));
                Array.Copy(posCar, 1, posCar, 0, posCar.Length - 1);
                ShowOutsimValues(e.Pos.X, e.Pos.Y);
                
            };
            outsim.Connect("127.0.0.1", 29966);
        }

        delegate void ShowOutsimValuesDelegate(double posX, double posY);
        private void ShowOutsimValues(double posX, double posY)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ShowOutsimValuesDelegate(ShowOutsimValues), new object[] { posX, posY});
                }
                else
                {
                    updateCar();
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (outsimThread.IsAlive)
                    outsimThread.Abort();
            }
        }

        public void updateCar()
        {
            for(int i = 0; i < posCar.Length - 1; i++)
            {
                g.DrawRectangle(Pens.Black, posCar[i].X, posCar[i].Y, 5, 5);
            }
        }
    }
}
