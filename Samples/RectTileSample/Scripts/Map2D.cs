using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace SBaier.Sampling.Examples.TileSampleOne
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
        [SerializeField] private MapTileSettings defaultTile;
        [SerializeField] private float _delayBetweenTiles = 0.1f;

        private MapTile[,] _tiles;
        private System.Random _random;
        private int _currentSeed;
        private TileSamplerFactory _factory = new TileSamplerFactory();

        private void Start()
        {
            _currentSeed = _seed;
            _random = new System.Random(_seed);
            InitMap();
            CreateRandomMap();
        }

        private void InitMap()
        {
            _tiles = new MapTile[_height, _width];
            MapTileSettings tileSetting = defaultTile;
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    MapTile mapTile = Instantiate(mapTilePrefab, _tileHook);
                    mapTile.SetSprite(tileSetting.Sprite);
                    mapTile.RectTransform.localPosition =
                        new Vector3(column * MapTileSettings.TileWidth, row * MapTileSettings.TileHeight, 0);
                    _tiles[row, column] = mapTile;
                }
            }
        }

        private void ResetMap()
        {
            MapTileSettings tileSetting = defaultTile;
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    _tiles[row, column].SetSprite(tileSetting.Sprite);
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
                Sampler2D<MapTileSettings> sampler2D = 
                    _factory.CreateWeightedNeighborConstraintSamplerWithPrioritizedSelector(
                        new Seed(_currentSeed),
                        _tileSet.Tiles, 
                        _tileSet.defaultTile);
                StartCoroutine(CreateMap(sampler2D));
            }
            else
            {
                Sampler2D<MapTileSettings> sampler2D = 
                    _factory.CreateWeightedBasicSampler(new Seed(_currentSeed), _tileSet.Tiles);
                StartCoroutine(CreateMap(sampler2D));
            }
        }

        private IEnumerator CreateMap(Sampler2D<MapTileSettings> sampler2D)
        {
            Sampling2DHandle<MapTileSettings> handle = sampler2D.Sample(_width, _height);
            while (!handle.IsFinished)
            {
                SampleStep2D<MapTileSettings> step = handle.ExecuteNextStep();
                _tiles[step.GridCoordinate.Y, step.GridCoordinate.X].SetSprite(step.Item.Sprite);
                yield return new WaitForSeconds(_delayBetweenTiles);
            }

            StartCoroutine(HandleMapDrawFinished());
        }

        private IEnumerator HandleMapDrawFinished()
        {
            if (_randomizeSeed)
            {
                yield return new WaitForSeconds(_randomizeDelay);
                ResetMap();
                CreateRandomMap();
            }
        }
    }
}
