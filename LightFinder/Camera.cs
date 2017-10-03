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
        public Icosahedron Icosahedronn { get; set; }
        public List<Vector> LookDirections { get; set; }

        public Camera(Vector b)
        {
            LookDirections = new List<Vector>();
            Origin = b.End;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 2);
            GenerateLookDirections(
                new Vector(
                    new Point(0, 0, 0), Origin), 10);
        }

#warning "ezt valahogy tesztelni kell!!"
        private void GenerateLookDirections(Vector branch, int resolution)
        {            
        }

        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            throw new NotImplementedException();
        }

        public Vector GetBrightestLightDirection()
        {
                return new Vector(Icosahedronn.Center, LookDirections.Where(x => x.Length() == LookDirections.Max().Length()).SingleOrDefault().End);
        }
    }
}