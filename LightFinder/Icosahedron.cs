using System.Collections.Generic;
using System.IO;

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

            points = StartSubdiv(subdiv).ToArray();

            for (int i = 0; i < points.Length; i++)
            {
                points[i] += o;
            }

            StreamWriter sw = new StreamWriter("asd.txt", false);
            for (int i = 0; i < points.Length; i++)
            {
                sw.WriteLine(points[i].ToFile());
            }
            sw.Close();
        }

        private List<Point> StartSubdiv(int subdiv)
        {//csúnyademegy, minden sornál meghalt egy kiscica :'(
            List<Point> temp = new List<Point>();

            temp.AddRange(subdivide(points[0], points[4], points[1], subdiv));
            temp.AddRange(subdivide(points[0], points[9], points[4], subdiv));
            temp.AddRange(subdivide(points[9], points[5], points[4], subdiv));
            temp.AddRange(subdivide(points[4], points[5], points[8], subdiv));
            temp.AddRange(subdivide(points[4], points[8], points[1], subdiv));

            temp.AddRange(subdivide(points[8], points[10], points[1], subdiv));
            temp.AddRange(subdivide(points[8], points[3], points[10], subdiv));
            temp.AddRange(subdivide(points[5], points[3], points[8], subdiv));
            temp.AddRange(subdivide(points[5], points[2], points[3], subdiv));
            temp.AddRange(subdivide(points[2], points[7], points[3], subdiv));

            temp.AddRange(subdivide(points[7], points[10], points[3], subdiv));
            temp.AddRange(subdivide(points[7], points[6], points[10], subdiv));
            temp.AddRange(subdivide(points[7], points[11], points[6], subdiv));
            temp.AddRange(subdivide(points[11], points[0], points[6], subdiv));
            temp.AddRange(subdivide(points[0], points[1], points[6], subdiv));

            temp.AddRange(subdivide(points[6], points[1], points[10], subdiv));
            temp.AddRange(subdivide(points[9], points[0], points[11], subdiv));
            temp.AddRange(subdivide(points[9], points[11], points[2], subdiv));
            temp.AddRange(subdivide(points[9], points[2], points[5], subdiv));
            temp.AddRange(subdivide(points[7], points[2], points[11], subdiv));

            /*var a = new HashSet<Point>(temp);
            temp = new List<Point>(a);*/
            return temp;
        }

        List<Point> subdivide(Point v1, Point v2, Point v3, long depth)
        {
            List<Point> ret = new List<Point>();
            Point v12 = Point.GetMiddlePoint(v1, v2);
            Point v23 = Point.GetMiddlePoint(v2, v3);
            Point v31 = Point.GetMiddlePoint(v3, v1);

            if (depth == 0)
            {
                ret.Add(v1);
                ret.Add(v2);
                ret.Add(v3);
            }

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
