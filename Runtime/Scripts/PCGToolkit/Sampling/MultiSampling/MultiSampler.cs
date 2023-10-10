using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCG.Toolkit
{
    public abstract class MultiSampler<T> : Sampler<T>
    {
        public abstract List<T> Sample();
    }
}
