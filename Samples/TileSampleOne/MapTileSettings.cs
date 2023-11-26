using System;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    [CreateAssetMenu(fileName = "MapTileSettings", menuName = "ScriptableObjects/MapTileSettings", order = 1)]
    public class MapTileSettings : ScriptableObject, Tile
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

        int Tile.GetSocketIdFor(TileSide side)
        {
            return side switch
            {
                TileSide.Top => (int)TopSocket,
                TileSide.TopRight => (int)TopRightSocket,
                TileSide.Right => (int)RightSocket,
                TileSide.BottomRight => (int)BottomRightSocket,
                TileSide.Bottom => (int)BottomSocket,
                TileSide.BottomLeft => (int)BottomLeftSocket,
                TileSide.Left => (int)LeftSocket,
                TileSide.TopLeft => (int)TopLeftSocket,
                _ => throw new InvalidOperationException()
            };
        }

        public override string ToString()
        {
            return Sprite.name;
        }
    }
}
