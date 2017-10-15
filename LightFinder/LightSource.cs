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

        public Point Location { get; set; }
        public float Intensity { get; set; }

        public Point IntersectLight(Vector ray)
        {
            throw new NotImplementedException();
        }
    }
}