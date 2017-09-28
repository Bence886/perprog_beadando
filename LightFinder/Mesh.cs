using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Mesh
    {
        public List<Triangle> Triangles { get; set; }
        public Mesh()
        {
            Triangles = new List<Triangle>();
        }
    }
}