using System.Collections.Generic;

namespace PCG.Toolkit
{
    public class ConstraintMultiSampler<T> : MultiSampler<T>
    {
        public override IReadOnlyCollection<T> Samples => _samples;

        private readonly int _sampleMaximum;
        private readonly SingleSampler<T> _baseSingleSampler;
        private SetConstraint<T> _constraint;
        private List<T> _constraintSamples;
        private List<T> _samples;

        public ConstraintMultiSampler(
            SingleSampler<T> baseSingleSampler, 
            SetConstraint<T> constraint, 
            int sampleMaximum = 100)
        {
            _baseSingleSampler = baseSingleSampler;
            _constraint = constraint;
            _sampleMaximum = sampleMaximum;
            _constraintSamples = new List<T>(sampleMaximum);
            _samples = new List<T>(sampleMaximum);
        }

        public override List<T> Sample()
        {
            InitConstraints();
            UpdateValidSamples();
            
            List<T> result = new List<T>(_sampleMaximum);
            while (_constraintSamples.Count > 0 && result.Count < _sampleMaximum)
            {
                _baseSingleSampler.UpdateSamples(_constraintSamples);
                T sample = _baseSingleSampler.Sample();
                result.Add(sample);
                _constraint.AddResultSample(sample);
                UpdateValidSamples();
            }

            return result;
        }

        public override void UpdateSamples(IList<T> samples)
        {
            _samples.Clear();
            _samples.AddRange(samples);
        }

        private void UpdateValidSamples()
        {
            _constraintSamples.Clear();

            foreach (T sample in _samples)
            {
                if (_constraint.IsValid(sample))
                {
                    _constraintSamples.Add(sample);
                }
            }
        }

        private void InitConstraints()
        {
            _constraint.ClearResultSample();
        }
    }
}
