using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class ConstraintTileSampler<TSample, TContext> : TileSampleBase<TSample>
        where TSample : Tile
        where TContext : TileSamplingValidationContext<TSample>, new()
    {
        private SingleSampler<TSample> _baseSampler;
        private Constraint<TContext> _constraint;
        private List<TSample> _constraintDomain = new List<TSample>();
        private TSample _defaultSample = default;
        private bool _useDefaultSample = false;

        public ConstraintTileSampler(
            Selector<Coordinate2D> selector,
            SingleSampler<TSample> baseSampler,
            Constraint<TContext> constraint) : base(selector)
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

        public void UpdateConstraint(Constraint<TContext> constraint)
        {
            _constraint = constraint;
        }

        protected override Func<SampleStep2D<TSample>> GetSampleNextFunction(Grid2D<TSample> grid)
        {
            TContext context = new TContext();
            context.Grid = grid;
            return () => SampleNext(context);
        }

        private SampleStep2D<TSample> SampleNext(TContext context)
        {
            Coordinate2D coordinate = _selector.GetNext();
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
            return new SampleStep2D<TSample>(sample, coordinate);
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