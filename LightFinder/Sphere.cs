using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Sphere : IMesh
    {
        public Point Center { get; set; }
        public float Radius { get; set; }

        public Sphere(Point c, float r)
        {
            Center = c;
            Radius = r;
        }

        public Point[] GetTriangles()
        {
            throw new NotImplementedException();
        }
    }
}