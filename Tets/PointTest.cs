using LightFinder;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinderTest
{
    [TestFixture]
    public class PointTest
    {
        [Test]
        public void InnerProduct()
        {
            Point a = new Point(1, 2, 3);
            Point b = new Point(4, 5, 6);
            float ex = 32;

            float c = Point.InnerProduct(a, b);

            Assert.AreEqual(ex, c);
        }

        [TestCase(1, 2, 3, 4, 5, 6, -3, 6, -3)]
        [TestCase(6, 9, 2, 3, 7, 5, 31, -24, 15)]
        [Test]
        public void CrossProduct(float a, float b, float c, float d, float e, float f, float g, float h, float i)
        {
            Point v1 = new Point(a, b, c);
            Point v2 = new Point(d, e, f);
            Point ex = new Point(g, h, i);

            Point v3 = Point.CrossProduct(v1, v2);

            Assert.AreEqual(ex, v3);
        }

        [Test]
        public void Equality()
        {
            Point a = new Point(0, 0, 0);
            Point b = new Point(0.0001f, 0, 0);

            bool e = a.Equals(b);

            Assert.IsTrue(e);
        }

        [Test]
        public void ListContains()
        {
            List<Point> pl = new List<Point>();
            pl.Add(new Point(0, 0, 0));
            Point p1 = new Point(0.0001f, 0, 0);

            bool e = !pl.Contains(p1);

            Assert.IsFalse(e);
        }

        [Test]
        public void Distance()
        {
            Point a = new Point(0, 0, 0);
            Point b = new Point(1, 0, 0);

            float d = Point.Distance(a, b);

            Assert.AreEqual(1, d);
        }
    }
}
