using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Point
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public void MultiplyByLambda(float a)
        {
            x *= a;
            y *= a;
            z *= a;
        }

        public void DevideByLambda(float a)
        {
            x /= a;
            y /= a;
            z /= a;
        }

        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}, Z:{2}", x, y, z);
        }

        public void normalize()
        {
            float d = (float)Math.Sqrt(x * x + y * y + z * z);
            if (d != 0)
            {
                DevideByLambda(d);
            }
        }

        public static Point GetMiddlePoint(Point a, Point b)
        {
            Point midle;
            midle = a + b;
            midle.DevideByLambda(2);
            midle.normalize();

            return midle;
        }

        public static float InnerProduct(Point v, Point q)
        {//http://www.lighthouse3d.com/tutorials/maths/inner-product/
            return v.x * q.x + v.y * q.y + v.z * q.z;
        }

        public string ToFile()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return string.Format("({0}, {1}, {2}),", x.ToString(nfi), y.ToString(nfi), z.ToString(nfi));
        }

        public override bool Equals(object obj)
        {
            Point o = (Point)obj;
            return x == o.x && y == o.y && z == o.z;
        }

        public static Point CrossProduct(Point b, Point c)
        {//http://www.lighthouse3d.com/tutorials/maths/vector-cross-product/
            return new Point(b.y * c.z - c.y * b.z, b.z * c.x - c.z * b.x, b.x * c.y - c.x * b.y);
        }
    }
}