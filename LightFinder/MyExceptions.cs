using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    class NoHit : ApplicationException
    {
        string message;
        public NoHit(string msg)
        {
            message = msg;
        }
    }
}
