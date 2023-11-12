using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class WeightedSingleSampler<T> : WeightedSampler<T>, SingleSampler<T> where T : Weighted
    {
        public WeightedSingleSampler(Random random) : base(random)
        {
            
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
    }
}