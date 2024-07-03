using System;
using System.Collections;
using UnityEngine;

namespace SBaier.Sampling.Samples.HexTileSample
{
    public class Map2D : MonoBehaviour
    {
        [SerializeField] private MapTileSet _tileSet;
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        [SerializeField] private Transform _tileHook;
        [SerializeField] private int _seed = 1000;
        [SerializeField] private MapTile mapTilePrefab;
        [SerializeField] private Type _type = Type.Basic;
        [SerializeField] private bool _randomizeSeed = true;
        [SerializeField] private float _randomizeDelay = 5;
        [SerializeField] private float _delayBetweenTiles = 0.1f;
        [SerializeField] private HexGridIndentation _gridIndentation;
        [SerializeField] private HexTileRotation _tileRotation;
        [SerializeField] private MapTileSettings _defaultTile;

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
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    _tiles[row, column] = CreateTile(_defaultTile, new Coordinate2D(column, row));
                }
            }
        }

        private void ResetMap()
        {
            Sprite defaultSprite = _defaultTile.Sprite;
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    _tiles[row, column].SetSprite(defaultSprite);
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
            Sampler2D<MapTileSettings> sampler = CreateSampler();
            StartCoroutine(CreateMap(sampler));
        }

        private Sampler2D<MapTileSettings> CreateSampler()
        {
            return _type switch
            {
                Type.Basic => _factory.CreateWeightedBasicHexSampler(new Seed(_currentSeed), _tileSet.Tiles, _tileRotation, _gridIndentation),
                Type.NeighborConstraint => _factory.CreateWeightedNeighborConstraintSamplerWithPrioritizedHexSelector(
                    new Seed(_currentSeed), _tileSet.Tiles, _tileSet.defaultTile, _tileRotation, _gridIndentation),
                Type.Areal => _factory.CreateArealSampler(new Seed(_currentSeed), _tileSet.Tiles, _tileRotation, _gridIndentation),
                _ => throw new ArgumentOutOfRangeException()
            };
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

        private MapTile CreateTile(MapTileSettings settings, Coordinate2D gridCoordinate)
        {
            MapTile mapTile = Instantiate(mapTilePrefab, _tileHook);
            mapTile.SetSprite(settings.Sprite);
            mapTile.transform.localPosition = GetScreenPosition(gridCoordinate);
            if (_tileRotation == HexTileRotation.PointyTop)
            {
                mapTile.transform.Rotate(Vector3.forward, 30);
            }
            return mapTile;
        }

        private Vector3 GetScreenPosition(Coordinate2D gridCoordinate)
        {
            return _tileRotation.GetScreenPosition(_gridIndentation, gridCoordinate, MapTileSettings.TileMax);
        }

        public enum Type
        {
            Basic = 0,
            NeighborConstraint = 1,
            Areal = 2
        }
    }
}
