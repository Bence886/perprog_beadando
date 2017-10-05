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

            points = StartSubdiv(subdiv).ToArray();

            StreamWriter sw = new StreamWriter("asd.txt", false);
            for (int i = 0; i < points.Length; i++)
            {
                sw.WriteLine(points[i].ToFile());
            }
            sw.Close();
        }

        private List<Point> StartSubdiv(int subdiv)
        {//csúnyademegy minden sornál meghalt egy kiscica :'(
            List<Point> temp = new List<Point>();
            #region kuka
            /*temp.AddRange(subdivide(points[0], points[4], points[1], subdiv));
            temp.AddRange(subdivide(points[0], points[9], points[4], subdiv));
            temp.AddRange(subdivide(points[9], points[5], points[4], subdiv));
            temp.AddRange(subdivide(points[4], points[5], points[8], subdiv));
            temp.AddRange(subdivide(points[4], points[8], points[1], subdiv));

            temp.AddRange(subdivide(points[4], points[10], points[1], subdiv));
            temp.AddRange(subdivide(points[8], points[3], points[10], subdiv));
            temp.AddRange(subdivide(points[5], points[3], points[8], subdiv));
            temp.AddRange(subdivide(points[5], points[2], points[3], subdiv));
            temp.AddRange(subdivide(points[2], points[7], points[3], subdiv));

            temp.AddRange(subdivide(points[7], points[10], points[3], subdiv));
            temp.AddRange(subdivide(points[7], points[6], points[10], subdiv));
            temp.AddRange(subdivide(points[7], points[11], points[6], subdiv));
            temp.AddRange(subdivide(points[11], points[0], points[6], subdiv));
            temp.AddRange(subdivide(points[0], points[1], points[6], subdiv));

            temp.AddRange(subdivide(points[4], points[1], points[10], subdiv));
            temp.AddRange(subdivide(points[9], points[0], points[11], subdiv));
            temp.AddRange(subdivide(points[9], points[11], points[2], subdiv));
            temp.AddRange(subdivide(points[9], points[2], points[0], subdiv));
            temp.AddRange(subdivide(points[7], points[2], points[11], subdiv));*/
            #endregion kuka

            int[][] tindices = {
                     new int[] {0,4,1},
                     new int[] { 0,9,4},
                     new int[] { 9,5,4},
                     new int[] { 4,5,8},
                     new int[] { 4,8,1},
                     new int[] { 8,10,1},
                     new int[] { 8,3,10},
                     new int[] { 5,3,8},
                     new int[] { 5,2,3},
                     new int[] { 2,7,3},
                     new int[] { 7,10,3},
                     new int[] { 7,6,10},
                     new int[] { 7,11,6},
                     new int[] { 11,0,6},
                     new int[] { 0,1,6},
                     new int[] { 6,1,10},
                     new int[] { 9,0,11},
                     new int[] { 9,11,2},
                     new int[] { 9,2,5},
                     new int[] { 7,2,11}
                 };
            for (int i = 0; i < 20; i++)
            {
                temp = subdivide(
                    points[tindices[i][0]],
                    points[tindices[i][1]],
                    points[tindices[i][2]],
                    subdiv);
            }
            return temp;
        }

        List<Point> subdivide(Point v1, Point v2, Point v3, long depth)
        {
            List<Point> ret = new List<Point>();
            Point v12 = v1 + v2;
            Point v23 = v2 + v3;
            Point v31 = v3 + v1;

            ret.Add(v1);
            ret.Add(v2);
            ret.Add(v3);

            v12.normalize();
            v23.normalize();
            v31.normalize();
            
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