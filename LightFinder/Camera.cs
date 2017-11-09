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

        static object lockObj = new object();

        public Camera(Point b)
        {
            LookDirections = new List<Point>();
            Origin = b;
            MaxDept = 2;
            Sampling = 100;
        }

        CreateBlenderScript bs = new CreateBlenderScript("BlenderTrace.txt");
        public void StartTrace(List<LightSource> lights, List<Triangle> triangles)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < Sampling; i++)
            {
                Task t = Task.Run(() => TaskMethod(lights, triangles));
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());
            bs.Close();
        }

        public void TaskMethod(List<LightSource> lights, List<Triangle> triangles)
        {
            Point ray = Point.GeneratePointOnSphere(Origin);
            Vector vector = new Vector(Origin, ray);
            float a = Trace(lights, triangles, ref vector, 0);
            ray.MultiplyByLambda(a);
            if (!LookDirections.Contains(vector.GetEndPoint()))
            {
                lock (lockObj)
                {
                    LookDirections.Add(vector.GetEndPoint());
                }
            }
        }

        private float Trace(List<LightSource> lights, List<Triangle> triangles, ref Vector ray, int dept)
        {
            if (dept == MaxDept)
            {
                return 0;
            }
            LightSource light = null;
            Point rayToLight;
            foreach (LightSource item in lights)
            {
                rayToLight = item.Location - ray.Location;
                rayToLight.Normalize();
                light = LightHitBeforeTriangle(item, triangles, new Vector(ray.Location, rayToLight));
            }
            if (light != null)
            {
                Point temp = light.Location - ray.Location;
                temp.Normalize();
                ray.Direction = temp;
                return light.Intensity;
            }
            else
            {
                Triangle triangleHit = null;
                try
                {
                    triangleHit = Triangle.ClosestTriangleHit(triangles, ray);
                }
                catch (NoHit)
                {
                    return 0;
                }
                Point pointHit = null;

                pointHit = triangleHit.InsideTringle(ray);
                float value = 0;
                Point offset = ray.GetEndPoint();
                offset.MultiplyByLambda(-0.0001f);
                pointHit += offset;
                bool backfacing = Point.DotProduct(triangleHit.Normal, ray.Direction) > 0;
                List<Point> TracePoints = new List<Point>();
                for (int i = 0; i < Sampling; i++)
                {
                    Vector newRay = new Vector(pointHit, Point.GeneratePointOnHalfSphere(triangleHit, backfacing));
                    value = Trace(lights, triangles, ref newRay, dept + 1);
                    if (!TracePoints.Contains(newRay.GetEndPoint()))
                    {
                        TracePoints.Add(newRay.GetEndPoint());
                    }
                }
                lock (lockObj)
                {
                    bs.CreateObject(TracePoints, "TracePath");
                }
                return value;
            }
        }

        private LightSource LightHitBeforeTriangle(LightSource light, List<Triangle> triangles, Vector ray)
        {
            LightSource lightHit = light;
            Triangle triangleHit = null;
            try
            {
                triangleHit = Triangle.ClosestTriangleHit(triangles, ray);
            }
            catch (NoHit)
            {
            }
            Point pointHit = null;
            if (triangleHit != null)
            {
                pointHit = triangleHit.InsideTringle(ray);
                if (Point.Distance(lightHit.Location, ray.Location) < Point.Distance(pointHit, ray.Location))
                {
                    return lightHit;
                }
                else
                {
                    return null;
                }
            }
            return light;
        }
    }
}