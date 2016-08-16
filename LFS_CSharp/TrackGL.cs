using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class TrackGL : GameWindow
    {
        private PTH_GL pth;
        private float cameraSpeed = 5f;
        private Matrix4 cameraMatrix;
        private float[] mouseSpeed = new float[2];
        private bool first = true;
        private Vector3 offset;

        public TrackGL(int width, int height)
            : base(width, height)
        {
            TrackParserGL parser = new TrackParserGL("AS1");
            if((pth = parser.ParseTrack()) != null)
            {
                Console.WriteLine("Track correctly parsed!");
            }
            GL.Viewport(0, 0, width, height);
            Console.WriteLine("Constructor");
            this.Run();
        }

        protected override void OnLoad(EventArgs e)
        {
            Console.WriteLine("OnLoad");
            this.VSync = VSyncMode.Off;
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Console.WriteLine("OnUpdate: {0}", this.UpdateFrequency);
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(System.Drawing.Color.Black);
            GL.LoadIdentity();
            GL.LineWidth(0.5f);
            GL.Color3(0.5, 0.3, 0.1);
            GL.Begin(BeginMode.Lines);
            drawTrack();
            GL.End();
            this.SwapBuffers();
            Console.WriteLine("OnRenderFrame: {0}", this.RenderFrequency);
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        private void drawTrack()
        {
            foreach (PTH_Nodes_GL node in pth.GetPTHNodes())
            {
                if (first = !first)
                {
                    offset = node.GetCenter();
                }
                GL.Vertex3((node.GetCenter() - offset));
                //Console.WriteLine(node.GetCenter().X);
            }
        }

    }
}
