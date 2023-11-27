using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class SingleSamplerBuilder
    {
        public WeightedSingleSamplerStepOne<T> CreateWeighted<T>() where T : Weighted
        {
            return new WeightedSingleSamplerStepOne<T>();
        }

        public class WeightedSingleSamplerStepOne<T> where T : Weighted
        {
            public WeightedSingleSamplerStepTwo<T> With(Seed seed)
            {
                return new WeightedSingleSamplerStepTwo<T>(seed);
            }
        }

        public class WeightedSingleSamplerStepTwo<T> where T : Weighted
        {
            private Seed _seed;

            public WeightedSingleSamplerStepTwo(Seed seed)
            {
                _seed = seed;
            }

            public ConstraintSamplerStep<T> InConstraintSampler()
            {
                return new ConstraintSamplerStep<T>(new WeightedSingleSampler<T>(_seed.Random));
            }

            public UpdateDomainStep<T> And()
            {
                return new UpdateDomainStep<T>(new WeightedSingleSampler<T>(_seed.Random));
            }
        }

        public class ConstraintSamplerStep<T>
        {
            private SingleSampler<T> _baseSampler;

            public ConstraintSamplerStep(SingleSampler<T> baseSampler)
            {
                _baseSampler = baseSampler;
            }

            public UpdateDomainStep<T> WithConstraint(Constraint<T> constraint)
            {
                return new UpdateDomainStep<T>(new ConstraintSingleSampler<T>(_baseSampler, constraint));
            }
        }

        public class UpdateDomainStep<T>
        {
            private SingleSampler<T> _sampler;

            public UpdateDomainStep(SingleSampler<T> sampler)
            {
                _sampler = sampler;
            }
            
            public SingleSampler<T> WithDomain(IList<T> domain)
            {
                _sampler.UpdateDomain(domain);
                return _sampler;
            }

            public SingleSampler<T> WithNoDomain()
            {
                _sampler.UpdateDomain(new List<T>());
                return _sampler;
            }
        }
    }
}