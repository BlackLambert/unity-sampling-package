using System;
using UnityEngine;

namespace SBaier.Sampling.Examples.TileSampleOne
{
    [CreateAssetMenu(fileName = "MapTileSettings", menuName = "ScriptableObjects/RectMapTileSettings", order = 1)]
    public class MapTileSettings : ScriptableObject, NeighborConstraintTile, Weighted
    {
        public const float TileHeight = 16;
        public const float TileWidth = 16;
        
        [field: SerializeField] public float Weight { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, Header("Constraints")] public Socket TopSocket { get; private set; }
        [field: SerializeField] public Socket TopRightSocket { get; private set; }
        [field: SerializeField] public Socket RightSocket { get; private set; }
        [field: SerializeField] public Socket BottomRightSocket { get; private set; }
        [field: SerializeField] public Socket BottomSocket { get; private set; }
        [field: SerializeField] public Socket BottomLeftSocket { get; private set; }
        [field: SerializeField] public Socket LeftSocket { get; private set; }
        [field: SerializeField] public Socket TopLeftSocket { get; private set; }

        int NeighborConstraintTile.GetSocketIdFor(int sideId)
        {
            return sideId switch
            {
                0 => (int)TopSocket,
                1 => (int)TopRightSocket,
                2 => (int)RightSocket,
                3 => (int)BottomRightSocket,
                4 => (int)BottomSocket,
                5 => (int)BottomLeftSocket,
                6 => (int)LeftSocket,
                7 => (int)TopLeftSocket,
                _ => throw new InvalidOperationException()
            };
        }

        public override string ToString()
        {
            return Sprite.name;
        }
    }
}
