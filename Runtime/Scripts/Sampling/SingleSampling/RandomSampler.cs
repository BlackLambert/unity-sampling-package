using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class RandomSampler<T> : Sampler<T>
    {
        public bool HasSample => _domain.Count > 0;
        
        private readonly List<T> _domain = new List<T>();
        private readonly Random _random;

        public RandomSampler(Random random)
        {
            _random = random;
        }
        
        public T Sample()
        {
            return _domain[_random.Next(_domain.Count)];
        }

        public List<T> Sample(int amount)
        {
            List<T> result = new List<T>(amount);
            List<int> indices = CreateRandomIndices(_domain.Count, amount);

            foreach (int index in indices)
            {
                result.Add(_domain[index]);
            }

            return result;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }
        
        private List<int> CreateRandomIndices(int itemsCount, int amount)
        {
            List<int> result = new List<int>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
            {
                result.Add(i);
            }
            
            //Source: https://stackoverflow.com/questions/273313/randomize-a-listt
            int n = itemsCount;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (result[k], result[n]) = (result[n], result[k]);
            }

            return result.GetRange(0, amount);
        }
    }
}