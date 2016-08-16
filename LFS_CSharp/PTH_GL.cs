using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LFS_CSharp
{
    public class PTH_GL
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
        PTH_Nodes_GL[] nodes;
        
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
            this.nodes = new PTH_Nodes_GL[numNodes];
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

        public PTH_Nodes_GL[] GetPTHNodes()
        {
            return this.nodes;
        }

        public void SetPTHNodes (PTH_Nodes_GL[] nodes)
        {
            this.nodes = nodes;
        }

        public void addPTHNodesGL(PTH_Nodes_GL node, int i)
        {
            this.nodes[i] = node;
        }
    }
    
    public class PTH_Nodes_GL
    {
        private Vector3 _center;
        private Vector3 _dir;
        private float _limitLeft;
        private float _limitRight;
        private float _driveLeft;
        private float _driveRight;

        public PTH_Nodes_GL() { 
            
        }

        public PTH_Nodes_GL(Vector3 center, Vector3 dir, float limitLeft, float limitRight, float driveLeft, float driveRight)
        {
            this._center = center;
            this._dir = dir;
            this._limitLeft = limitLeft;
            this._limitRight = limitRight;
            this._driveLeft = driveLeft;
            this._driveRight = driveRight;
        }

        public Vector3 GetCenter()
        {
            return this._center;
        }

        public void SetCenter(Vector3 center)
        {
            this._center = center;
        }

        public Vector3 GetDir()
        {
            return this._dir;
        }

        public void SetDir(Vector3 dir)
        {
            this._dir = dir;
        }

        public float GetLimitLeft()
        {
            return this._limitLeft;
        }

        public void SetLimitLeft(float limitLeft)
        {
            this._limitLeft = limitLeft;
        }

        public float GetLimitRight()
        {
            return this._limitRight;
        }

        public void SetLimitRight(float limitRight)
        {
            this._limitRight = limitRight;
        }

        public float GetDriveLeft()
        {
            return this._driveLeft;
        }

        public void SetDriveLeft(float driveLeft)
        {
            this._driveLeft = driveLeft;
        }

        public float GetDriveRight()
        {
            return this._driveRight;
        }

        public void SetDriveRight(float driveRight)
        {
            this._driveRight = driveRight;
        }
    }
}
