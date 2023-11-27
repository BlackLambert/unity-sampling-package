using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class TileSamplerBuilder
    {
        public ConstraintStep<TTile, TContext> CreateConstraintSampler<TTile, TContext>()
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            return new ConstraintStep<TTile, TContext>();
        }

        public WithSimpleSamplerStep<TTile> CreateBasicSampler<TTile>()
        {
            return new WithSimpleSamplerStep<TTile>(sampler => new BasicTileSampler<TTile>(sampler));
        }

        public class ConstraintStep<TTile, TContext>
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            public SelectorStep<TTile, TContext> WithConstraint(Constraint<TContext> constraint)
            {
                return new SelectorStep<TTile, TContext>(constraint);
            }
        }
        
        public class SelectorStep<TTile, TContext> 
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            private readonly Constraint<TContext> _constraint;

            public SelectorStep(Constraint<TContext> constraint)
            {
                _constraint = constraint;
            }

            public DefaultTileStep<TTile, TContext> WithBasicSelector()
            {
                return new DefaultTileStep<TTile, TContext>(new BasicSelector<Coordinate2D>(), _constraint);
            }

            public DefaultTileStep<TTile, TContext> WithPrioritizedSelector(int initialPriority)
            {
                return new DefaultTileStep<TTile, TContext>(new PrioritizedSelector<Coordinate2D>(initialPriority), _constraint);
            }

            public DefaultTileStep<TTile, TContext> WithPrioritizedSelector()
            {
                return new DefaultTileStep<TTile, TContext>(new PrioritizedSelector<Coordinate2D>(), _constraint);
            }
        }

        public class DefaultTileStep<TTile, TContext> 
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            private readonly Selector<Coordinate2D> _selector;
            private readonly Constraint<TContext> _constraint;

            public DefaultTileStep(
                Selector<Coordinate2D> selector,
                Constraint<TContext> constraint)
            {
                _selector = selector;
                _constraint = constraint;
            }

            public WithSimpleSamplerStep<TTile> And()
            {
                return new WithSimpleSamplerStep<TTile>(Create);
            }

            public WithSimpleSamplerStep<TTile> WithDefaultTile(TTile tile)
            {
                return new WithSimpleSamplerStep<TTile>(sampler => Create(sampler, tile));
            }

            private ConstraintTileSampler<TTile, TContext> Create(SingleSampler<TTile> singleSampler)
            {
                return new ConstraintTileSampler<TTile, TContext>(_selector, singleSampler, _constraint);
            }

            private ConstraintTileSampler<TTile, TContext> Create(SingleSampler<TTile> singleSampler, TTile tile)
            {
                return new ConstraintTileSampler<TTile, TContext>(_selector, singleSampler, _constraint, tile);
            }
        }

        public class WithSimpleSamplerStep<TTile>
        {
            private Func<SingleSampler<TTile>, TileSampler<TTile>> _createFunction;
            
            public WithSimpleSamplerStep(Func<SingleSampler<TTile>, TileSampler<TTile>> createFunction)
            {
                _createFunction = createFunction;
            }
            
            public UpdateDomainStep<TTile> WithSingleSampler(SingleSampler<TTile> singleSampler)
            {
                return new UpdateDomainStep<TTile>(_createFunction(singleSampler));
            }
        }

        public class UpdateDomainStep<T>
        {
            private TileSampler<T> _sampler;

            public UpdateDomainStep(TileSampler<T> sampler)
            {
                _sampler = sampler;
            }
            
            public TileSampler<T> WithDomain(IList<T> domain)
            {
                _sampler.UpdateDomain(domain);
                return _sampler;
            }

            public TileSampler<T> WithNoDomain()
            {
                _sampler.UpdateDomain(new List<T>());
                return _sampler;
            }
        }
    }
}