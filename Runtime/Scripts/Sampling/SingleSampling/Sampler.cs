using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public abstract class Sampler<T>
    {
        public abstract IReadOnlyCollection<T> Samples { get; }
        public abstract void UpdateSamples(IList<T> samples);
    }
}
