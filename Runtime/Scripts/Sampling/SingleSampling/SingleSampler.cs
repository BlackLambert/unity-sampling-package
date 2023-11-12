using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public interface SingleSampler<T> : Sampler<T>
    {
        T Sample();
        List<T> Sample(int amount);
    }
}
