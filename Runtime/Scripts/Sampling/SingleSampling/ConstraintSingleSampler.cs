using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class ConstraintSingleSampler<T> : SingleSampler<T>
    {
        public IReadOnlyCollection<T> Domain => _samples;

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

        public T Sample()
        {
            UpdateBaseSampler();
            return _baseSingleSampler.Sample();
        }

        public List<T> Sample(int amount)
        {
            UpdateBaseSampler();
            return _baseSingleSampler.Sample(amount);
        }
        
        public void UpdateDomain(IList<T> domain)
        {
            _samples.Clear();
            _samples.AddRange(domain);
        }

        private void UpdateBaseSampler()
        {
            UpdateConstraintSamples();
            ValidateItemsNotEmpty(_constraintSamples);
            _baseSingleSampler.UpdateDomain(_constraintSamples);
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
