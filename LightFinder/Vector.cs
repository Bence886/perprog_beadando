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
        private Point direction;
        public Point Direction
        {
            get
            {
                return direction;
            }

            set
            {
                if (Point.CompFloat(value.Lenght(), 1.0f, 0.0001f))
                {
                    direction = value;
                }
                else
                {
                    throw new  FormatException("Not a Unit Vector!");
                }
            }
        }
        public int Length { get; set; }

        public Vector(Point p, Point d)
        {
            Location = p;
            Direction = d;
            Length = 1;
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
            Location.X /= b;
            Location.Y /= b;
            Location.Z /= b;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.Location - b.Location, a.Direction - b.Direction);
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
            Point u = v0.Direction, v = v1.Direction;
            return (u.X * v.X + u.Y * v.Y + u.Z * v.Z);
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