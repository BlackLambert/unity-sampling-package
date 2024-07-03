using System.Collections.Generic;

namespace SBaier.Sampling
{
    public interface Sampler2D<T>
    {
        Sampling2DHandle<T> Sample(int size);
        Sampling2DHandle<T> Sample(int width, int height);
        void UpdateDomain(IList<T> domain);
    }
}