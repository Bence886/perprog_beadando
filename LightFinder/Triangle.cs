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
            this.p1 = p1;
            this.p2 = p2;
            this.p0 = p3;
            CalcNormal();
        }

        public Point p1 { get; set; }
        public Point p2 { get; set; }
        public Point p0 { get; set; }
        public Vector normal { get; set; }

        private void CalcNormal()
        {
            Point u = (p1 - p0);
            Point v = (p2 - p0);
            Point p = new Point((u.y * v.z - u.z * v.y), (u.z * v.x - u.x * v.z), (u.x*v.z-u.y*v.x));
            
            Vector V = new Vector(new Point(0, 0, 0), p);
            V.DevideByLambda(V.Length());
            normal = V;
        }

        public Point InsideTringle(Vector ray)
        {//http://geomalgorithms.com/a06-_intersect-2.html
            Vector u, v, n;
            Vector dir, w0, w;
            float r, a, b;
            u = new Vector(p1 - p0);
            v = new Vector(p2 - p0);

            n = u * v;

        }
    }
}
