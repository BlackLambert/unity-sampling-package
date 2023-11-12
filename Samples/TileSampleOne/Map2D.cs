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

        private System.Random _random;
        private int _currentSeed;

        private void Start()
        {
            _currentSeed = _seed;
            _random = new System.Random(_seed);
            if (_randomizeSeed)
            {
                StartCoroutine(CreateRandomMap());
            }
            else
            {
                CreateMap();
            }
        }

        private IEnumerator CreateRandomMap()
        {
            _currentSeed = _random.Next();
            CreateMap();
            yield return new WaitForSeconds(_randomizeDelay);
            StartCoroutine(CreateRandomMap());
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
            System.Random random = new System.Random(_currentSeed);
            WeightedSingleSampler<MapTileSettings> baseSampler = new WeightedSingleSampler<MapTileSettings>(random);
            BasicTileSampler<MapTileSettings> tilesSampler = new BasicTileSampler<MapTileSettings>(baseSampler);
            tilesSampler.UpdateDomain(_tileSet.Tiles);
            Grid2D<MapTileSettings> tiles = tilesSampler.Sample(_width, _height);
            CreateMap(tiles);
        }

        private void CreateNeighbourConstraintMap()
        {
            System.Random random = new System.Random(_currentSeed);
            WeightedSingleSampler<MapTileSettings> baseSampler = new WeightedSingleSampler<MapTileSettings>(random);
            TileNeighbourConstraint<MapTileSettings> constraint = new TileNeighbourConstraint<MapTileSettings>();
            ConstraintTileSampler<MapTileSettings, MapTileValidationContext> tilesSampler = 
                new ConstraintTileSampler<MapTileSettings, MapTileValidationContext>(baseSampler, constraint);
            tilesSampler.UpdateDomain(_tileSet.Tiles);
            Grid2D<MapTileSettings> tiles = tilesSampler.Sample(_width, _height);
            CreateMap(tiles);
        }

        private void CreateMap(Grid2D<MapTileSettings> tiles)
        {
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    MapTileSettings tileSetting = tiles[column, row];
                    MapTile mapTile = Instantiate(mapTilePrefab, _tileHook);
                    mapTile.SetSprite(tileSetting.Sprite);
                    mapTile.RectTransform.localPosition =
                        new Vector3(column * MapTileSettings.Width, row * MapTileSettings.Height, 0);
                }
            }
        }
    }
}
