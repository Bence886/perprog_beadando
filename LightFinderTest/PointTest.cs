using LightFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinderTest
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        public void InnerProduct()
        {
            Point a = new Point(1, 2, 3);
            Point b = new Point(4, 5, 6);
            float ex = 32;

            float c = Point.InnerProduct(a, b);
            
            Assert.AreEqual(ex, c);
        }

        [TestMethod]
        public void CrossProduct()
        {
            Point a = new Point(1, 2, 3);
            Point b = new Point(4, 5, 6);
            Point ex = new Point(-3, 6, -3);

            Point c = Point.CrossProduct(a, b);

            Assert.AreEqual(ex, c);
        }

        [TestMethod]
        public void Equality()
        {
            Point a = new Point(0, 0, 0);
            Point b = new Point(0.0001f, 0, 0);

            bool e = a.Equals(b);

            Assert.IsTrue(e);
        }

        [TestMethod]
        public void ListContains()
        {
            List<Point> pl = new List<Point>();
            pl.Add(new Point(0, 0, 0));
            Point p1 = new Point(0.0001f, 0, 0);

            bool e = !pl.Contains(p1);

            Assert.IsFalse(e);
        }

        [TestMethod]
        public void Distance()
        {
            Point a = new Point(0, 0, 0);
            Point b = new Point(1, 0, 0);

            float d = Point.Distance(a, b);

            Assert.AreEqual(1, d);
        }
    }
}
