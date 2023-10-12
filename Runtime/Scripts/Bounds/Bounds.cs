using UnityEngine;

namespace PCGToolkit.Sampling
{
    public interface Bounds
    {
        bool Contains(Vector3 point);
    }
}
