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

namespace LFS_CSharp
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
        Volume car;
        //Camera cam = new Camera();
        //Vector2 lastMousePos = new Vector2();
        
        SMX_GL smx;

        void initProgram()
        {
            //lastMousePos = new Vector2(Mouse.X, Mouse.Y);

            pgmId = GL.CreateProgram();

            loadShader("./resources/shaders/vs.glsl", ShaderType.VertexShader, pgmId, out vsId);
            loadShader("./resources/shaders/fs.glsl", ShaderType.FragmentShader, pgmId, out fsId);

            GL.LinkProgram(pgmId);
            Console.WriteLine(GL.GetProgramInfoLog(pgmId));

            attribute_vpos = GL.GetAttribLocation(pgmId, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmId, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmId, "modelview");

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);

            GL.GenBuffers(1, out ibo_elements);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "LFS Viewer";
            Console.WriteLine("OnLoad");
            initProgram();
            GL.ClearColor(Color.Black);
            GL.PointSize(5f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> cols = new List<Vector3>();

            int vertCount = 0;

            verts.AddRange(car.GetVerts().ToList());
            cols.AddRange(car.GetColorData().ToList());
            inds.AddRange(car.GetIndices().ToList());
            vertCount += car.VertCount;
            

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
            car.Scale = new Vector3(0.1f, 0.1f, 0.1f);

            car.CalculateModelMatrix();
            car.ViewProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(1.3f, ClientSize.Width / (float)ClientSize.Height, 1.0f, 40.0f);
            car.ModelViewProjectionMatrix = car.ModelMatrix * car.ViewProjectionMatrix;

            GL.UseProgram(pgmId);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indiceData.Length * sizeof(int)), indiceData, BufferUsageHint.StaticDraw);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            int indiceAt = 0;

            GL.UniformMatrix4(uniform_mview, false, ref car.ModelViewProjectionMatrix);
            GL.DrawElements(BeginMode.Triangles, car.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));

            indiceAt += car.IndiceCount;

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
            car = new Cube();
            car.Position = new Vector3(0.3f, -0.5f, -3.0f);
            TrackParserGL parser = new TrackParserGL("AS1");
            if ((smx = parser.ParseSMXTrack()) != null)
            {
                Console.WriteLine("Track correctly parsed!");
            }
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
                    car.Position.Y = (float)e.Pos.Y / 65535;
                    car.Position.X = (float)e.Pos.X / 65535;
                    car.Position.Z = -12.0f;
                    car.Position -= new Vector3(firstPos);
                    Console.WriteLine("{0} - {1}", car.Position.X, car.Position.Y);
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
