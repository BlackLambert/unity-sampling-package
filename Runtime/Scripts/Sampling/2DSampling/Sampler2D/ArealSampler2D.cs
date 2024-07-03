using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class ArealSampler2D<T> : Sampler2DBase<T>
    {
        private readonly TileInfo _tileInfo;
        private readonly Sampler<T> _baseSampler;
        private readonly List<Coordinate2D> _missingCoordinates = new();
        private readonly Dictionary<T, List<Coordinate2D>> _sampleToPossibleCoordinates = new();
        private readonly List<T> _validSamples = new();
        private readonly Sampler<Coordinate2D> _startCoordinatesSampler;

        public ArealSampler2D(
            TileInfo tileInfo,
            Sampler<T> baseSampler,
            Sampler<Coordinate2D> startCoordinatesSampler,
            Sampler<Coordinate2D> coordinateSampler,
            Grid2DCoordinateFactory grid2DCoordinateFactory) :
            base(coordinateSampler, grid2DCoordinateFactory)
        {
            _tileInfo = tileInfo;
            _baseSampler = baseSampler;
            _startCoordinatesSampler = startCoordinatesSampler;
        }

        protected override void InitSampling(List<Coordinate2D> coordinates)
        {
            InitMissingCoordinates(coordinates);
            InitSampleToCoordinates();
            InitSamples();
        }

        protected override Func<SampleStep2D<T>> GetSampleNextFunction(Grid2D<T> grid2D) => () => SampleNext(grid2D);
        protected override Func<bool> GetHasNextSampleFunction() => () => _missingCoordinates.Count > 0;

        private void InitMissingCoordinates(List<Coordinate2D> coordinates)
        {
            _missingCoordinates.Clear();
            _missingCoordinates.AddRange(coordinates);
        }

        private void InitSampleToCoordinates()
        {
            _sampleToPossibleCoordinates.Clear();
            foreach (T element in _domain)
            {
                _sampleToPossibleCoordinates[element] = null;
            }
        }

        private void InitSamples()
        {
            _validSamples.Clear();
            _validSamples.AddRange(_domain);
            _baseSampler.UpdateDomain(_validSamples);
        }

        private SampleStep2D<T> SampleNext(Grid2D<T> grid2D)
        {
            T sample = _baseSampler.Sample();
            List<Coordinate2D> possibleCoordinates = _sampleToPossibleCoordinates[sample];
            Sampler<Coordinate2D> coordinateSampler;

            if (possibleCoordinates == null)
            {
                possibleCoordinates = new List<Coordinate2D>();
                _sampleToPossibleCoordinates[sample] = possibleCoordinates;
                _startCoordinatesSampler.UpdateDomain(_missingCoordinates);
                coordinateSampler = _startCoordinatesSampler;
            }
            else
            {
                _coordinateSampler.UpdateDomain(possibleCoordinates);
                coordinateSampler = _coordinateSampler;
            }

            Coordinate2D coordinate = coordinateSampler.Sample();
            Coordinate2D[] neighbors = _tileInfo.GetNeighborCoordinates(coordinate);
            _missingCoordinates.Remove(coordinate);
            UpdatePossibleCoordinates(possibleCoordinates, neighbors);
            RemoveInvalidCoordinates(coordinate);
            Coordinate2D gridCoordinate = _grid2DCoordinateFactory.ToGridCoordinate(coordinate);
            return new SampleStep2D<T>(sample, gridCoordinate);
        }

        private void UpdatePossibleCoordinates(List<Coordinate2D> possibleCoordinates,
            Coordinate2D[] neighbors)
        {
            foreach (Coordinate2D neighbor in neighbors)
            {
                if (_missingCoordinates.Contains(neighbor) && !possibleCoordinates.Contains(neighbor))
                {
                    possibleCoordinates.Add(neighbor);
                }
            }
        }

        private void RemoveInvalidCoordinates(Coordinate2D coordinate)
        {
            foreach (KeyValuePair<T, List<Coordinate2D>> pair in _sampleToPossibleCoordinates)
            {
                RemoveFrom(pair.Key, pair.Value, coordinate);
            }
        }

        private void RemoveFrom(T sample, List<Coordinate2D> coordinates, Coordinate2D coordinate)
        {
            if (coordinates != null && coordinates.Remove(coordinate) && coordinates.Count == 0)
            {
                _validSamples.Remove(sample);
                _baseSampler.UpdateDomain(_validSamples);
            }
        }
    }
}