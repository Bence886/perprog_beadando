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
            Sampling = 20;
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
                if (!LookDirections.Contains(vector.End))
                {
                    LookDirections.Add(vector.End);
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
            foreach (LightSource item in lights)
            {
                light = LightHitBeforeTriangle(item, triengles, new Vector(ray.Start, item.Location));
            }
            if (light != null)
            {
                ray.End = light.Location;
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
                Point offset = ray.End;
                offset.MultiplyByLambda(-1);
                offset.DevideByLambda(1000);
                pointHit += offset;
                Point rayDirection = ray.End - ray.Start;
                rayDirection.Normalize();
                bool backfacing = Point.DotProduct(triangleHit.normal, rayDirection) > 0;
                for (int i = 0; i < Sampling; i++)
                {
                    Vector vector = new Vector(pointHit, Point.GeneratePointOnHalfSphere(pointHit, triangleHit, backfacing));
                    value = Trace(lights, triengles, ref vector, dept + 1);
                    if (!TracePoints.Contains(vector.End))
                    {
                        TracePoints.Add(vector.End);
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
            {}
            Point pointHit = null;
            if (triangleHit != null)
            {
                pointHit = triangleHit.InsideTringle(ray);
                if (Point.Distance(lightHit.Location, ray.Start) < Point.Distance(pointHit, ray.Start))
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

        public Vector GetBrightestLightDirection()
        {//ez nem lesz jó
            return new Vector(Icosahedronn.Center, LookDirections.Where(x => x == LookDirections.Max()).SingleOrDefault());
        }
    }
}