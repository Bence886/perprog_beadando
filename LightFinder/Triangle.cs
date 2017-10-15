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

            float Nx, Ny, Nz;
            Nx = (u.y * v.z - u.z * v.y);
            Ny = (u.z * v.x - u.x * v.z);
            Nz = (u.x * v.z - u.y * v.x);

            normal =  new Point(Nx, Ny, Nz);
            normal.Normalize();
        }

        public Point InsideTringle(Vector ray)
        {//http://geomalgorithms.com/a06-_intersect-2.html
         //http://www.lighthouse3d.com/tutorials/maths/ray-triangle-intersection/
            Point e1, e2, h, s, q;
            float a, f, u, v;
            e1 = p1 - p0;
            e2 = p2 - p0;
            h = Point.CrossProduct(ray.End-ray.Start, e2);
            a = Point.InnerProduct(e1, h);

            if (a > -0.00001 && a < 0.00001)
            {
                throw new NoHit();
            }

            f = 1 / a;
            s = ray.Start - p0;
            u = f * (Point.InnerProduct(s, h));
            if (u < 0.0 || u > 1.0)
            {
                throw new NoHit();
            }

            q = Point.CrossProduct(s, e1);

            v = f * Point.InnerProduct(ray.End-ray.Start, q);
            if (v < 0.0 || u + v > 1.0)
            {
                throw new NoHit();
            }
            float t = f * Point.InnerProduct(e2, q);
            if (t > 0.00001)
            {
                return new Point(
                    ray.Start.x + (ray.End - ray.Start).x * t,
                    ray.Start.y + (ray.End - ray.Start).y * t,
                    ray.Start.z + (ray.End - ray.Start).z * t);
            }
            throw new NoHit();
        }
    }
}
