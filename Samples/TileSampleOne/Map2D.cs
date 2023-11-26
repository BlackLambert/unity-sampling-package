using System;
using System.Collections;
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
        [SerializeField] private MapTile mapTilePrefab;
        [SerializeField] private bool _useNeighbourConstraint = true;
        [SerializeField] private bool _randomizeSeed = true;
        [SerializeField] private float _randomizeDelay = 5;
        [SerializeField] private MapTileSettings _defaultTile;

        private MapTile[,] _tiles;
        private System.Random _random;
        private int _currentSeed;

        private void Start()
        {
            _currentSeed = _seed;
            _random = new System.Random(_seed);
            InitMap();
            
            if (_randomizeSeed)
            {
                InvokeRepeating(nameof(CreateRandomMap), 0, _randomizeDelay);
            }
            else
            {
                CreateMap();
            }
        }

        private void InitMap()
        {
            _tiles = new MapTile[_height, _width];
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    MapTileSettings tileSetting = _defaultTile;
                    MapTile mapTile = Instantiate(mapTilePrefab, _tileHook);
                    mapTile.SetSprite(tileSetting.Sprite);
                    mapTile.RectTransform.localPosition =
                        new Vector3(column * MapTileSettings.TileWidth, row * MapTileSettings.TileHeight, 0);
                    _tiles[row, column] = mapTile;
                }
            }
        }

        private void CreateRandomMap()
        {
            _currentSeed = _random.Next();
            CreateMap();
        }

        private void CreateMap()
        {
            if (_useNeighbourConstraint)
            {
                CreateNeighbourConstraintMap();
            }
            else
            {
                CreateBasicMap();
            }
        }

        private void CreateBasicMap()
        {
            TileSampler<MapTileSettings> tilesSampler = 
                TileSamplerFactory.CreateWeightedBasicSampler(_currentSeed, _tileSet.Tiles);
            Grid2D<MapTileSettings> tiles = tilesSampler.Sample(_width, _height);
            UpdateMap(tiles);
        }

        private void CreateNeighbourConstraintMap()
        {
            TileSampler<MapTileSettings> tilesSampler = 
                TileSamplerFactory.CreateWeightedNeighborConstraintSamplerWithPrioritizedSelector(_currentSeed, _tileSet.Tiles, _tileSet.DefaultTile);
            Grid2D<MapTileSettings> tiles = tilesSampler.Sample(_width, _height);
            UpdateMap(tiles);
        }

        private void UpdateMap(Grid2D<MapTileSettings> tiles)
        {
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    _tiles[row, column].SetSprite(tiles[column, row].Sprite);
                }
            }
        }
    }
}
