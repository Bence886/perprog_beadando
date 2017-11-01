using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Triangle
    {
        public Triangle(Point p1, Point p2, Point p3)
        {
            this.P0 = p1;
            this.P1 = p2;
            this.P2 = p3;
            CalcNormal();
        }

        public Point P0 { get; set; }
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point Normal { get; set; }

        private void CalcNormal()
        {//https://math.stackexchange.com/questions/305642/how-to-find-surface-normal-of-a-triangle
            Point u = (P1 - P0);
            Point v = (P2 - P0);

            Normal = Point.CrossProduct(u, v);
            Normal.Normalize();
        }

        public Point InsideTringle(Vector ray)
        {//http://geomalgorithms.com/a06-_intersect-2.html
         //http://www.lighthouse3d.com/tutorials/maths/ray-triangle-intersection/
            Point e1, e2, h, s, q;
            float a, f, u, v;
            e1 = P1 - P0;
            e2 = P2 - P0;
            h = Point.CrossProduct(ray.Direction, e2);
            a = Point.InnerProduct(e1, h);

            if (a > -0.00001 && a < 0.00001)
            {
                throw new NoHit();
            }

            f = 1 / a;
            s = ray.Location - P0;
            u = f * (Point.InnerProduct(s, h));
            if (u < 0.0 || u > 1.0)
            {
                throw new NoHit();
            }

            q = Point.CrossProduct(s, e1);

            v = f * Point.InnerProduct(ray.Direction, q);
            if (v < 0.0 || u + v > 1.0)
            {
                throw new NoHit();
            }
            float t = f * Point.InnerProduct(e2, q);
            if (t > 0.00001)
            {
                return new Point(
                    ray.Location.X + ray.Direction.X * t,
                    ray.Location.Y + ray.Direction.Y * t,
                    ray.Location.Z + ray.Direction.Z * t);
            }
            throw new NoHit();
        }

        public override string ToString()
        {
            return string.Format("Triangle x:{0}, y:{1}, z:{2}",P0.ToString(), P1.ToString(), P2.ToString());
        }

        public static Triangle ClosestTriangleHit(List<Triangle> triengles, Vector ray)
        {
            Point closest = null;
            Triangle hitTriangle = null;
            foreach (Triangle item in triengles)
            {
                try
                {
                    Point hit = item.InsideTringle(ray);
                    if (closest == null || Point.Distance(ray.Location, hit) < Point.Distance(ray.Location, closest))
                    {
                        hitTriangle = item;
                        closest = hit;
                    }
                }
                catch (NoHit)
                {
                }
            }
            if (hitTriangle == null)
            {
                Log.WriteLog("No triangle hit.", LogType.Console, LogLevel.Trace);
                throw new NoHit();
            }
            Log.WriteLog(string.Format("Closest Trianglehit at: {0}", hitTriangle.ToString()), LogType.Console, LogLevel.Trace);
            return hitTriangle;
        }
    }
}
