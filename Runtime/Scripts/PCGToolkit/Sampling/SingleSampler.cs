using System.Collections.Generic;

namespace PCG.Toolkit
{
    public abstract class SingleSampler<T> : Sampler<T>
    {
        public abstract T Sample();
        public abstract List<T> Sample(int amount);
    }
}
