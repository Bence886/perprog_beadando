using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Vector
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Vector(Point s, Point e)
        {
            Start = s;
            End = e;
        }

        public void DevideByLambda(float b)
        {
            End.x /= b;
            End.y /= b;
            End.z /= b;
        }

        public float Length()
        {
            Point a = End - Start;
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

    }
}