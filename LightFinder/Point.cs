﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    public class Point:IEquatable<Point>
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public void MultiplyByLambda(float a)
        {
            x *= a;
            y *= a;
            z *= a;
        }

        public static float DotProduct(Point u, Point v)
        {
            return (u.x * v.x + u.y * v.y + u.z * v.z);
        }

        public void DevideByLambda(float a)
        {
            x /= a;
            y /= a;
            z /= a;
        }

        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}, Z:{2}", x, y, z);
        }

        public void Normalize()
        {
            float d = (float)Math.Sqrt(x * x + y * y + z * z);
            if (d != 0)
            {
                DevideByLambda(Math.Abs(d));
            }
        }

        public static Point GetMiddlePoint(Point a, Point b)
        {
            Point midle;
            midle = a + b;
            midle.DevideByLambda(2);
            midle.Normalize();

            return midle;
        }

        public static float InnerProduct(Point v, Point q)
        {//http://www.lighthouse3d.com/tutorials/maths/inner-product/
            return v.x * q.x + v.y * q.y + v.z * q.z;
        }

        public string ToFile()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return string.Format("({0}, {1}, {2}),", x.ToString(nfi), y.ToString(nfi), z.ToString(nfi));
        }

        public override bool Equals(object obj)
        {
            Point o = (Point)obj;
            float epsilon = 0.0001f;
            return CompFloat(x, o.x, epsilon)
                && CompFloat(y, o.y, epsilon) 
                && CompFloat(z, o.z, epsilon);
        }

        private bool CompFloat(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static Point CrossProduct(Point b, Point c)
        {//http://www.lighthouse3d.com/tutorials/maths/vector-cross-product/
            return new Point(b.y * c.z - c.y * b.z, b.z * c.x - c.z * b.x, b.x * c.y - c.x * b.y);
        }

        public static float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }

        public float Lenght()
        {
            return Distance(new Point(0, 0, 0), this);
        }

        public bool Equals(Point o)
        {
            float epsilon = 0.001f;
            return CompFloat(x, o.x, epsilon)
                && CompFloat(y, o.y, epsilon)
                && CompFloat(z, o.z, epsilon);
        }
    }
}
