using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Scene
    {
        public List<LightSource> Lights { get; set; }
        public List<Triangle> Triangles { get; set; }
        public List<Camera> Cameras { get; set; }

        public Scene(string filename)
        {
            Lights = new List<LightSource>();
            Triangles = new List<Triangle>();
            Cameras = new List<Camera>();
            ReadInputFile(filename);
            CreateFloor(-1);
        }

        public void StartTrace()
        {
            foreach (Camera item in Cameras)
            {
                item.Trace(Lights, Triangles);
            }
        }

        private float Trace(Vector akt)
        {
            throw new NotImplementedException();
        }

        private void CreateFloor(float z)
        {
            Triangles.Add(new Triangle(new Point(100, -100, 0), new Point(100, 100, 0), new Point(-100, 100, 0)));
            Triangles.Add(new Triangle(new Point(-100, 100, 0), new Point(-100, -100, 0), new Point(100, -100, 0)));
        }

        private void ReadInputFile(string filename)
        {
            MyXMLReader x = new MyXMLReader(filename);
            Console.WriteLine("Opened xml");
            Triangles = x.GetTriangles();
            Console.WriteLine("Loaded triangles");
            Lights = x.GetLights();
            Console.WriteLine("Loaded lights");
            Cameras = x.GetCameras();
            Console.WriteLine("Loaded cameras");
        }
    }
}