using System.Collections.Generic;
using System.IO;

namespace LightFinder
{
    class Icosahedron
    {//http://www.opengl.org.ru/docs/pg/0208.html

        int[][] TriangleIndicies =
             {
                 new int[] {0, 4, 1},
                 new int[] {0, 9, 4},
                 new int[] {9, 5, 4},
                 new int[] {4, 5, 8},
                 new int[] {4, 8, 1},

                 new int[] {8, 10, 1},
                 new int[] {8, 3, 10},
                 new int[] {5, 3, 8},
                 new int[] {5, 2, 3},
                 new int[] {2, 7, 3},

                 new int[] {7, 10, 3},
                 new int[] {7, 6, 10},
                 new int[] {7, 11, 6},
                 new int[] {11, 0, 6},
                 new int[] {0, 1, 6},

                 new int[] {6, 1, 10},
                 new int[] {9, 0, 11},
                 new int[] {9, 11, 2},
                 new int[] {9, 2, 5},
                 new int[] {7, 2, 11}
             };

        public Point[] points = {
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
        }

        private List<Point> StartSubdiv(int subdiv)
        {
            List<Point> temp = new List<Point>();

            for (int i = 0; i < TriangleIndicies.Length; i++)
            {
                subdivide(points[TriangleIndicies[i][0]], points[TriangleIndicies[i][1]], points[TriangleIndicies[i][2]], subdiv, ref temp);
            }

            return temp;
        }

        void subdivide(Point v1, Point v2, Point v3, long depth, ref List<Point> ret)
        {
            //List<Point> ret = new List<Point>();
            Point v12 = Point.GetMiddlePoint(v1, v2);
            Point v23 = Point.GetMiddlePoint(v2, v3);
            Point v31 = Point.GetMiddlePoint(v3, v1);

            if (depth == 0)
            {
                if (!ret.Contains(v1))
                    ret.Add(v1);
                if (!ret.Contains(v2))
                    ret.Add(v2);
                if (!ret.Contains(v3))
                    ret.Add(v3);
            }

            if (depth != 0)
            {
                subdivide(v1, v12, v31, depth - 1, ref ret);
                subdivide(v2, v23, v12, depth - 1, ref ret);
                subdivide(v3, v31, v23, depth - 1, ref ret);
                subdivide(v12, v23, v31, depth - 1, ref ret);
            }
            //return ret;
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
