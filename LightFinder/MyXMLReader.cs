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

            var cams = xDoc.Descendants("lights");
            foreach (XElement item in cams.Elements())
            {
                ret.Add(new LightSource(
                    new Point(float.Parse(item.Attribute("posx").Value), float.Parse(item.Attribute("posy").Value), float.Parse(item.Attribute("posz").Value)), 
                    int.Parse(item.Attribute("intensity").Value)));
            }

            return ret;
        }

        public List<Camera> GetCameras()
        {
            List<Camera> ret = new List<Camera>();

            var cams = xDoc.Descendants("branches");
            foreach (XElement item in cams.Elements())
            {
                ret.Add(new Camera(new Point(float.Parse(item.Attribute("startx").Value), float.Parse(item.Attribute("starty").Value), float.Parse(item.Attribute("startz").Value))));
            }

            return ret;
        }

        public List<Triangle> GetTriangles()
        {
            List<Triangle> ret = new List<Triangle>();

            var cams = xDoc.Descendants("triangles");
            foreach (XElement item in cams.Elements())
            {
                var point0 = item.Element("point0");
                var point1 = item.Element("point1");
                var point2 = item.Element("point2");
                ret.Add(new Triangle(
                    new Point(float.Parse(point0.Attribute("posx").Value), float.Parse(point0.Attribute("posy").Value), float.Parse(point0.Attribute("posz").Value)),
                    new Point(float.Parse(point1.Attribute("posx").Value), float.Parse(point1.Attribute("posy").Value), float.Parse(point1.Attribute("posz").Value)),
                    new Point(float.Parse(point2.Attribute("posx").Value), float.Parse(point2.Attribute("posy").Value), float.Parse(point2.Attribute("posz").Value))));
            }
            return ret;
        }
    }
}