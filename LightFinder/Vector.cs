using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Vector
    {
        public Point Location { get; set; }
        public Point Direction { get; set; }
        public int Length { get; set; }

        public Vector(Point p, Point d)
        {
            Location = p;
            Direction = d;
        }

        public Point GetEndPoint()
        {
            Point ret = Direction;
            ret.MultiplyByLambda(Length);
            ret += Location;
            return ret;
        }

        public void DevideByLambda(float b)
        {
            Location.x /= b;
            Location.y /= b;
            Location.z /= b;
        }

        public static Vector operator-(Vector a, Vector b)
        {
            return new Vector(a.Location - b.Location, a.Direction-b.Direction);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.Location + b.Location, a.Direction + b.Direction);
        }

        public static Point CrossProduct(Vector v0, Vector v1)
        {
            Point u = v1.Direction;
            Point v = v1.Direction;

            Point p = Point.CrossProduct(u, v);

            return p;
        }

        public static float DotProduct(Vector v0, Vector v1)
        {
            Point u=v0.Direction, v=v1.Direction;
            return (u.x * v.x + u.y * v.y + u.z * v.z);
        }

        public void MultiplyByLambda(float f)
        {
            Direction.MultiplyByLambda(f);
        }

        public override string ToString()
        {
            return string.Format("Location:{0}, Direction.{1}, Length:{2}", Location.ToString(), Direction.ToString(), Length);
        }
    }
}