using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;

namespace LFS_CSharp.track
{
    class Track : Volume
    {

        private List<Vector3> objectVertices;
        private List<Vector3> objectColors;
        private List<int> objectIndices;

        public Track(String filename)
        {
            objectVertices = new List<Vector3>();
            objectColors = new List<Vector3>();
            objectIndices = new List<int>();
            readTrackFromFile(filename);
        }

        private void readTrackFromFile(String filename)
        {
            FileStream sr = new FileStream("./resources/smx/"+filename+"_3DH.smx", FileMode.Open, FileAccess.Read);
            byte[] data;
            try
            {
                int length = (int)sr.Length;
                long nb_Points = 0;
                long nb_Indices = 0;
                data = new byte[length];
                SMX_GL smx = new SMX_GL();
                using(BinaryReader bin = new BinaryReader(sr))
                {
                    smx.LFSSMX = new String(bin.ReadChars(6));
                    smx.version = bin.ReadByte();
                    smx.revision = bin.ReadByte();
                    smx.smx_version = bin.ReadByte();
                    smx.dimensions = bin.ReadByte();
                    smx.resolution = bin.ReadByte();
                    smx.vertex_colors = bin.ReadByte();
                    smx.empty = bin.ReadBytes(4);
                    smx.track = new String(bin.ReadChars(32));
                    smx.groundColR = bin.ReadByte();
                    smx.groundColG = bin.ReadByte();
                    smx.groundColB = bin.ReadByte();
                    smx.empty2 = bin.ReadBytes(9);
                    smx.numObjects = bin.ReadInt32();
                    int offsetForTriangle = 0;
                    smx.smxObjectsList = new ObjectBlockSmx[smx.numObjects];
                    for (int i = 0; i < smx.numObjects; i++)
                    {
                        ObjectBlockSmx _smxObject = new ObjectBlockSmx();
                        _smxObject.center = new OpenTK.Vector3(bin.ReadInt32(), bin.ReadInt32(), bin.ReadInt32());
                        Console.WriteLine("X: {0}, Y: {1}, Z: {2}", _smxObject.center.X/65536, _smxObject.center.Y / 65536, _smxObject.center.Z / 65536);
                        _smxObject.radius = bin.ReadInt32();
                        _smxObject.numPoints = bin.ReadInt32();
                        _smxObject.smxPointsList = new PointBlockSmx[_smxObject.numPoints];
                        _smxObject.numTriangles = bin.ReadInt32();
                        _smxObject.smxTrianglesList = new TriangleBlockSmx[_smxObject.numTriangles];
                        long offset_1 = bin.BaseStream.Position;
                        for (int j = 0; j < _smxObject.numPoints; j++)
                        {
                            PointBlockSmx _smxPoint = new PointBlockSmx();
                            _smxPoint.point = new OpenTK.Vector3((float)bin.ReadInt32() / 65536, (float)bin.ReadInt32() / 65536, (float)bin.ReadInt32() / 65536);
                            _smxPoint.color = bin.ReadInt32();
                            _smxObject.smxPointsList[j] = _smxPoint;
                            objectVertices.Add(_smxPoint.point);
                            int intValue;
                            byte[] intBytes = BitConverter.GetBytes(_smxPoint.color);
                            Array.Reverse(intBytes);
                            byte[] result = intBytes;
                            objectColors.Add(new Vector3(result[1], result[2], result[3]));
                            nb_Points++;
                        }
                        for (int k = 0; k < _smxObject.numTriangles; k++)
                        {
                            TriangleBlockSmx _smxTriangle = new TriangleBlockSmx();
                            _smxTriangle.vertexA = bin.ReadUInt16();
                            _smxTriangle.vertexB = bin.ReadUInt16();
                            _smxTriangle.vertexC = bin.ReadUInt16();
                            _smxTriangle.empty = bin.ReadUInt16();
                            offset_1 = bin.BaseStream.Position;
                            _smxObject.smxTrianglesList[k] = _smxTriangle;
                            objectIndices.Add(_smxTriangle.vertexA+offsetForTriangle);
                            objectIndices.Add(_smxTriangle.vertexB+offsetForTriangle);
                            objectIndices.Add(_smxTriangle.vertexC+offsetForTriangle);
                            nb_Indices++;
                        }
                        offsetForTriangle += _smxObject.numPoints;
                        smx.smxObjectsList[i] = _smxObject;
                    }
                    FooterBlockSmx _smxFooter = new FooterBlockSmx();
                    _smxFooter.numCpObjects = bin.ReadInt32();
                    _smxFooter.objectIndex = new int[_smxFooter.numCpObjects];
                    for (int i = 0; i < _smxFooter.numCpObjects; i++)
                    {
                        _smxFooter.objectIndex[i] = bin.ReadInt32();
                    }
                }

                IndiceCount = (int)nb_Indices;
                VertCount = (int)nb_Points;
                ColorDataCount = (int)nb_Points;
            }
            catch(IOException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                throw ex;
            }
            catch(NotSupportedException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                throw ex;
            }
            finally
            {
                sr.Close();
            }
            
        }

        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.Scale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }

        public override Vector3[] GetColorData()
        {
            return objectColors.ToArray();
        }

        public override int[] GetIndices(int offset = 0)
        {
            return objectIndices.ToArray();
        }

        public override Vector3[] GetVerts()
        {
            return objectVertices.ToArray();
        }
    }
}
