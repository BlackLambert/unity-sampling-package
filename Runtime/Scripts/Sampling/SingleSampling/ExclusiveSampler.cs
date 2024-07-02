using System;
using System.Collections.Generic;
using System.Linq;

namespace SBaier.Sampling
{
    /// <summary>
    /// Selects samples based on a defined domain.
    /// Every element in the domain can only sampled once
    /// Throws an exception if there is no element left to sample
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExclusiveSampler<T> : Sampler<T>
    {
        public bool HasSample => _domain.Count > 0;
        
        private Sampler<T> _baseSampler;
        private List<T> _domain = new List<T>();
        
        public ExclusiveSampler(Sampler<T> baseSampler)
        {
            _baseSampler = baseSampler;
        }
        
        public T Sample()
        {
            ValidateAmount(1);
            T sample = _baseSampler.Sample();
            RemoveSampleFromDomain(sample);
            return sample;
        }

        public List<T> Sample(int amount)
        {
            ValidateAmount(amount);
            List<T> samples = _baseSampler.Sample(amount);
            RemoveSamplesFromDomain(samples);
            return samples;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
            _baseSampler.UpdateDomain(_domain);
        }

        private void RemoveSampleFromDomain(T sample)
        {
            _domain.Remove(sample);
            _baseSampler.UpdateDomain(_domain);
        }

        private void RemoveSamplesFromDomain(List<T> samples)
        {
            foreach (T sample in samples)
            {
                _domain.Remove(sample);
            }

            _baseSampler.UpdateDomain(_domain);
        }

        private void ValidateAmount(int amount)
        {
            if (amount > _domain.Count)
            {
                throw new ArgumentException("Failed to select samples. No samples left.");
            }
        }
    }
}