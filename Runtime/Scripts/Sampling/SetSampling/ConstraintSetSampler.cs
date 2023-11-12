using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class ConstraintSetSampler<TSample, TContext> : SetSampler<TSample> 
        where TContext : SetSamplingValidationContext<TSample>, new()
    {
        public IReadOnlyCollection<TSample> Domain => _domain;

        private readonly int _sampleMaximum;
        private readonly SingleSampler<TSample> _baseSingleSampler;
        private Constraint<TContext> _constraint;
        private List<TSample> _constraintDomain;
        private List<TSample> _domain;

        public ConstraintSetSampler(
            SingleSampler<TSample> baseSingleSampler, 
            Constraint<TContext> constraint,
            int sampleMaximum = 100)
        {
            _baseSingleSampler = baseSingleSampler;
            _constraint = constraint;
            _sampleMaximum = sampleMaximum;
            _constraintDomain = new List<TSample>(sampleMaximum);
            _domain = new List<TSample>(sampleMaximum);
        }

        public List<TSample> Sample()
        {
            TContext context = new TContext();
            List<TSample> result = new List<TSample>(_sampleMaximum);
            UpdateConstraintDomain(context);
            
            while (_constraintDomain.Count > 0 && result.Count < _sampleMaximum)
            {
                _baseSingleSampler.UpdateDomain(_constraintDomain);
                TSample sample = _baseSingleSampler.Sample();
                result.Add(sample);
                context.AddSample(sample);
                UpdateConstraintDomain(context);
            }

            return result;
        }

        public void UpdateDomain(IList<TSample> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }

        private void UpdateConstraintDomain(TContext context)
        {
            _constraintDomain.Clear();

            foreach (TSample sample in _domain)
            {
                context.CurrentDomainElementToValidate = sample;
                if (_constraint.IsValid(context))
                {
                    _constraintDomain.Add(sample);
                }
            }
        }
    }
}
