using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class Vector
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Vector(Point s, Point e)
        {
            Start = s;
            End = e;
        }
    }
}