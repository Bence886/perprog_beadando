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

        public Camera(Point b)
        {
            LookDirections = new List<Point>();
            Origin = b;
            MaxDept = 4;
            Sampling = 10;
        }

        public void Init()
        {
            Icosahedronn = new Icosahedron(Origin, 3);
        }

        List<Point> TracePoints;
        public void StartTrace(List<LightSource> lights, List<Triangle> triengles)
        {
            CreateBlenderScript bs = new CreateBlenderScript("BlenderTrace.txt");
            for (int i = 0; i < Sampling; i++)
            {
                TracePoints = new List<Point>();
                Point ray = Point.GeneratePointOnSphere(Origin);
                Vector vector = new Vector(Origin, ray);
                float a = Trace(lights, triengles, ref vector, 0);
                ray.MultiplyByLambda(a);
                if (!LookDirections.Contains(vector.GetEndPoint()))
                {
                    LookDirections.Add(vector.GetEndPoint());
                }
                bs.CreateObject(TracePoints, "TracePath");
            }
            bs.Close();
        }

        private float Trace(List<LightSource> lights, List<Triangle> triengles,ref Vector ray, int dept)
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
                light = LightHitBeforeTriangle(item, triengles, new Vector(ray.Location, rayToLight));
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
                    triangleHit = Triangle.ClosestTriangleHit(triengles, ray);
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
                for (int i = 0; i < Sampling; i++)
                {
                    Vector newRay = new Vector(pointHit, Point.GeneratePointOnHalfSphere(triangleHit, backfacing));
                    value = Trace(lights, triengles, ref newRay, dept + 1);
                    if (!TracePoints.Contains(newRay.GetEndPoint()))
                    {
                        TracePoints.Add(newRay.GetEndPoint());
                    }
                }
                return value;
            }
        }

        private LightSource LightHitBeforeTriangle(LightSource light, List<Triangle> triengles, Vector ray)
        {
            LightSource lightHit = light;
            Triangle triangleHit = null;
            try
            {
                triangleHit = Triangle.ClosestTriangleHit(triengles, ray);
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