using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    public class Map2D : MonoBehaviour
    {
        [SerializeField] private MapTileSet _tileSet;
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        [SerializeField] private RectTransform _tileHook;
        [SerializeField] private int _seed = 1000;
        [SerializeField] private Tile _tilePrefab;

        private void Start()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            System.Random random = new System.Random(_seed);
            WeightedTileSampler<MapTileSettings> tilesSampler = new WeightedTileSampler<MapTileSettings>(random);
            tilesSampler.UpdateDomain(_tileSet.Tiles);
            Grid2D<MapTileSettings> tiles = tilesSampler.Sample(_width, _height);

            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    MapTileSettings tileSetting = tiles[column, row];
                    Tile tile = Instantiate(_tilePrefab, _tileHook);
                    tile.SetSprite(tileSetting.Sprite);
                    tile.RectTransform.localPosition =
                        new Vector3(column * MapTileSettings.Width, row * MapTileSettings.Height, 0);
                }
            }
        }
    }
}
