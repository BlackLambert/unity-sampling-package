using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public abstract class WeightedSampler<T> : Sampler<T> where T : Weighted
    {
        public IReadOnlyCollection<T> Domain => _weightedDomain.ReadonlyKeys;

        protected readonly WeightedList<T> _weightedDomain;

        public WeightedSampler(Random random)
        {
            _weightedDomain = new WeightedList<T>(random);
        }

        public void UpdateDomain(IList<T> domain)
        {
            _weightedDomain.Clear();
            for (int i = 0; i < domain.Count; i++)
            {
                T sample = domain[i];
                _weightedDomain.Add(sample, sample.Weight);
            }
        }
    }
}