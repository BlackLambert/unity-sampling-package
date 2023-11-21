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
        [field: SerializeField] public Socket RightSocket { get; private set; }
        [field: SerializeField] public Socket BottomSocket { get; private set; }
        [field: SerializeField] public Socket LeftSocket { get; private set; }

        int Tile.TopSocketId => (int)TopSocket;
        int Tile.RightSocketId => (int)RightSocket;
        int Tile.BottomSocketId => (int)BottomSocket;
        int Tile.LeftSocketId => (int)LeftSocket;

        public override string ToString()
        {
            return Sprite.name;
        }
    }
}
