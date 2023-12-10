using UnityEngine;

namespace PCGToolkit.Sampling
{
    public interface Bounds2D
    {
        bool Contains(Vector2 point);
    }
}
