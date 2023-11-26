using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class ConstraintTileSampler<TSample, TContext> : TileSampler<TSample>
        where TSample : Tile
        where TContext : TileSamplingValidationContext<TSample>, new()
    {
        public IReadOnlyCollection<TSample> Domain => _domain;
        private Selector<Coordinate2D> _selector;
        private SingleSampler<TSample> _baseSampler;
        private Constraint<TContext> _constraint;
        private List<TSample> _domain = new List<TSample>();
        private List<TSample> _constraintDomain = new List<TSample>();
        private TSample _defaultSample = default;
        private bool _useDefaultSample = false;

        public ConstraintTileSampler(
            Selector<Coordinate2D> selector,
            SingleSampler<TSample> baseSampler,
            Constraint<TContext> constraint)
        {
            _selector = selector;
            _baseSampler = baseSampler;
            _constraint = constraint;
        }
        
        public ConstraintTileSampler(
            Selector<Coordinate2D> selector,
            SingleSampler<TSample> baseSampler,
            Constraint<TContext> constraint,
            TSample defaultSample) : this (selector, baseSampler, constraint)
        {
            _defaultSample = defaultSample;
            _useDefaultSample = true;
        }

        public void UpdateDomain(IList<TSample> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }

        public void UpdateConstraint(Constraint<TContext> constraint)
        {
            _constraint = constraint;
        }

        public Grid2D<TSample> Sample(int size)
        {
            return Sample(size, size);
        }

        public Grid2D<TSample> Sample(int width, int height)
        {
            TContext context = new TContext();
            context.Grid = new Grid2D<TSample>(width, height);
            _selector.Init(CreateCoordinates(width, height));

            while (_selector.HasNext())
            {
                SampleAt(context, _selector.GetNext());
            }
            
            return context.Grid;
        }

        private void SampleAt(TContext context, Coordinate2D coordinate)
        {
            context.CurrentSampleXCoordinate = coordinate.X;
            context.CurrentSampleYCoordinate = coordinate.Y;
            UpdateConstraintDomain(context);
            bool hasDomainElements = _constraintDomain.Count > 0;
            
            if (!hasDomainElements && !_useDefaultSample)
            {
                throw new InvalidOperationException($"No sample found for (x: {coordinate.X} | y: {coordinate.Y})");
            }

            _baseSampler.UpdateDomain(_constraintDomain);
            TSample sample = hasDomainElements ? _baseSampler.Sample() : _defaultSample;
            context.Grid[coordinate.X, coordinate.Y] = sample;
        }

        private ICollection<Coordinate2D> CreateCoordinates(int width, int height)
        {
            List<Coordinate2D> result = new List<Coordinate2D>(width * height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Add(new Coordinate2D { X = x, Y = y });
                }
            }

            return result;
        }

        private void UpdateConstraintDomain(TContext context)
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