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
            Scene s = new Scene("in.xml");

            CreateBlenderScript bs = new CreateBlenderScript("Blender.txt");
            foreach (Camera item in s.Cameras)
            {
                bs.CreateObject(item.Icosahedronn.points.ToList(), "Camera");
            }

            bs.Close();
        }
    }
}