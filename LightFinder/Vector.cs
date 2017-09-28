using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Vector
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Vector(Point s, Point e)
        {
            Start = s;
            End = e;
        }

        public Vector(Point a)
        {
            Start = new Point(0, 0, 0);
            End = a;
        }
        
        public void DevideByLambda(float b)
        {
            End.x /= b;
            End.y /= b;
            End.z /= b;
        }

        public static Vector operator-(Vector a, Vector b)
        {
            return new Vector(a.Start-b.Start, a.End-b.End);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.Start + b.Start, a.End + b.End);
        }

        public static Vector DotProduct(Vector v0, Vector v1)
        {
            Vector u, v;
            u = (v0.)
            Point p = new Point((u.y * v.z - u.z * v.y), (u.z * v.x - u.x * v.z), (u.x * v.z - u.y * v.x));

            Vector V = new Vector(new Point(0, 0, 0), p);
            V.DevideByLambda(V.Length());
            normal = V;
        }

        public float Length()
        {
            Point a = End - Start;
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }
    }
}