using System;
using UnityEngine;

namespace SBaier.Sampling
{
    public struct Coordinate2D : IComparable<Coordinate2D>, IEquatable<Coordinate2D>
    {
        public int X;
        public int Y;
        
        public Coordinate2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public static Coordinate2D operator +(Coordinate2D coord, Vector2Int vec)
        {
            return new Coordinate2D(coord.X + vec.x, coord.Y + vec.y);
        }

        public static Coordinate2D operator -(Coordinate2D coord, Vector2Int vec)
        {
            return new Coordinate2D(coord.X - vec.x, coord.Y - vec.y);
        }

        public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b)
        {
            return new Coordinate2D(a.X + b.X, a.Y + b.Y);
        }

        public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b)
        {
            return new Coordinate2D(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public int CompareTo(Coordinate2D other)
        {
            var xComparison = X.CompareTo(other.X);
            if (xComparison != 0) return xComparison;
            return Y.CompareTo(other.Y);
        }

        public bool Equals(Coordinate2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate2D other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}