using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Camera
    {
        public Point Origin { get; set; }
        public Sphere Sphere { get; set; }
        Dictionary<Vector, float> LookDirections;

        public Camera(Vector b)
        {
            LookDirections = new Dictionary<Vector, float>();
            Origin = b.End;
            GenerateLookDirections(b);
        }

#warning "Ez lehet async?!"
        private void GenerateLookDirections(Vector branch) 
        {

        }

        public Vector GetBrightestLightDirection()
        {
            return LookDirections.Max().Key;
        }
    }
}