using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class RingSampler<T> : Sampler<T>
    {
        public bool HasSample => _domain.Count > 0;
        private List<T> _domain = new List<T>();

        private int _currentIndex = 0;
        
        public T Sample()
        {
            if (!HasSample)
            {
                throw new InvalidOperationException("Failed to sample. The domain is empty");
            }
            
            _currentIndex = (_currentIndex + 1) % _domain.Count;
            return _domain[_currentIndex];
        }

        public List<T> Sample(int amount)
        {
            List<T> result = new List<T>(amount);
            for (int i = 0; i < amount; i++)
            {
                result.Add(Sample());
            }
            return result;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
            _currentIndex = 0;
        }
    }
}
