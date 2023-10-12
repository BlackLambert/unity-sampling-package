using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling
{
    public abstract class MultiSampler<T> : Sampler<T>
    {
        public abstract List<T> Sample();
    }
}
