using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector()
        {
        }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        private void Add(float V)
        {
            X += V;
            Y += V;
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.X / b.X, a.Y / b.Y);
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
            //return $"X: {X}, Y: {Y}";     // in C#7.0
        }
    }
}
