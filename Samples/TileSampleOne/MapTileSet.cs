using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    [CreateAssetMenu(fileName = "MapTileSet", menuName = "ScriptableObjects/MapTileSet", order = 1)]
    public class MapTileSet : ScriptableObject
    {
        [field: SerializeField] public List<MapTileSettings> Tiles { get; private set; } = new List<MapTileSettings>();
    }
}
