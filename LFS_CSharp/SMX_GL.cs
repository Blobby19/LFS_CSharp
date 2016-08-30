using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class SMX_GL
    {

        public String LFSSMX;
        public byte version;
        public byte revision;
        public byte smx_version;
        public byte dimensions;
        public byte resolution;
        public byte vertex_colors;
        public byte[] empty;
        public String track;
        public byte groundColR;
        public byte groundColG;
        public byte groundColB;
        public byte[] empty2;
        public int numObjects;
        public ObjectBlockSmx[] smxObjectsList;
    }

    class ObjectBlockSmx
    {
        public Vector3 center;
        public int radius;
        public int numPoints;
        public int numTriangles;
        public PointBlockSmx[] smxPointsList;
        public TriangleBlockSmx[] smxTrianglesList;
    }

    class PointBlockSmx
    {
        public Vector3 point;
        public int color;
    }

    class TriangleBlockSmx
    {
        public UInt16 vertexA;
        public UInt16 vertexB;
        public UInt16 vertexC;
        public UInt16 empty;
    }

    class FooterBlockSmx
    {
        public int numCpObjects;
        public int[] objectIndex;
    }
}
