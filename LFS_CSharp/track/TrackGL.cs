using InSimDotNet.Out;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LFS_CSharp.track
{
    class TrackGL : GameWindow
    {
        OutSim outsim;
        int pgmId;
        int vsId;
        int fsId;
        int attribute_vpos;
        int attribute_vcol;
        int uniform_mview;
        int vbo_position;
        int vbo_color;
        int vbo_mview;
        int ibo_elements;
        Vector3[] vertData;
        Vector3[] colData;
        int[] indiceData;
        float time = 0.0f;
        Volume track;
        Camera cam = new Camera();
        Vector2 lastMousePos = new Vector2();
        
        SMX_GL smx;

        void initProgram()
        {
            //lastMousePos = new Vector2(Mouse.X, Mouse.Y);

            // Create Program to be used by OpenGL.
            pgmId = GL.CreateProgram();

            // Load, compile and Specify what shaders to use
            loadShader("./resources/shaders/vs.glsl", ShaderType.VertexShader, pgmId, out vsId);
            loadShader("./resources/shaders/fs.glsl", ShaderType.FragmentShader, pgmId, out fsId);

            // Make link with OpenGL program
            GL.LinkProgram(pgmId);
            Console.WriteLine(GL.GetProgramInfoLog(pgmId));

            // Get Attributes @ from differents shaders
            attribute_vpos = GL.GetAttribLocation(pgmId, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmId, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmId, "modelview");

            // Generate buffers for attributes
            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);

            // Generate buffers for indices to have better performance in OpenGL and reuse same vertices
            GL.GenBuffers(1, out ibo_elements);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "LFS Viewer";
            Console.WriteLine("OnLoad");
            initProgram();
            cam.Position = new Vector3(850.0f, -480.0f, 1.0f);
            GL.ClearColor(Color.Black);
            GL.PointSize(5f);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == 27)
            {
                Exit();
            }

            switch (e.KeyChar)
            {
                case 'z':
                    cam.Move(0f, 1f, 0f);
                    break;
                case 'q':
                    cam.Move(-1f, 0f, 0f);
                    break;
                case 's':
                    cam.Move(0f, -1f, 0f);
                    break;
                case 'd':
                    cam.Move(1f, 0f, 0f);
                    break;
                case 'a':
                    cam.Move(0f, 0f, 1f);
                    break;
                case 'e':
                    cam.Move(0f, 0f, -1f);
                    break;
            }
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        }

        void ResetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            lastMousePos = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> cols = new List<Vector3>();

            int vertCount = 0;

            verts.AddRange(track.GetVerts().ToList());
            cols.AddRange(track.GetColorData().ToList());
            inds.AddRange(track.GetIndices().ToList());
            vertCount += track.VertCount;
            

            vertData = verts.ToArray();
            colData = cols.ToArray();
            indiceData = inds.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertData.Length * Vector3.SizeInBytes), vertData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(colData.Length * Vector3.SizeInBytes), colData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

            time += (float)e.Time;

            //car.Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
            track.Scale = new Vector3(0.1f, 0.1f, 0.1f);

            track.CalculateModelMatrix();
            track.ViewProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(1.3f, ClientSize.Width / (float)ClientSize.Height, 1.0f, 40.0f);
            track.ModelViewProjectionMatrix = track.ModelMatrix * track.ViewProjectionMatrix;

            GL.UseProgram(pgmId);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indiceData.Length * sizeof(int)), indiceData, BufferUsageHint.StaticDraw);

            if (Focused)
            {
                Vector2 delta = lastMousePos - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
                lastMousePos += delta;

                cam.AddRotation(delta.X, delta.Y);
                ResetCursor();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            // Set viewport to Width & Height
            GL.Viewport(0, 0, Width, Height);

            // Clear all screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Enable Graphic Cards to be used
            GL.Enable(EnableCap.DepthTest);

            // Enable 
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            int indiceAt = 0;

            GL.UniformMatrix4(uniform_mview, false, ref track.ModelViewProjectionMatrix);
            GL.DrawElements(BeginMode.Triangles, track.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));

            indiceAt += track.IndiceCount;

            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcol);

            GL.Flush();
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        void loadShader(String filename, ShaderType type, int program, out int address)
        {

            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public TrackGL(int width, int height)
            : base(width, height, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4))
        {
            track = new Track("Aston");
            track.Position = new Vector3(0.3f, -0.5f, -3.0f);
            /*TrackParserGL parser = new TrackParserGL("AU1");
            if ((smx = parser.ParseSMXTrack()) != null)
            {
                Console.WriteLine("Track correctly parsed!");
            }*/
            Thread thread = new Thread(new ThreadStart(CallOutsimThread));
            thread.Start();
        }

        private void CallOutsimThread()
        {
            try
            {
                Console.WriteLine("Outsim Thread Started!");
                outsim = new OutSim();
                bool first = true;
                Vector2 firstPos = new Vector2(0.0f, 0.0f);
                outsim.PacketReceived += (sender, e) =>
                {
                    if (first) firstPos = new Vector2((float)e.Pos.X / 65535, (float)e.Pos.Y / 65535);
                    first = false;
                    //car.Position.Y = (float)e.Pos.Y / 65535;
                    //car.Position.X = (float)e.Pos.X / 65535;
                    //car.Position.Z = -12.0f;
                    //car.Position -= new Vector3(firstPos);
                    //Console.WriteLine("{0} - {1}", car.Position.X, car.Position.Y);
                };
                outsim.Connect("127.0.0.1", 29966);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
