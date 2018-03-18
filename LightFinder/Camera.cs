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
            Sampling = 1000;
        }
        CreateBlenderScript bs;
        public void StartTrace(List<LightSource> lights, List<Triangle> triangles, int num)
        {
            bs = new CreateBlenderScript("BlenderTrace" + num + ".txt");
            StartParalel(lights, triangles);
            //StartSequential(lights, triangles);
        }

        private void StartSequential(List<LightSource> lights, List<Triangle> triangles)
        {
            for (int i = 0; i < Sampling; i++)
            {
                Point ray = Point.GeneratePointOnSphere(Origin);
                Vector vector = new Vector(Origin, ray);
                float a = Trace(lights, triangles, vector, MaxDept);
                ray.MultiplyByLambda(a);
                if (!LookDirections.Contains(vector.GetEndPoint()))
                {
                    LookDirections.Add(vector.GetEndPoint());
                }
            }
            bs.Close();
        }

        private void StartParalel(List<LightSource> lights, List<Triangle> triangles)
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
            float a = Trace(lights, triangles, vector, MaxDept);
            ray.MultiplyByLambda(a);
            if (!LookDirections.Contains(vector.GetEndPoint()))
            {
                lock (lockObj)
                {
                    LookDirections.Add(vector.GetEndPoint());
                }
            }
        }

        /*private float Trace(List<LightSource> lights, List<Triangle> triangles, ref Vector ray, int dept)
        {
            Log.WriteLog("Trace Dept: " + dept, LogType.Console, LogLevel.Trace);

            List<float> value = new List<float>();
            List<LightSource> light = new List<LightSource>();
            Point rayToLight;
            foreach (LightSource item in lights)
            {
                rayToLight = item.Location - ray.Location;
                rayToLight.Normalize();
                LightSource l = LightHitBeforeTriangle(item, triangles, new Vector(ray.Location, rayToLight));
                if (l != null)
                {
                    light.Add(l);
                }
            }
            foreach (LightSource akt in light)
            {
                Point temp = akt.Location - ray.Location;
                temp.Normalize();
                ray.Direction = temp;
                value.Add(akt.Intensity);
            }
            if (light.Count() == 0)
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
                if (dept + 1 <= MaxDept)
                {
                    Point pointHit = triangleHit.InsideTringle(ray);
                    Point offset = new Point(ray.Direction);
                    offset.MultiplyByLambda(-1);
                    offset.MultiplyByLambda(0.001f);
                    pointHit += offset;
                    bool backfacing = Point.DotProduct(triangleHit.Normal, ray.Direction) > 0;
                    List<Point> TracePoints = new List<Point>();
                    for (int i = 0; i < Sampling; i++)
                    {
                        Vector newRay = new Vector(pointHit, Point.GeneratePointOnHalfSphere(triangleHit, backfacing));
                        value.Add(Trace(lights, triangles, ref newRay, dept + 1));
                        if (!TracePoints.Contains(newRay.GetEndPoint()))
                        {
                            TracePoints.Add(newRay.GetEndPoint());
                        }
                    }
                    lock (lockObj)
                    {
                        bs.CreateObject(TracePoints, dept + "_TracePath");
                    }
                }
                if (value.Count != 0)
                {
                    return value.Max();
                }
            }
            return 0;
        }*/

        private float Trace(List<LightSource> lights, List<Triangle> triangles, Vector startPoint, int dept)
        {
            for (int deptCounter = 0; deptCounter < dept; deptCounter++)
            {
                List<LightSource> directHitLights = new List<LightSource>();
                Point rayToLight;
                foreach (LightSource item in lights)
                {
                    rayToLight = item.Location - startPoint.Location;
                    rayToLight.Normalize();
                    if (LightHitBeforeTriangle(item, triangles, new Vector(startPoint.Location, rayToLight)))
                    {
                        directHitLights.Add(item);
                    }
                }
                if (directHitLights.Count() > 0)
                {
                    return lights.Select(x => x.Intensity).Max();
                }
                Tuple<Triangle, Point> TriangePointPair = null;
                try
                {
                    TriangePointPair = Triangle.ClosestTriangleHit(triangles, startPoint);
                }
                catch (NoHit)
                {
                    return 0;
                }

                Triangle triangleHit = TriangePointPair.Item1;
                Point pointHit = TriangePointPair.Item2;
                Point offset = new Point(startPoint.Direction);
                offset.MultiplyByLambda(-1);
                offset.MultiplyByLambda(0.001f);
                pointHit += offset;

                bool backfacing = Point.DotProduct(triangleHit.Normal, startPoint.Direction) > 0;

                startPoint = new Vector(pointHit, Point.GeneratePointOnHalfSphere(triangleHit, backfacing));
            }
            return 0;
        }

        private bool LightHitBeforeTriangle(LightSource light, List<Triangle> triangles, Vector ray)
        {
            Tuple<Triangle, Point> TrianglePointPair = null;
            try
            {
                TrianglePointPair = Triangle.ClosestTriangleHit(triangles, ray);
            }
            catch (NoHit)
            {
            }
            if (TrianglePointPair != null)
            {
                Point pointHit = TrianglePointPair.Item2;
                if (Point.Distance(light.Location, ray.Location) < Point.Distance(pointHit, ray.Location))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
