using System.Collections.Generic;
using System.Linq;

namespace SBaier.Sampling
{
    public class FirstElementSampler<T> : Sampler<T>
    {
        public bool HasSample => _domain.FirstOrDefault() != null;
        
        private IEnumerable<T> _domain;
        
        public T Sample()
        {
            return _domain.First();
        }

        public List<T> Sample(int amount)
        {
            List<T> result = new List<T>(amount);
            for (int i = 0; i < amount; i++)
            {
                result.Add(_domain.First());
            }
            return result;
        }

        public void UpdateDomain(IEnumerable<T> domain)
        {
            _domain = domain;
        }
    }
}