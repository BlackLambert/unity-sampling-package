using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public interface SetSampler<TSample> : Sampler<TSample>
    {
        List<TSample> Sample();
    }
}
