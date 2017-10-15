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
            Sampling = 1000;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 3);
        }

        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            for (int i = 0; i < Sampling; i++)
            {
                Point ray = GeneratePointOnHalfSphere(Origin, 
                    new Triangle(new Point(-1, -1, 0), new Point(1, -1, 1), new Point(1, 1, 0)));

                ray.MultiplyByLambda(Trace(lights, triengles, new Vector(Origin, ray), 0));
                LookDirections.Add(ray);
            }
        }

        private int Trace(List<LightSource> lights, List<Triangle> triengles, Vector ray, int dept)
        {
           /* if (dept == Dept)
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
            if (closest == null && closest == ray.Start)
            {
                return 0;
            }

            Point lightHit = ray.Start;
            foreach (LightSource item in lights)
            {
                lightHit = item.IntersectLight(ray);
            }

            for (int i = 0; i < Sampling; i++)
            {
                Vector newRay = new Vector(closest, GeneratePointOnHalfSphere(closest, hitTriangle)); //halfsphere
                Trace(lights, triengles, newRay, dept+1);
            }*/
            return 1;
        }

        private Point GeneratePointOnHalfSphere(Point closest, Triangle hitTriangle)
        {
            Point normal = hitTriangle.normal;
            Point direction = new Point(normal.y, normal.x*-1, normal.z);
            Point cross = Point.CrossProduct(normal, direction);

            /*normal.MultiplyByLambda((float)rnd.NextDouble() * (1 - (-1)) + (-1));
            direction.MultiplyByLambda((float)rnd.NextDouble() * (1 - (-1)) + (-1));
            cross.MultiplyByLambda((float)rnd.NextDouble());*/

            float x, y, z;
            x = (float)rnd.NextDouble() * (1 - (-1)) + (-1);
            y = (float)rnd.NextDouble() * (1 - (-1)) + (-1);
            z = (float)rnd.NextDouble();

            Point randomPoint = new Point(x*normal.x + y*direction.x + z* cross.x, x * normal.y + y * direction.y + z * cross.y, x * normal.z + y * direction.z + z * cross.z);
            randomPoint.Normalize();
            randomPoint = randomPoint + closest;
            return randomPoint;
        }

        private Point GeneratePointOnSphere(Point origin)
        {
            Point randomPoint = new Point((float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1));
            while (randomPoint.Lenght() > 0)
            {
                randomPoint = new Point((float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1), (float)rnd.NextDouble() * (1 - (-1)) + (-1));
            }
            randomPoint.Normalize();
            randomPoint = randomPoint + origin;
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