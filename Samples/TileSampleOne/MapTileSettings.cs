using System;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    [Serializable]
    public class MapTileSettings : Weighted
    {
        public const float Height = 16;
        public const float Width = 16;
        
        [field: SerializeField] public float Weight { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public override string ToString()
        {
            return Sprite.name;
        }
    }
}
