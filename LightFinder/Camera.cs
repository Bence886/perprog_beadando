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
        public List<Point> LookDirections { get; set; }
        public int Dept { get; set; }

        public Camera(Vector b)
        {
            LookDirections = new List<Point>();
            Origin = b.End;
            Dept = 4;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 3);
        }

        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            Point ray = GeneratePointOnSphere(Origin);
            //dont do doubles
            ray.MultiplyByLambda(Trace(lights, triengles, new Vector(Origin, ray), 0));
            if (!LookDirections.Contains(ray))
            {
                LookDirections.Add(ray);
            }
        }

        private int Trace(List<LightSource> lights, List<Triangle> triengles, Vector ray, int dept)
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
                    if ( hit == ray.Start || Point.Distance(ray.Start, hit) > Point.Distance(ray.Start, closest))
                    {
                        hitTriangle = item;
                        closest = hit;
                    }
                }
                catch (NoHit)
                {
                    Log.WriteLog(string.Format("Ray: {0}, \t missed: {1}", ray.ToString(), item.ToString()), LogType.File, LogLevel.Debug);
                }
            }
            if (closest == null)
            {
                return 0;
            }
            Point newRay = GeneratePointOnHalfSphere(closest, hitTriangle); //halfsphere
        }

        private Point GeneratePointOnSphere(Point origin)
        {
            Point randomPoint = new Point(0, 0, 0);
            return randomPoint;
        }

        private Point FindClosestPoint(List<Point> hitpoint, Point start)
        {
            Point closest = new Point(0, 0, 0);



            return closest;
        }

        public Vector GetBrightestLightDirection()
        {//ez nem lesz jó
            return new Vector(Icosahedronn.Center, LookDirections.Where(x => x == LookDirections.Max()).SingleOrDefault());
        }
    }
}