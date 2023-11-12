using System;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling
{
    public class ConstraintTileSampler<TSample, TContext> : TileSampler<TSample> 
        where TSample : Tile 
        where TContext : TileSamplingValidationContext<TSample>, new()
    {
        public IReadOnlyCollection<TSample> Domain => _domain;
        private SingleSampler<TSample> _baseSampler;
        private Constraint<TContext> _constraint;
        private List<TSample> _domain = new List<TSample>();
        private List<TSample> _constraintDomain = new List<TSample>();
        
        public ConstraintTileSampler(
            SingleSampler<TSample> baseSampler,
            Constraint<TContext> constraint)
        {
            _baseSampler = baseSampler;
            _constraint = constraint;
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
            Grid2D<TSample> result = new Grid2D<TSample>(width, height);
            context.Grid = result;
            int tilesAmount = width * height;

            for (int i = 0; i < tilesAmount; i++)
            {
                int column = i % height;
                int row = i / height;

                context.CurrentSampleXCoordinate = column;
                context.CurrentSampleYCoordinate = row;
                UpdateConstraintDomain(context);

                if (_constraintDomain.Count == 0)
                {
                    throw new InvalidOperationException($"No sample found for (x: {column} | y: {row})");
                }
                
                _baseSampler.UpdateDomain(_constraintDomain);
                TSample sample = _baseSampler.Sample();
                result[column, row] = sample;
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
