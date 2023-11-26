using System;

namespace PCGToolkit.Sampling
{
    public struct Coordinate2D : IComparable<Coordinate2D>, IEquatable<Coordinate2D>
    {
        public int X;
        public int Y;


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