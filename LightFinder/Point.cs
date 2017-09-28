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
    }
}