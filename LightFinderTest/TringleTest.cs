﻿using LightFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestClass]
public class TringleTest
{
    [TestInitialize]
    public void Init()
    {

    }

    [TestMethod]
    public void CalcNormalTest()
    {
        //arrange
        Point p0 = new Point(1, -1, 0);
        Point p1 = new Point(1, 1, 0);
        Point p2 = new Point(-1, 1, 0);
        Triangle T = new Triangle(p0, p1, p2);
        Point V = new Point(0, 0, 1);

        //act
        Point N = T.normal.End;

        //assert
        Assert.AreEqual(V.x, N.x);
        Assert.AreEqual(V.y, N.y);
        Assert.AreEqual(V.z, N.z);
    }

    [TestMethod]
    public void InsideTringle()
    {
        Point p0 = new Point(1, -1, 0);
        Point p1 = new Point(1, 1, 0);
        Point p2 = new Point(-1, -1, 0);
        Triangle T = new Triangle(p0, p1, p2);
#warning "Bence! számold ki hogy jó értéket adj meg!"
        Point p3 = new Point(0.33f, 0.33f, -1);
        Point p4 = new Point(0.33f, 0.33f, 1);

        Vector V = new Vector(p3, p4);
        Point R = new Point(0.33f, 0.33f, 0);

        Point H = T.InsideTringle(V);

        Assert.AreEqual(R, H);
    }
}
