using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Sampling.Examples
{
    public abstract class BoundsSettings3D : ScriptableObject
    {
        public abstract Bounds<Vector3> GetBounds();
        public abstract Vector3 GetCenter();
    }
}
