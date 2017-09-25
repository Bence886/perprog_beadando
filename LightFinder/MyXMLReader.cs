using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LightFinder
{
    class MyXMLReader
    {
        XDocument xDoc;
        public MyXMLReader(string filename)
        {
            xDoc = XDocument.Load(filename);
        }

        public List<LightSource> GetLights()
        {
            List<LightSource> ret = new List<LightSource>();

            return ret;
        }

        public List<Camera> GetCameras()
        {
            List<Camera> ret = new List<Camera>();

            return ret;
        }

        public List<IMesh> GetMeshes()
        {
            List<IMesh> ret = new List<IMesh>();

            return ret;
        }
    }
}