using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Icosahedron
    {//http://www.opengl.org.ru/docs/pg/0208.html
        Point[] points = {
            new Point(-0.525731112119133606f, 0.0f, 0.850650808352039932f),
            new Point(0.525731112119133606f, 0.0f, 0.850650808352039932f),
            new Point(-0.525731112119133606f, 0.0f, -0.850650808352039932f),
            new Point(0.525731112119133606f, 0.0f, -0.850650808352039932f),
            new Point(0.0f, 0.850650808352039932f, 0.525731112119133606f),
            new Point(0.0f, 0.850650808352039932f, -0.525731112119133606f),
            new Point(0.0f, -0.850650808352039932f, 0.525731112119133606f),
            new Point(0.0f, -0.850650808352039932f, -0.525731112119133606f),
            new Point(0.850650808352039932f, 0.525731112119133606f, 0.0f),
            new Point(-0.850650808352039932f, 0.525731112119133606f, 0.0f),
            new Point(0.850650808352039932f, -0.525731112119133606f, 0.0f),
            new Point(-0.850650808352039932f, -0.525731112119133606f, 0.0f)
        };

        public Point Center { get; set; }

        public Icosahedron(Point o, int subdiv)
        {
            Center = o;
            for (int i = 0; i < points.Length; i++)
            {
                points[i] += o;
            }

            List<Point> temp = new List<Point>();

            for (int i = 0; i < points.Length - 3; i++)
            {
                temp.AddRange(subdivide(points[i], points[i + 1], points[i + 2], subdiv));
            }

            points = temp.ToArray();

            StreamWriter sw = new StreamWriter("asd.txt");
            for (int i = 0; i < points.Length; i++)
            {
                sw.WriteLine(points[i].ToFile());
            }
            sw.Close();
        }

        List<Point> subdivide(Point v1, Point v2, Point v3, long depth)
        {
            List<Point> ret = new List<Point>();
            Point v12 = v1 + v2;
            Point v23 = v2 + v3;
            Point v31 = v3 + v1;

            ret.Add(v12);
            ret.Add(v23);
            ret.Add(v31);

            if (depth != 0)
            {
                ret.AddRange(subdivide(v1, v12, v31, depth - 1));
                ret.AddRange(subdivide(v2, v23, v12, depth - 1));
                ret.AddRange(subdivide(v3, v31, v23, depth - 1));
                ret.AddRange(subdivide(v12, v23, v31, depth - 1));
            }

            return ret;
        }
    }
}


/*
import bpy
import bmesh

def create_Vertices(name, verts):
    me = bpy.data.meshes.new(name+'Mesh')
    ob = bpy.data.objects.new(name, me)
    ob.show_name = True
    bpy.context.scene.objects.link(ob)
    me.from_pydata(verts, [], [])
    me.update()
    return ob

verts = []

create_Vertices("asd", verts)
*/
