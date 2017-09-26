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
        public List<Vector> LookDirections { get; set; }

        public Camera(Vector b)
        {
            LookDirections = new List<Vector>();
            Origin = b.End;
            Sphere = new Sphere(Origin, 10);
            GenerateLookDirections(b, 10);
        }

#warning "ezt valahogy tesztelni kell!!"
        private void GenerateLookDirections(Vector branch, int resolution)
        {//https://www.cmu.edu/biolphys/deserno/pdf/sphere_equi.pdf
            int n = 0;
            while (n < resolution)
            {
                double a = 4 * Math.PI * (Sphere.Radius * Sphere.Radius) / n;
                double d = Math.Sqrt(a);
                int M1 = (int)Math.Round(Math.PI / d, 0);
                double d0 = Math.PI / M1;
                double d1 = a / d0;
                for (int i = 0; i < M1 - 1; i++)
                {
                    double ro = Math.PI * (i + 0.5) / M1;
                    double M2 = Math.Round(2 * Math.PI * Math.Sin(ro / d1));
                    for (int j = 0; j < M2; j++)
                    {
                        double fi = 2 * Math.PI * j / M2;
                        Vector v = new Vector(new Point(0, 0, 0), new Point((float)(Sphere.Radius * (Math.Sin(ro) * Math.Cos(fi))), (float)(Sphere.Radius * (Math.Sin(ro) * Math.Sin(fi))), (float)Math.Cos(ro)));
                        v.DevideByLambda(Sphere.Radius);
                        LookDirections.Add(v);
                        n++;
                    }
                }
            }
        }

        public void Trace(List<LightSource> lights, List<IMesh> meshes)
        {
            throw new NotImplementedException();
        }

#warning majd kiderül kell e átlagolni vagy így jó
        public Vector GetBrightestLightDirection()
        {
            return new Vector(Sphere.Center, LookDirections.Where(x=>x.Length() == LookDirections.Max().Length()).SingleOrDefault().End);
        }
    }
}