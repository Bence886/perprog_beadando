﻿using System;
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
                item.StartTrace(Lights, Triangles);
            }
        }

        public void Trace()
        {
            foreach (Camera item in Cameras)
            {
                item.StartTrace(Lights, Triangles);
            }
        }

        private void CreateFloor(float z)
        {
            Triangles.Add(new Triangle(new Point(100, -100, 0), new Point(100, 100, 0), new Point(-100, 100, 0)));
            Triangles.Add(new Triangle(new Point(-100, 100, 0), new Point(-100, -100, 0), new Point(100, -100, 0)));
            Log.WriteLog("Created floors", LogType.Console, LogLevel.Debug);
        }

        private void ReadInputFile(string filename)
        {
            MyXMLReader x = new MyXMLReader(filename);
            Log.WriteLog("Opened xml", LogType.Console, LogLevel.Trace);
            Triangles = x.GetTriangles();
            Log.WriteLog("Loaded triangles", LogType.Console, LogLevel.Trace);
            Lights = x.GetLights();
            Log.WriteLog("Loaded lights", LogType.Console, LogLevel.Trace);
            Cameras = x.GetCameras();
            Log.WriteLog("Loaded cameras", LogType.Console, LogLevel.Trace);
        }
    }
}