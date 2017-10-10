using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class CreateBlenderScript
    {
        string def = @"
import bpy
import bmesh
def create_Vertices(name, verts):
    me = bpy.data.meshes.new(name+'Mesh')
    ob = bpy.data.objects.new(name, me)
    ob.show_name = True
    bpy.context.scene.objects.link(ob)
    me.from_pydata(verts, [], [])
    me.update()
    return ob";

        StreamWriter sw;

        public CreateBlenderScript(string name)
        {
            sw = new StreamWriter(name, false);
            sw.WriteLine(def);
        }

        public void Close()
        {
            sw.Close();
        }

        public void CreateObject(List<Point> points, string objName)
        {
            sw.WriteLine("verts = [");
            foreach (Point item in points)
            {
                sw.WriteLine(item.ToFile());
            }
            sw.WriteLine("]");
            sw.WriteLine(@"create_Vertices(""" + objName + "\", verts)");
        }
    }
}
