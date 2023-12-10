using System;
using UnityEngine;

namespace PCGToolkit.Sampling
{
    public interface Bounds3D
    {
        bool Contains(Vector3 point);
    }
}
