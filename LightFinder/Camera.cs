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

        public Camera(Vector b)
        {
            LookDirections = new List<Point>();
            Origin = b.End;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 3);
            LookDirections = Icosahedronn.points.ToList();
        }

        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            foreach (Point item in LookDirections)
            {
                item.MultiplyByLambda(Trace(lights, triengles, new Vector(Origin, item), 10, 1));
            }
        }

        private int Trace(List<LightSource> lights, List<Triangle> triengles, Vector ray, int dept, int hits)
        {
            List<Point> hitpoint = new List<Point>();
            Point closest = ray.Start;
            foreach (Triangle item in triengles)
            {
                try
                {
                    hitpoint.Add(item.InsideTringle(ray));
                }
                catch (NoHit)
                {
                    Log.WriteLog(string.Format("Ray: {0}, \t missed: {1}",ray.ToString(), item.ToString()), LogType.File, LogLevel.Debug);
                }
            }
            closest = hitpoint.Where(y => Point.Distance(y, ray.Start) == hitpoint.Select(x => Point.Distance(x, ray.Start)).Min()).FirstOrDefault(); //legközelebbi hit
            Log.WriteLog(string.Format("Ray: {0} \t hit at:{1}", ray, closest), LogType.File, LogLevel.Debug);


            return hits;
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