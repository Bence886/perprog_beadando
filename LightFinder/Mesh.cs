using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Mesh : IMesh
    {
        public List<Point> Points { get; set; }
        public Mesh()
        {
            Points = new List<Point>();
        }

        public List<Triangle> GetTriengles()
        {
            throw new NotImplementedException();
        }
    }
}