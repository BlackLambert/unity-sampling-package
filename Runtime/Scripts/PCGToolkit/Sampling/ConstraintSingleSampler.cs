using System;
using System.Collections.Generic;

namespace PCG.Toolkit
{
    public class ConstraintSingleSampler<T> : SingleSampler<T>
    {
        public override IReadOnlyCollection<T> Samples => _samples;

        private readonly SingleSampler<T> _baseSingleSampler;
        private Constraint<T> _constraint;
        private List<T> _constraintSamples = new List<T>();
        private List<T> _samples = new List<T>();

        public ConstraintSingleSampler(
            SingleSampler<T> baseSingleSampler,
            Constraint<T> constraint)
        {
            _baseSingleSampler = baseSingleSampler;
            _constraint = constraint;
        }

        public void UpdateConstraint(Constraint<T> constraint)
        {
            _constraint = constraint;
        }

        public override T Sample()
        {
            UpdateBaseSampler();
            return _baseSingleSampler.Sample();
        }

        public override List<T> Sample(int amount)
        {
            UpdateBaseSampler();
            return _baseSingleSampler.Sample(amount);
        }
        
        public override void UpdateSamples(IList<T> samples)
        {
            _samples.Clear();
            _samples.AddRange(samples);
        }

        private void UpdateBaseSampler()
        {
            UpdateConstraintSamples();
            ValidateItemsNotEmpty(_constraintSamples);
            _baseSingleSampler.UpdateSamples(_constraintSamples);
        }

        private void UpdateConstraintSamples()
        {
            _constraintSamples.Clear();

            foreach (T sample in _samples)
            {
                if (_constraint.IsValid(sample))
                {
                    _constraintSamples.Add(sample);
                }
            }
        }

        private void ValidateItemsNotEmpty(IReadOnlyCollection<T> items)
        {            
            if (items.Count == 0)
            {
                throw new InvalidOperationException("There are no valid items!");
            }
        }
    }
}
