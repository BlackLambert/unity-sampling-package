using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
    public abstract class BoundsSettings2D : ScriptableObject
    {
        public abstract Bounds<Vector2> GetBounds();
        public abstract Vector2 GetCenter();
    }
}
