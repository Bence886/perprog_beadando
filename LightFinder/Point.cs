using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}, Z:{2}", x, y, z);
        }

        public string ToFile()
        {
            return string.Format("({0}% {1}% {2})%", x, y, z);
        }
    }
}