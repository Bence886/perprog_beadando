using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Camera
    {
        public int Sampling { get; set; }
        public Point Origin { get; set; }
        public Icosahedron Icosahedronn { get; set; }
        public List<Point> LookDirections { get; set; }
        public int MaxDept { get; set; }
        static Random rnd = new Random();

        public Camera(Vector b)
        {
            LookDirections = new List<Point>();
            Origin = b.End;
            MaxDept = 4;
            Sampling = 100;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 3);
        }

        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            for (int i = 0; i < Sampling; i++)
            {
                Point ray = Point.GeneratePointOnSphere(Origin);
                ray.MultiplyByLambda(Trace(lights, triengles, new Vector(Origin, ray), 0));
                if (!LookDirections.Contains(ray))
                {
                    LookDirections.Add(ray);
                }
            }
        }

        private void TheradStarter(List<LightSource> lights, List<Triangle> triengles)
        {
            Point ray = Point.GeneratePointOnSphere(Origin);
            ray.MultiplyByLambda(Trace(lights, triengles, new Vector(Origin, ray), 0));
            if (!LookDirections.Contains(ray))
            {
                LookDirections.Add(ray);
            }
        }

        private float Trace(List<LightSource> lights, List<Triangle> triengles, Vector ray, int dept)
        {
            if (dept == MaxDept)
            {
                return 0;
            }
            float value = 0;
            foreach (LightSource item in lights)
            {
                value += LightHitBeforeTriangle(item, triengles, new Vector(ray.Start, item.Location));
            }
            if (value > 0)
            {
                return value;
            }
            else
            {
                Triangle triangleHit = null;
                try
                {
                    triangleHit = Triangle.ClosestTriangleHit(triengles, ray);
                }
                catch (NoHit)
                {
                    return 0;
                }
                Point pointHit = null;
                pointHit = triangleHit.InsideTringle(ray);

                for (int i = 0; i < Sampling; i++)
                {
                    value += Trace(lights, triengles, new Vector(pointHit, Point.GeneratePointOnHalfSphere(pointHit, triangleHit)), dept + 1);
                }
                return value;
            }
        }

        private float LightHitBeforeTriangle(LightSource light, List<Triangle> triengles, Vector ray)
        {
            LightSource lightHit = light;
            Triangle triangleHit = null;
            try
            {
                triangleHit = Triangle.ClosestTriangleHit(triengles, ray);
            }
            catch (NoHit)
            {
                return 0;
            }
            Point pointHit = null;
            pointHit = triangleHit.InsideTringle(ray);
            if (Point.Distance(lightHit.Location, ray.Start) < Point.Distance(pointHit, ray.Start))
            {
                return lightHit.Intensity;
            }
            return 0;
        }

        public Vector GetBrightestLightDirection()
        {//ez nem lesz jó
            return new Vector(Icosahedronn.Center, LookDirections.Where(x => x == LookDirections.Max()).SingleOrDefault());
        }
    }
}