using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    interface IMesh
    {
        Point[] GetTriangles();
        int GetPolyCount();
        int GetTriangleCount();
    }
}