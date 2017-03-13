using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp.track
{
    class TrackParserGL
    {
        private String _shortName;
        private String _trackName;
        private PTH_GL _pthTrack = null;
        private SMX_GL _smxTrack = null;

        public TrackParserGL(String shortName)
        {
            this._shortName = shortName;
            switch(_shortName.Substring(0, 2))
            {
                case "AS":
                    this._trackName = "Aston";
                    break;
                case "KY":
                    this._trackName = "Kyoto";
                    break;
                case "FE":
                    this._trackName = "Fern Bay";
                    break;
                case "SO":
                    this._trackName = "South City";
                    break;
                case "WE":
                    this._trackName = "Westhill";
                    break;
                case "BL":
                    this._trackName = "Blackwood";
                    break;
                case "AU":
                    this._trackName = "Autocross";
                    break;
            }
        }

        public PTH_GL ParsePTHTrack()
        {
            FileStream reader = new FileStream("H:\\LFS\\data\\smx\\"+ this._shortName +".pth", FileMode.Open, FileAccess.Read);
            byte[] data;

            try
            {
                int length = (int)reader.Length;
                data = new byte[length];
                _pthTrack = new PTH_GL();
                using(BinaryReader binReader = new BinaryReader(reader))
                {
                    _pthTrack.SetLFSPTH(binReader.ReadChars(6));
                    _pthTrack.SetVersion(binReader.ReadByte());
                    _pthTrack.SetRevision(binReader.ReadByte());
                    _pthTrack.SetNumNodes(binReader.ReadInt32());
                    _pthTrack.SetFinishLine(binReader.ReadInt32());
                    for (int i = 0; i < _pthTrack.GetNumNodes(); i++)
                    {
                        PTH_Nodes_GL node = new PTH_Nodes_GL();
                        node.SetCenter(new OpenTK.Vector3(binReader.ReadInt32(), binReader.ReadInt32(), binReader.ReadInt32()));
                        node.SetDir(new OpenTK.Vector3(binReader.ReadSingle(), binReader.ReadSingle(), binReader.ReadSingle()));
                        node.SetLimitLeft(binReader.ReadSingle());
                        node.SetLimitRight(binReader.ReadSingle());
                        node.SetDriveLeft(binReader.ReadSingle());
                        node.SetDriveRight(binReader.ReadSingle());
                        _pthTrack.addPTHNodesGL(node, i);
                    }
                }
                return _pthTrack;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public SMX_GL ParseSMXTrack()
        {
            FileStream reader = new FileStream("./resources/smx/"+this._trackName+"_3DH.smx", FileMode.Open, FileAccess.Read);
            StreamWriter writer = new StreamWriter(@"C:\Users\Fedora\Documents\Developpement\LFS_track_converter\track_converter\" + this._trackName + "_3DH.obj");
            byte[] data;

            try
            {
                int length = (int)reader.Length;
                data = new byte[length];
                _smxTrack = new SMX_GL();
                using(BinaryReader bin = new BinaryReader(reader))
                {
                    _smxTrack.LFSSMX = new String(ReadChars(bin, 6));
                    _smxTrack.version = ReadByte(bin);
                    _smxTrack.revision = ReadByte(bin);
                    _smxTrack.smx_version = ReadByte(bin);
                    _smxTrack.dimensions = ReadByte(bin);
                    _smxTrack.resolution = ReadByte(bin);
                    _smxTrack.vertex_colors = ReadByte(bin);
                    _smxTrack.empty = ReadBytes(bin, 4);
                    _smxTrack.track = new String(ReadChars(bin, 32));
                    _smxTrack.groundColR = ReadByte(bin);
                    _smxTrack.groundColG = ReadByte(bin);
                    _smxTrack.groundColB = ReadByte(bin);
                    _smxTrack.empty2 = ReadBytes(bin, 9);
                    _smxTrack.numObjects = ReadInt(bin);
                    long offset = bin.BaseStream.Position;
                    int offsetForTriangle = 0;
                    _smxTrack.smxObjectsList = new ObjectBlockSmx[_smxTrack.numObjects];
                    for(int i = 0; i < _smxTrack.numObjects; i++)
                    {
                        ObjectBlockSmx _smxObject = new ObjectBlockSmx();
                        _smxObject.center = new OpenTK.Vector3(ReadInt(bin, offset), ReadInt(bin, offset), ReadInt(bin, offset));
                        _smxObject.radius = ReadInt(bin, offset);
                        _smxObject.numPoints = ReadInt(bin, offset);
                        _smxObject.smxPointsList = new PointBlockSmx[_smxObject.numPoints];
                        _smxObject.numTriangles = ReadInt(bin, offset);
                        _smxObject.smxTrianglesList = new TriangleBlockSmx[_smxObject.numTriangles];
                        long offset_1 = bin.BaseStream.Position;
                        writer.WriteLine("g object"+i);
                        for (int j = 0; j < _smxObject.numPoints; j++)
                        {
                            PointBlockSmx _smxPoint = new PointBlockSmx();
                            _smxPoint.point = new OpenTK.Vector3((float)ReadInt(bin, offset_1)/65536, (float)ReadInt(bin, offset_1)/65536, (float)ReadInt(bin, offset_1)/65536);
                            _smxPoint.color = ReadInt(bin, offset_1);
                            offset_1 = bin.BaseStream.Position;
                            _smxObject.smxPointsList[j] = _smxPoint;
                            writer.WriteLine("v " + _smxPoint.point.X + " " + _smxPoint.point.Y + " " + _smxPoint.point.Z);
                        }
                        for(int k = 0; k < _smxObject.numTriangles; k++)
                        {
                            TriangleBlockSmx _smxTriangle = new TriangleBlockSmx();
                            _smxTriangle.vertexA = ReadWord(bin, offset_1);
                            _smxTriangle.vertexB = ReadWord(bin, offset_1);
                            _smxTriangle.vertexC = ReadWord(bin, offset_1);
                            _smxTriangle.empty = ReadWord(bin, offset_1);
                            offset_1 = bin.BaseStream.Position;
                            _smxObject.smxTrianglesList[k] = _smxTriangle;
                            writer.WriteLine("f " + (_smxTriangle.vertexA+1+ offsetForTriangle) + " " + (_smxTriangle.vertexB+1+ offsetForTriangle) + " " + (_smxTriangle.vertexC+1+ offsetForTriangle));
                        }
                        offsetForTriangle += _smxObject.numPoints;
                        _smxTrack.smxObjectsList[i] = _smxObject;
                    }
                    writer.Close();
                    FooterBlockSmx _smxFooter = new FooterBlockSmx();
                    _smxFooter.numCpObjects = ReadInt(bin);
                    Console.WriteLine(_smxFooter.numCpObjects);
                    _smxFooter.objectIndex = new int[_smxFooter.numCpObjects];
                    for(int i = 0; i < _smxFooter.numCpObjects; i++)
                    {
                        _smxFooter.objectIndex[i] = ReadInt(bin);
                        Console.WriteLine(_smxFooter.objectIndex[i]);
                    }
                }

            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return _smxTrack;
        }

        private UInt16 ReadWord(BinaryReader bin, long offset = 0)
        {
            //Console.WriteLine("Position: {0}", bin.BaseStream.Position);
            return bin.ReadUInt16();
        }

        private char[] ReadChars(BinaryReader bin, int number, long offset = 0)
        {
            //Console.WriteLine("Position: {0}", bin.BaseStream.Position - offset);
            return bin.ReadChars(number);
        }

        private int ReadInt(BinaryReader bin, long offset = 0)
        {
            //Console.WriteLine("Position: {0}", bin.BaseStream.Position - offset);
            return bin.ReadInt32();
        }

        private byte[] ReadBytes(BinaryReader bin, int number, long offset = 0)
        {
            //Console.WriteLine("Position: {0}", bin.BaseStream.Position - offset);
            return bin.ReadBytes(number);
        }

        private byte ReadByte(BinaryReader bin, long offset = 0)
        {
            //Console.WriteLine("Position: {0}", bin.BaseStream.Position - offset);
            return bin.ReadByte();
        }

    }
}
