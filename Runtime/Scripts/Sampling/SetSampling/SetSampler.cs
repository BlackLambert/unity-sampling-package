using System.Collections.Generic;

namespace SBaier.Sampling
{
    public interface SetSampler<T>
    {
        List<T> Sample();
        void UpdateDomain(IList<T> domain);
    }
}
