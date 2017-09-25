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
        public List<IMesh> Meshes { get; set; }
        public List<Camera> Cameras { get; set; }

        public Scene(string filename)
        {
            Lights = new List<LightSource>();
            Meshes = new List<IMesh>();
            Cameras = new List<Camera>();
            ReadInputFile(filename);
            CreateFloor(-1);
        }

        private void CreateFloor(float z)
        {
            Mesh floor = new Mesh();
            floor.Points.Add(new Point(-100, -100, z));
            floor.Points.Add(new Point(100, -100, z));
            floor.Points.Add(new Point(100, 100, z));
            floor.Points.Add(new Point(-100, 100, z));

            Meshes.Add(floor);
        }

        private void ReadInputFile(string filename)
        {
            MyXMLReader x = new MyXMLReader(filename);
            Meshes = x.GetMeshes();
            Lights = x.GetLights();
            Cameras = x.GetCameras();
        }
    }
}