using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class TrackParserGL
    {
        private String _shortName;
        private PTH_GL _pthTrack = null;

        public TrackParserGL(String shortName)
        {
            this._shortName = shortName;
        }

        public PTH_GL ParseTrack()
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

    }
}
