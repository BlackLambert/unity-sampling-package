using System;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
    [Serializable]
    public class Noise1DStep
    {
        [field: SerializeField] 
        public float IntervalScale { get; private set; } = 1;

        [field: SerializeField]
        public float ValueScale { get; private set; } = 1;

        [field: SerializeField] 
        public float Offset { get; private set; } = 0;
    }
}
