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

            var cams = xDoc.Descendants("branches");
            foreach (XElement item in cams.Elements())
            {
                ret.Add(new Camera(new Vector(new Point(float.Parse(item.Attribute("startx").Value), float.Parse(item.Attribute("starty").Value), float.Parse(item.Attribute("startz").Value)), new Point(float.Parse(item.Attribute("endx").Value), float.Parse(item.Attribute("endy").Value), float.Parse(item.Attribute("endz").Value)))));
            }

            return ret;
        }

        public List<IMesh> GetMeshes()
        {
            List<IMesh> ret = new List<IMesh>();

            return ret;
        }
    }
}