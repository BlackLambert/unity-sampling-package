using System;
using UnityEngine;

namespace SBaier.Sampling.Samples.HexTileSample
{
    [CreateAssetMenu(fileName = "MapTileSettings", menuName = "ScriptableObjects/HexMapTileSettings", order = 1)]
    public class MapTileSettings : ScriptableObject, NeighborConstraintTile, Weighted
    {
        public const float TileMax = 2.29f;
        
        [field: SerializeField] public float Weight { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, Header("Constraints")] public Socket TopRightSocket { get; private set; }
        [field: SerializeField] public Socket RightSocket { get; private set; }
        [field: SerializeField] public Socket BottomRightSocket { get; private set; }
        [field: SerializeField] public Socket BottomLeftSocket { get; private set; }
        [field: SerializeField] public Socket LeftSocket { get; private set; }
        [field: SerializeField] public Socket TopLeftSocket { get; private set; }

        int NeighborConstraintTile.GetSocketIdFor(int sideId)
        {
            return sideId switch
            {
                0 => (int)TopRightSocket,
                1 => (int)RightSocket,
                2 => (int)BottomRightSocket,
                3 => (int)BottomLeftSocket,
                4 => (int)LeftSocket,
                5 => (int)TopLeftSocket,
                _ => throw new InvalidOperationException()
            };
        }

        public override string ToString()
        {
            return Sprite.name;
        }
    }
}
