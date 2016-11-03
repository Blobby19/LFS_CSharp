using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp.track
{
    public class PTH
    {
        String LFSPTH;
        byte version;
        byte revision;
        int numNodes;
        int finishLine;
        public int minimumCenterX = int.MaxValue;
        public int minimumCenterY = int.MaxValue;
        public int minimumCenterZ = int.MaxValue;
        public int maximumCenterX = int.MinValue;
        public int maximumCenterY = int.MinValue;
        public int maximumCenterZ = int.MinValue;
        PTH_Nodes[] nodes;
        
        public String GetLFSPTH()
        {
            return this.LFSPTH;
        }

        public void SetLFSPTH(char[] LFSPTH)
        {
            this.LFSPTH = new String(LFSPTH);
        }

        public byte GetVersion()
        {
            return this.version;
        }

        public void SetVersion(byte version)
        {
            this.version = version;
        }

        public byte GetRevision()
        {
            return this.revision;
        }

        public void SetRevision(byte revision)
        {
            this.revision = revision;
        }

        public int GetNumNodes()
        {
            return this.numNodes;
        }

        public void SetNumNodes(int numNodes)
        {
            this.nodes = new PTH_Nodes[numNodes];
            this.numNodes = numNodes;
        }

        public int GetFinishLine()
        {
            return this.finishLine;
        }

        public void SetFinishLine(int finishLine)
        {
            this.finishLine = finishLine;
        }

        public PTH_Nodes[] GetPTHNodes()
        {
            return this.nodes;
        }

        public void SetPTHNodes (PTH_Nodes[] nodes)
        {
            this.nodes = nodes;
        }

        public void addPTHNodes(PTH_Nodes node, int i)
        {
            this.nodes[i] = node;
        }
    }
    
    public class PTH_Nodes
    {
        int centerX;
        int centerY;
        int centerZ;
        float dirX;
        float dirY;
        float dirZ;
        float limitLeft;
        float limitRight;
        float driveLeft;
        float driveRight;

        public PTH_Nodes()
        {

        }

        public PTH_Nodes(int centerX, int centerY, int centerZ, 
            float dirX, float dirY, float dirZ, 
            float limitLeft, float limitRight, 
            float driveLeft, float driveRight)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.centerZ = centerZ;
            this.dirX = dirX;
            this.dirY = dirY;
            this.dirZ = dirZ;
            this.limitLeft = limitLeft;
            this.limitRight = limitRight;
            this.driveLeft = driveLeft;
            this.driveRight = driveRight;
        }

        public int GetCenterX()
        {
            return this.centerX;
        }

        public void SetCenterX(int centerX)
        {
            this.centerX = centerX;
        }

        public int GetCenterY()
        {
            return this.centerY;
        }

        public void SetCenterY(int centerY)
        {
            this.centerY = centerY;
        }

        public int GetCenterZ()
        {
            return this.centerZ;
        }

        public void SetCenterZ(int centerZ)
        {
            this.centerZ = centerZ;
        }

        public float GetDirX()
        {
            return this.dirX;
        }

        public void SetDirX(float dirX)
        {
            this.dirX = dirX;
        }

        public float GetDirY()
        {
            return this.dirY;
        }

        public void SetDirY(float dirY)
        {
            this.dirY = dirY;
        }

        public float GetDirZ()
        {
            return this.dirZ;
        }

        public void SetDirZ(float dirZ)
        {
            this.dirZ = dirZ;
        }

        public float GetLimitLeft()
        {
            return this.limitLeft;
        }

        public void SetLimitLeft(float limitLeft)
        {
            this.limitLeft = limitLeft;
        }

        public float GetLimitRight()
        {
            return this.limitRight;
        }

        public void SetLimitRight(float limitRight)
        {
            this.limitRight = limitRight;
        }

        public float GetDriveLeft()
        {
            return this.driveLeft;
        }

        public void SetDriveLeft(float driveLeft)
        {
            this.driveLeft = driveLeft;
        }

        public float GetDriveRight()
        {
            return this.driveRight;
        }

        public void SetDriveRight(float driveRight)
        {
            this.driveRight = driveRight;
        }
    }
}
