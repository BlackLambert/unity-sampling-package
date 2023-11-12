using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public interface Sampler<T>
    {
        IReadOnlyCollection<T> Domain { get; }
        void UpdateDomain(IList<T> domain);
    }
}
