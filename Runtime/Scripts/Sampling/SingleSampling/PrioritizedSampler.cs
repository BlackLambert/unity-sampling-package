using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class PrioritizedSampler<T> : Sampler<T>
    {
        public bool HasSample => _prioritizedDomain.Count > 0;

        private readonly int _startPriority;
        private readonly PriorityComparer _comparer = new();
        private readonly List<KeyValuePair<T, int>> _prioritizedDomain = new();
        private readonly FirstElementSampler<KeyValuePair<T, int>> _firstElementSampler = new ();

        public PrioritizedSampler()
        {
            _startPriority = 0;
        }
        
        public PrioritizedSampler(int startPriority)
        {
            _startPriority = startPriority;
        }
        
        public T Sample()
        {
            ValidateDomainNotEmpty();
            KeyValuePair<T, int> firstPair = _firstElementSampler.Sample();
            return firstPair.Key;
        }

        public List<T> Sample(int amount)
        {
            ValidateDomainNotEmpty();
            KeyValuePair<T, int> firstPair = _firstElementSampler.Sample();
            List<T> result = new List<T>();

            for (int i = 0; i < amount; i++)
            {
                result.Add(firstPair.Key);
            }

            return result;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _prioritizedDomain.Clear();
            
            foreach (T item in domain)
            {
                _prioritizedDomain.Add(new KeyValuePair<T, int>(item, _startPriority));
            }

            Reorder();
        }

        private void Reorder()
        {
            _prioritizedDomain.Sort(_comparer);
            _prioritizedDomain.Reverse();
            _firstElementSampler.UpdateDomain(_prioritizedDomain);
        }

        private void ValidateDomainNotEmpty()
        {
            if (!HasSample)
            {
                throw new InvalidOperationException("There is not item left to get.");
            }
        }

        private class PriorityComparer : IComparer<KeyValuePair<T, int>>
        {
            public int Compare(KeyValuePair<T, int> x, KeyValuePair<T, int> y)
            {
                return x.Value.CompareTo(y.Value);
            }
        }
    }
}