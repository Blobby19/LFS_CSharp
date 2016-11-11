using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class VectorUtils
    {
        public static Equation computeMediatrice(PointF a, PointF b)
        {
            PointF midpoint = new PointF();
            midpoint.X = (a.X + b.X) / 2;
            midpoint.Y = (a.Y + b.Y) / 2;
            double slope = computeSlope(a, b);
            slope = computeNegativeReciprocal(slope);

            double offset = midpoint.Y - slope * midpoint.X;

            return new Equation(slope, offset);
        }

        public static double computeDistance(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static double computeSlope(PointF a, PointF b)
        {
            if ((b.X - a.X) == 0) return 0.0f;
            return (b.Y - a.Y) / (b.X - a.X);
        }

        public static double computeNegativeReciprocal(double slope)
        {
            if (0 == slope) return 0.0f;
            return -1 / slope;
        }

        public static PointF computeVector(Equation lineEquation, double x)
        {
            return new PointF((float)x, (float)(x * lineEquation.getSlope() + lineEquation.getOffset()));
        }

        public static PointF computeIntersection(Equation a_line, Equation b_line)
        {
            double x, y;
            x = (b_line.getOffset() - a_line.getOffset()) / (a_line.getSlope() - b_line.getSlope());
            y = a_line.getSlope() * x + a_line.getOffset();

            return new PointF((float)x, (float)y);
        }

        public static double computeAngularDisplacement(double radius, PointF a, PointF b)
        {
            double distance = VectorUtils.computeDistance(a, b);
            return distance / radius;
        }

        public static double radiansToDegrees(double radians)
        {
            return (radians * 180) / Math.PI;
        }

        public class Equation
        {

            private double slope, offset;

            public Equation()
            {
                this.slope = this.offset = 0;
            }

            public Equation(double slope, double offset)
            {
                this.slope = slope;
                this.offset = offset;
            }

            public double getSlope()
            {
                return slope;
            }

            public void setSlope(double slope)
            {
                this.slope = slope;
            }

            public double getOffset()
            {
                return offset;
            }

            public void setOffset(double offset)
            {
                this.offset = offset;
            }
        }

    }
}
