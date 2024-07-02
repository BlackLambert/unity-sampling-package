using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class WeightedSampler<T> : Sampler<T> where T : Weighted
    {
        public bool HasSample => _weightedDomain.Count > 0;
        
        protected readonly WeightedList<T> _weightedDomain;

        public WeightedSampler(Random random)
        {
            _weightedDomain = new WeightedList<T>(random);
        }

        public T Sample()
        {
            return _weightedDomain.GetRandomItem();
        }

        public List<T> Sample(int amount)
        {
            List<T> result = new List<T>(amount);
            
            for (int i = 0; i < amount; i++)
            {
                result.Add(_weightedDomain.GetRandomItem());
            }

            return result;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _weightedDomain.Clear();
            foreach (T sample in domain)
            {
                _weightedDomain.Add(sample, sample.Weight);
            }
        }
    }
}