using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class WeightedSingleSampler<T> : SingleSampler<T> where T : Weighted
    {
        public override IReadOnlyCollection<T> Samples => _weightedSamples.ReadonlyKeys;

        private WeightedList<T> _weightedSamples;
        private Random _random;

        public WeightedSingleSampler(Random random)
        {
            _weightedSamples = new WeightedList<T>(random);
        }
        
        public override void UpdateSamples(IList<T> samples)
        {
            _weightedSamples.Clear();
            for (int i = 0; i < samples.Count; i++)
            {
                T sample = samples[i];
                _weightedSamples.Add(sample, sample.Weight);
            }
        }

        public override T Sample()
        {
            return _weightedSamples.GetRandomItem();
        }

        public override List<T> Sample(int amount)
        {
            List<T> result = new List<T>(amount);
            
            for (int i = 0; i < amount; i++)
            {
                result.Add(_weightedSamples.GetRandomItem());
            }

            return result;
        }
    }
}