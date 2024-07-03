using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class ConstraintSampler2D<TSample> : Sampler2DBase<TSample>
        where TSample : Tile
    {
        private Sampler<TSample> _baseSampler;
        private Constraint<TileSamplingContext<TSample>> _constraint;
        private List<TSample> _constraintDomain = new List<TSample>();
        private TSample _defaultSample = default;
        private bool _useDefaultSample = false;
        private readonly TileInfo _tileInfo;

        public ConstraintSampler2D(
            TileInfo tileInfo,
            Sampler<Coordinate2D> coordinateSampler,
            Sampler<TSample> baseSampler,
            Constraint<TileSamplingContext<TSample>> constraint, 
            Grid2DCoordinateFactory grid2DCoordinateFactory) : 
            base(coordinateSampler, grid2DCoordinateFactory)
        {
            _baseSampler = baseSampler;
            _constraint = constraint;
            _tileInfo = tileInfo;
        }
        
        public ConstraintSampler2D(
            TileInfo tileInfo,
            Sampler<Coordinate2D> coordinateSampler,
            Sampler<TSample> baseSampler,
            Constraint<TileSamplingContext<TSample>> constraint,
            TSample defaultSample, 
            Grid2DCoordinateFactory grid2DCoordinateFactory) : 
            this (tileInfo, coordinateSampler, baseSampler, constraint, grid2DCoordinateFactory)
        {
            _defaultSample = defaultSample;
            _useDefaultSample = true;
        }

        protected override void InitSampling(List<Coordinate2D> coordinates)
        {
            _coordinateSampler.UpdateDomain(coordinates);
            _constraintDomain.Clear();
        }

        protected override Func<SampleStep2D<TSample>> GetSampleNextFunction(Grid2D<TSample> grid)
        {
            BasicTileSamplingContext<TSample> context = new BasicTileSamplingContext<TSample>
            {
                TileInfo = _tileInfo,
                Grid = grid
            };
            return () => SampleNext(context);
        }

        protected override Func<bool> GetHasNextSampleFunction() => () => _coordinateSampler.HasSample;

        private SampleStep2D<TSample> SampleNext(BasicTileSamplingContext<TSample> context)
        {
            Coordinate2D coordinate = _coordinateSampler.Sample();
            context.CurrentSampleCoordinate = coordinate;
            UpdateConstraintDomain(context);
            bool hasDomainElements = _constraintDomain.Count > 0;
            
            if (!hasDomainElements && !_useDefaultSample)
            {
                throw new InvalidOperationException($"No sample found for (x: {coordinate.X} | y: {coordinate.Y})");
            }

            _baseSampler.UpdateDomain(_constraintDomain);
            TSample sample = hasDomainElements ? _baseSampler.Sample() : _defaultSample;
            return new SampleStep2D<TSample>(sample, _grid2DCoordinateFactory.ToGridCoordinate(coordinate));
        }

        private void UpdateConstraintDomain(BasicTileSamplingContext<TSample> context)
        {
            _constraintDomain.Clear();

            foreach (TSample sample in _domain)
            {
                context.CurrentDomainElementToValidate = sample;
                if (_constraint.IsValid(context))
                {
                    _constraintDomain.Add(sample);
                }
            }
        }
    }
}