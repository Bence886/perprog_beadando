using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Point : IEquatable<Point>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public void MultiplyByLambda(float a)
        {
            X *= a;
            Y *= a;
            Z *= a;
        }

        public static float DotProduct(Point u, Point v)
        {
            return (u.X * v.X + u.Y * v.Y + u.Z * v.Z);
        }

        public void DevideByLambda(float a)
        {
            X /= a;
            Y /= a;
            Z /= a;
        }

        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}, Z:{2}", X, Y, Z);
        }

        public void Normalize()
        {
            float d = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            if (d != 0)
            {
                DevideByLambda(Math.Abs(d));
            }
        }

        public static Point GetMiddlePoint(Point a, Point b)
        {
            Point midle;
            midle = a + b;
            midle.DevideByLambda(2);
            midle.Normalize();

            return midle;
        }

        public static float InnerProduct(Point v, Point q)
        {//http://www.lighthouse3d.com/tutorials/maths/inner-product/
            return v.X * q.X + v.Y * q.Y + v.Z * q.Z;
        }

        public string ToFile()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return string.Format("({0}, {1}, {2}),", X.ToString(nfi), Y.ToString(nfi), Z.ToString(nfi));
        }

        public override bool Equals(object obj)
        {
            Point o = (Point)obj;
            float epsilon = 0.0001f;
            return CompFloat(X, o.X, epsilon)
                && CompFloat(Y, o.Y, epsilon)
                && CompFloat(Z, o.Z, epsilon);
        }

        public static bool CompFloat(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static Point CrossProduct(Point b, Point c)
        {//http://www.lighthouse3d.com/tutorials/maths/vector-cross-product/
            return new Point(b.Y * c.Z - b.Z * c.Y, b.Z * c.X - b.X * c.Z, b.X * c.Y - b.Y * c.X);

        }

        public static float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z));
        }

        public float Lenght()
        {
            return Distance(new Point(0, 0, 0), this);
        }

        public bool Equals(Point o)
        {
            float epsilon = 0.001f;
            return Point.CompFloat(X, o.X, epsilon)
                && Point.CompFloat(Y, o.Y, epsilon)
                && Point.CompFloat(Z, o.Z, epsilon);
        }

        static Random rnd = new Random();
        public static Point GeneratePointOnHalfSphere(Triangle hitTriangle, bool backfacing)
        {
            Point normal = hitTriangle.Normal;
            if (backfacing)
            {
                normal.MultiplyByLambda(-1);
            }
            Point direction = Point.CrossProduct(normal, hitTriangle.P1 - hitTriangle.P0);
            direction.Normalize();
            Point cross = Point.CrossProduct(normal, direction);

            float x, y, z;
            x = (float)rnd.NextDouble() * (1 - (-1)) + (-1);
            y = (float)rnd.NextDouble() * (1 - (-1)) + (-1);
            z = (float)rnd.NextDouble();

            Point randomPoint = new Point(
            x * direction.X + y * cross.X + z * normal.X,
            x * direction.Y + y * cross.Y + z * normal.Y,
            x * direction.Z + y * cross.Z + z * normal.Z);

            randomPoint.Normalize();
            return randomPoint;
        }

        public static Point GeneratePointOnSphere(Point origin)
        {
            Point randomPoint = new Point((float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1));
            while (randomPoint.Lenght() > 1)
            {
                randomPoint = new Point((float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1));
            }
            randomPoint.Normalize();
            return randomPoint;
        }
    }
}
