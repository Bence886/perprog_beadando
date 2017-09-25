using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Mesh:IMesh
    {
        public List<Point> Points { get; set; }
        public Mesh()
        {
            Points = new List<Point>();
        }

        public Point[] GetTriangles()
        {
            throw new NotImplementedException();
        }

        public int GetPolyCount()
        {
            throw new NotImplementedException();
        }

        public int GetTriangleCount()
        {
            throw new NotImplementedException();
        }
    }
}