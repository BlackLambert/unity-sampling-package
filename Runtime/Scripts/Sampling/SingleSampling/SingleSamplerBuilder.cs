using System.Collections.Generic;

namespace SBaier.Sampling
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
                return new ConstraintSamplerStep<T>(new WeightedSampler<T>(_seed.Random));
            }

            public UpdateDomainStep<T> And()
            {
                return new UpdateDomainStep<T>(new WeightedSampler<T>(_seed.Random));
            }
        }

        public class ConstraintSamplerStep<T>
        {
            private Sampler<T> _baseSampler;

            public ConstraintSamplerStep(Sampler<T> baseSampler)
            {
                _baseSampler = baseSampler;
            }

            public UpdateDomainStep<T> WithConstraint(Constraint<T> constraint)
            {
                return new UpdateDomainStep<T>(new ConstraintSampler<T>(_baseSampler, constraint));
            }
        }

        public class UpdateDomainStep<T>
        {
            private Sampler<T> _sampler;

            public UpdateDomainStep(Sampler<T> sampler)
            {
                _sampler = sampler;
            }
            
            public Sampler<T> WithDomain(IList<T> domain)
            {
                _sampler.UpdateDomain(domain);
                return _sampler;
            }

            public Sampler<T> WithNoDomain()
            {
                _sampler.UpdateDomain(new List<T>());
                return _sampler;
            }
        }
    }
}