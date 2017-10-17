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
        public int Dept { get; set; }
        static Random rnd = new Random();

        public Camera(Vector b)
        {
            LookDirections = new List<Point>();
            Origin = b.End;
            Dept = 4;
            Sampling = 10;
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
            if (dept == Dept)
            {
                return 0;
            }
            Point closest = ray.Start;
            Triangle hitTriangle = triengles.First();
            foreach (Triangle item in triengles)
            {
                try
                {
                    Point hit = item.InsideTringle(ray);
                    if (hit == ray.Start || Point.Distance(ray.Start, hit) > Point.Distance(ray.Start, closest))
                    {
                        hitTriangle = item;
                        closest = hit;
                    }
                }
                catch (NoHit)
                {
                    Log.WriteLog(string.Format("Dept: {2} Ray: {0}, \t missed: {1}", ray.ToString(), item.ToString(), dept), LogType.File, LogLevel.Trace);
                }
            }
            float value = 0;
            foreach (LightSource item in lights)
            {
                if (Point.Distance(item.Location, ray.Start) < Point.Distance(closest, ray.Start) && item.IntersectLight(ray))
                {
                    value += item.Intensity;
                    Log.WriteLog(string.Format("Dept: {2} Ray: {0}, \t hit: {1}", ray.ToString(), item.ToString(), dept), LogType.File, LogLevel.Debug);
                }
            }
            if (closest == null && closest == ray.Start)
            {
                return 0;
            }
            for (int i = 0; i < Sampling; i++)
            {
                Vector newRay = new Vector(closest, Point.GeneratePointOnHalfSphere(closest, hitTriangle));
                Trace(lights, triengles, newRay, dept + 1);
            }
            return value;
        }

        public Vector GetBrightestLightDirection()
        {//ez nem lesz jó
            return new Vector(Icosahedronn.Center, LookDirections.Where(x => x == LookDirections.Max()).SingleOrDefault());
        }
    }
}