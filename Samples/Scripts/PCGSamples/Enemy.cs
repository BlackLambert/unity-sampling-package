using System;
using PCG.Toolkit;
using UnityEngine;

namespace PCG.Tests
{
    [Serializable]
    public class Enemy : Weighted
    {
        [field:SerializeField]
        public string ID { get; set; }
        [field:SerializeField]
        public float Weight { get; set; }
        [field:SerializeField]
        public int Power { get; set; }
        [field:SerializeField]
        public ESize Size { get; set; }


        public override string ToString()
        {
            return $"{ID} (Weight: {Weight} | Power: {Power} | Size: {Size}) ";
        }

        protected bool Equals(Enemy other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Enemy)obj);
        }

        public override int GetHashCode()
        {
            return (ID != null ? ID.GetHashCode() : 0);
        }

        [Flags]
        public enum ESize
        {
            None = 0,
            Small = 1,
            Medium = 2,
            Big = 4
        }
    }
}