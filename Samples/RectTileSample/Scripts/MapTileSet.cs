using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Sampling.Examples.TileSampleOne
{
    [CreateAssetMenu(fileName = "MapTileSet", menuName = "ScriptableObjects/RectMapTileSet", order = 1)]
    public class MapTileSet : ScriptableObject
    {
        [field: SerializeField] public List<MapTileSettings> Tiles { get; private set; } = new List<MapTileSettings>();
        [field: SerializeField] public MapTileSettings defaultTile = null;
    }
}
