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
            return new ConstraintStep<TTile, TContext>(new ConstraintSamplerContext<TTile, TContext>());
        }

        public SelectorStep<TTile> CreateBasicSampler<TTile>()
        {
            return new SelectorStep<TTile>(new BasicSamplerContext<TTile>());
        }

        public class ConstraintStep<TTile, TContext>
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            private readonly ConstraintSamplerContext<TTile, TContext> _context;

            public ConstraintStep(ConstraintSamplerContext<TTile, TContext> context)
            {
                _context = context;
            }
            
            public DefaultTileStep<TTile, TContext> WithConstraint(Constraint<TContext> constraint)
            {
                _context.Constraint = constraint;
                return new DefaultTileStep<TTile, TContext>(_context);
            }
        }

        public class DefaultTileStep<TTile, TContext>
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            private readonly ConstraintSamplerContext<TTile, TContext> _context;

            public DefaultTileStep(ConstraintSamplerContext<TTile, TContext> context)
            {
                _context = context;
            }

            public SelectorStep<TTile> And()
            {
                return new SelectorStep<TTile>(_context);
            }

            public SelectorStep<TTile> WithDefaultTile(TTile tile)
            {
                _context.DefaultTile = tile;
                return new SelectorStep<TTile>(_context);
            }
        }

        public class SelectorStep<TTile>
        {
            private readonly Context<TTile> _context;

            public SelectorStep(Context<TTile> context)
            {
                _context = context;
            }

            public WithSimpleSamplerStep<TTile> WithSelector(Selector<Coordinate2D> selector)
            {
                _context.Selector = selector;
                return new WithSimpleSamplerStep<TTile>(_context);
            }

            public WithSimpleSamplerStep<TTile> WithBasicSelector()
            {
                _context.Selector = new BasicSelector<Coordinate2D>();
                return new WithSimpleSamplerStep<TTile>(_context);
            }

            public WithSimpleSamplerStep<TTile> WithPrioritizedSelector(int initialPriority)
            {
                _context.Selector = new PrioritizedSelector<Coordinate2D>(initialPriority);
                return new WithSimpleSamplerStep<TTile>(_context);
            }

            public WithSimpleSamplerStep<TTile> WithRandomSelector(Random random)
            {
                _context.Selector = new RandomSelector<Coordinate2D>(random);
                return new WithSimpleSamplerStep<TTile>(_context);
            }

            public WithSimpleSamplerStep<TTile> WithPrioritizedSelector()
            {
                _context.Selector = new PrioritizedSelector<Coordinate2D>();
                return new WithSimpleSamplerStep<TTile>(_context);
            }
        }

        public class WithSimpleSamplerStep<TTile>
        {
            private Context<TTile> _context;

            public WithSimpleSamplerStep(Context<TTile> context)
            {
                _context = context;
            }

            public UpdateDomainStep<TTile> WithSingleSampler(SingleSampler<TTile> singleSampler)
            {
                _context.SingleSampler = singleSampler;
                return new UpdateDomainStep<TTile>(_context.Build());
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

        public class ConstraintSamplerContext<TTile, TContext> : Context<TTile>
            where TTile : Tile
            where TContext : TileSamplingValidationContext<TTile>, new()
        {
            public TTile DefaultTile { get; set; }
            public Constraint<TContext> Constraint { get; set; }

            public override TileSampler<TTile> Build()
            {
                return DefaultTile == null
                    ? new ConstraintTileSampler<TTile, TContext>(Selector, SingleSampler, Constraint)
                    : new ConstraintTileSampler<TTile, TContext>(Selector, SingleSampler, Constraint, DefaultTile);
            }
        }

        public class BasicSamplerContext<T> : Context<T>
        {
            public override TileSampler<T> Build()
            {
                return new BasicTileSampler<T>(SingleSampler, Selector);
            }
        }

        public abstract class Context<T>
        {
            public Selector<Coordinate2D> Selector { get; set; }
            public SingleSampler<T> SingleSampler { get; set; }
            public abstract TileSampler<T> Build();
        }
    }
}