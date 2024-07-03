using System.Collections.Generic;

namespace SBaier.Sampling
{
    
    public interface Sampler<T>
    {
        bool HasSample { get; }
        T Sample();
        List<T> Sample(int amount);
        void UpdateDomain(IEnumerable<T> domain);
    }
}
