using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.CurrentLogLevel = LogLevel.Debug;
            Log.AllConsole = true;
            Log.FileLog = false;

            Scene s = new Scene("in.xml");
            Log.WriteLog("Scene initialized!", LogType.Console, LogLevel.Message);
            CreateBlenderScript bs = new CreateBlenderScript("Blender.txt");

            foreach (Triangle item in s.Triangles)
            {
                bs.CreateObject(new List<Point> { item.p0, item.p1, item.p2} ,"Object");
            }
            foreach (LightSource item in s.Lights)
            {
                bs.CreateObject(new List<Point> { item.Location}, "Light");
            }

            s.StartTrace();
            Log.WriteLog("Trace finished", LogType.Console, LogLevel.Message);
            foreach (Camera item in s.Cameras)
            {
                bs.CreateObject(item.LookDirections.ToList(), "Camera");
            }

            bs.Close();
            Log.CloseWriter();
            Console.WriteLine("Program finished!");
            //Console.ReadKey();
        }
    }
}