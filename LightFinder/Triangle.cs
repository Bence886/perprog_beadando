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
            this.p0 = p1;
            this.p1 = p2;
            this.p2 = p3;
            CalcNormal();
        }

        public Point p0 { get; set; }
        public Point p1 { get; set; }
        public Point p2 { get; set; }
        public Point normal { get; set; }

        private void CalcNormal()
        {//https://math.stackexchange.com/questions/305642/how-to-find-surface-normal-of-a-triangle
            Point u = (p1 - p0);
            Point v = (p2 - p0);
            /*
            float Nx, Ny, Nz, Ax, Ay, Az;
            Nx = (u.y * v.z - u.z * v.y);
            Ny = (u.z * v.x - u.x * v.z);
            Nz = (u.x * v.y - u.y * v.x);

            Ax = Nx / (Math.Abs(Nx) + Math.Abs(Ny) + Math.Abs(Nz));
            Ay = Ny / (Math.Abs(Nx) + Math.Abs(Ny) + Math.Abs(Nz));
            Az = Nz / (Math.Abs(Nx) + Math.Abs(Ny) + Math.Abs(Nz));
            */
            normal = Point.CrossProduct(u, v);//new Point(Ax, Ay, Az);
            normal.Normalize();
        }

        public Point InsideTringle(Vector ray)
        {//http://geomalgorithms.com/a06-_intersect-2.html
         //http://www.lighthouse3d.com/tutorials/maths/ray-triangle-intersection/
            Point e1, e2, h, s, q;
            float a, f, u, v;
            e1 = p1 - p0;
            e2 = p2 - p0;
            h = Point.CrossProduct(ray.Direction, e2);
            a = Point.InnerProduct(e1, h);

            if (a > -0.00001 && a < 0.00001)
            {
                throw new NoHit();
            }

            f = 1 / a;
            s = ray.Location - p0;
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
                    ray.Location.x + ray.Direction.x * t,
                    ray.Location.y + ray.Direction.y * t,
                    ray.Location.z + ray.Direction.z * t);
            }
            throw new NoHit();
        }

        public override string ToString()
        {
            return string.Format("Triangle x:{0}, y:{1}, z:{2}",p0.ToString(), p1.ToString(), p2.ToString());
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
