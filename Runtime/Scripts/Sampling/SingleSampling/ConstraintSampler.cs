using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class ConstraintSampler<T> : Sampler<T>
    {
        public bool HasSample => _domain.Count > 0;
        
        private readonly Sampler<T> _baseSingleSampler;
        private Constraint<T> _constraint;
        private List<T> _constraintDomain = new List<T>();
        private List<T> _domain = new List<T>();

        public ConstraintSampler(
            Sampler<T> baseSingleSampler,
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

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }

        private void UpdateBaseSampler()
        {
            UpdateConstraintDomain();
            ValidateNotEmpty(_constraintDomain);
            _baseSingleSampler.UpdateDomain(_constraintDomain);
        }

        private void UpdateConstraintDomain()
        {
            _constraintDomain.Clear();

            foreach (T sample in _domain)
            {
                if (_constraint.IsValid(sample))
                {
                    _constraintDomain.Add(sample);
                }
            }
        }

        private void ValidateNotEmpty(IReadOnlyCollection<T> items)
        {            
            if (items.Count == 0)
            {
                throw new InvalidOperationException("There are no valid items!");
            }
        }
    }
}
