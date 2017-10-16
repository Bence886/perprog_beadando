using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class LightSource
    {
        public LightSource(Point location, float intensity)
        {
            Location = location;
            Intensity = intensity;
        }

        private float rad = 0.01f;

        public Point Location { get; set; }
        public float Intensity { get; set; }

        public bool IntersectLight(Vector ray)
        {
            Point op = Location - ray.Start;
            float b = Point.DotProduct(op, ray.End);
            float disc = b * b - Point.DotProduct(op, op) + rad * rad;
            if (disc < 0)
                return false;
            else disc = (float)Math.Sqrt(disc);
            return true;
        }

        public override string ToString()
        {
            return Location.ToString();
        }
    }
}