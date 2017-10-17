using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class LightSource
    {
        public LightSource(Point location, float intensity)
        {
            Location = location;
            Intensity = intensity;

        }

        public Point Location { get; set; }
        public float Intensity { get; set; }

        public bool IntersectLight(Vector ray)
        {
            Point op = Location - ray.Start;
            float b = Point.DotProduct(op, ray.End);
            float disc = b * b - Point.DotProduct(op, op) + Intensity * Intensity;
            if (disc < 0)
                return false;
            else disc = (float)Math.Sqrt(disc);
            return true;
        }

        public static LightSource LightHit(List<LightSource> lights, Vector ray)
        {
            LightSource closest = null;
            foreach (LightSource item in lights)
            {
                if (item.IntersectLight(ray) && (closest == null || Point.Distance(item.Location, ray.Start) < Point.Distance(closest.Location, ray.Start)))
                {
                    closest = item;
                }
            }
            if (closest == null)
            {
                Log.WriteLog("No Light hit.", LogType.Console, LogLevel.Trace);
                throw new NoHit();
            }
            Log.WriteLog(string.Format("Closest Light at: {0}", closest.ToString()), LogType.Console, LogLevel.Trace);
            return closest;
        }

        public override string ToString()
        {
            return Location.ToString();
        }
    }
}