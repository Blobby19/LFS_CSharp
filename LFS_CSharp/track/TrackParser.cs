using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LFS_CSharp.track
{
    class TrackParser
    {
        private String _shortName;
        private PTH _pthTrack = null;

        public TrackParser(String shortName)
        {
            this._shortName = shortName;
        }

        public PTH ParsePTHTrack()
        {
            FileStream reader = new FileStream("H:\\LFS\\data\\smx\\"+ this._shortName +".pth", FileMode.Open, FileAccess.Read);
            byte[] data;

            try
            {
                int length = (int)reader.Length;
                data = new byte[length];
                _pthTrack = new PTH();
                using(BinaryReader binReader = new BinaryReader(reader))
                {
                    _pthTrack.SetLFSPTH(binReader.ReadChars(6));
                    _pthTrack.SetVersion(binReader.ReadByte());
                    _pthTrack.SetRevision(binReader.ReadByte());
                    _pthTrack.SetNumNodes(binReader.ReadInt32());
                    _pthTrack.SetFinishLine(binReader.ReadInt32());
                    for (int i = 0; i < _pthTrack.GetNumNodes(); i++)
                    {
                        PTH_Nodes node = new PTH_Nodes();
                        node.SetCenterX(binReader.ReadInt32());
                        if (node.GetCenterX() < _pthTrack.minimumCenterX) _pthTrack.minimumCenterX = node.GetCenterX();
                        if (node.GetCenterX() > _pthTrack.maximumCenterX) _pthTrack.maximumCenterX = node.GetCenterX();
                        node.SetCenterY(binReader.ReadInt32());
                        if (node.GetCenterY() < _pthTrack.minimumCenterY) _pthTrack.minimumCenterY = node.GetCenterY();
                        if (node.GetCenterY() > _pthTrack.maximumCenterY) _pthTrack.maximumCenterY = node.GetCenterY();
                        node.SetCenterZ(binReader.ReadInt32());
                        if (node.GetCenterZ() < _pthTrack.minimumCenterZ) _pthTrack.minimumCenterZ = node.GetCenterZ();
                        if (node.GetCenterZ() > _pthTrack.maximumCenterZ) _pthTrack.maximumCenterZ = node.GetCenterZ();
                        node.SetDirX(binReader.ReadSingle());
                        node.SetDirY(binReader.ReadSingle());
                        node.SetDirZ(binReader.ReadSingle());
                        node.SetLimitLeft(binReader.ReadSingle());
                        node.SetLimitRight(binReader.ReadSingle());
                        node.SetDriveLeft(binReader.ReadSingle());
                        node.SetDriveRight(binReader.ReadSingle());
                        _pthTrack.addPTHNodes(node, i);
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
