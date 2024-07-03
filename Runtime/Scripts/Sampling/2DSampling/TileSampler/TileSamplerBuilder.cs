using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class TileSamplerBuilder
    {
        public CoordinateSamplerStep<TTile> CreateHexTileSampler<TTile>(
            HexTileRotation rotation = HexTileRotation.FlatTop,
            HexGridIndentation indentation = HexGridIndentation.Odd)
            where TTile : Tile
        {
            Grid2DCoordinateFactory factory = new HexGrid2DCoordinatesFactory(rotation, indentation);
            TileInfo tileInfo = new HexTileInfo(rotation);
            return new CoordinateSamplerStep<TTile>(new TileContext()
                { CoordinateFactory = factory, TileInfo = tileInfo });
        }

        public CoordinateSamplerStep<TTile> CreateRectTileSampler<TTile>()
            where TTile : Tile
        {
            Grid2DCoordinateFactory factory = new RectGrid2DCoordinatesFactory();
            TileInfo tileInfo = new RectTileInfo();
            return new CoordinateSamplerStep<TTile>(new TileContext()
                { CoordinateFactory = factory, TileInfo = tileInfo });
        }

        public class CoordinateSamplerStep<TTile>
            where TTile : Tile
        {
            private readonly BasicSamplerContext<TTile> _context;

            public CoordinateSamplerStep(TileContext context)
            {
                _context = new BasicSamplerContext<TTile>() { TileContext = context };
            }

            public SingleSamplerStep<TTile> WithCoordinateSampler(Sampler<Coordinate2D> sampler)
            {
                _context.CoordinateSampler = sampler;
                return new SingleSamplerStep<TTile>(_context);
            }

            public SingleSamplerStep<TTile> WithBasicCoordinateSampler()
            {
                FirstElementSampler<Coordinate2D> baseSampler = new FirstElementSampler<Coordinate2D>();
                _context.CoordinateSampler = new ExclusiveSampler<Coordinate2D>(baseSampler);
                return new SingleSamplerStep<TTile>(_context);
            }

            public SingleSamplerStep<TTile> WithPrioritizedCoordinateSampler(int initialPriority = 0)
            {
                PrioritizedSampler<Coordinate2D> baseSampler = new PrioritizedSampler<Coordinate2D>(initialPriority);
                _context.CoordinateSampler = new ExclusiveSampler<Coordinate2D>(baseSampler);
                return new SingleSamplerStep<TTile>(_context);
            }

            public SingleSamplerStep<TTile> WithRandomCoordinateSampler(Random random)
            {
                RandomSampler<Coordinate2D> baseSampler = new RandomSampler<Coordinate2D>(random);
                _context.CoordinateSampler = new ExclusiveSampler<Coordinate2D>(baseSampler);
                return new SingleSamplerStep<TTile>(_context);
            }
        }

        public class SingleSamplerStep<TTile>
            where TTile : Tile
        {
            private BasicSamplerContext<TTile> _context;

            public SingleSamplerStep(BasicSamplerContext<TTile> context)
            {
                _context = context;
            }

            public UpdateDomainStep<TTile> WithSingleSampler(Sampler<TTile> singleSampler)
            {
                _context.ElementSampler = singleSampler;
                return new UpdateDomainStep<TTile>(_context);
            }
        }

        public class UpdateDomainStep<T>
            where T : Tile
        {
            private BasicSamplerContext<T> _context;

            public UpdateDomainStep(BasicSamplerContext<T> context)
            {
                _context = context;
            }

            public ConstraintStep<T> WithDomain(IList<T> domain)
            {
                _context.Domain = domain;
                return new ConstraintStep<T>(_context);
            }

            public ConstraintStep<T> WithNoDomain()
            {
                _context.Domain = new List<T>();
                return new ConstraintStep<T>(_context);
            }
        }

        public class ConstraintStep<TTile>
            where TTile : Tile
        {
            private readonly BasicSamplerContext<TTile> _basicSamplerContext;

            public ConstraintStep(BasicSamplerContext<TTile> context)
            {
                _basicSamplerContext = context;
            }

            public Sampler2D<TTile> BuildBasicSampler()
            {
                return _basicSamplerContext.Build();
            }

            public Sampler2D<TTile> BuildArealSampler(Sampler<Coordinate2D> startCoordinatesSampler)
            {
                ArealSamplerContext<TTile> context = new ArealSamplerContext<TTile>();
                context.BasicSamplerContext = _basicSamplerContext;
                context.StartCoordinateSampler = startCoordinatesSampler;
                return context.Build();
            }

            public DefaultTileStep<TTile> WithConstraint(Constraint<TileSamplingContext<TTile>> constraint)
            {
                ConstraintSamplerContext<TTile> constraintSamplerContext =
                    new ConstraintSamplerContext<TTile>() { BasicSamplerContext = _basicSamplerContext };
                constraintSamplerContext.Constraint = constraint;
                return new DefaultTileStep<TTile>(constraintSamplerContext);
            }
        }

        public class DefaultTileStep<TTile>
            where TTile : Tile
        {
            private readonly ConstraintSamplerContext<TTile> _context;

            public DefaultTileStep(ConstraintSamplerContext<TTile> context)
            {
                _context = context;
            }

            public Sampler2D<TTile> Build()
            {
                return _context.Build();
            }

            public Sampler2D<TTile> WithDefaultTile(TTile tile)
            {
                _context.DefaultTile = tile;
                return _context.Build();
            }
        }

        public class ArealSamplerContext<TTile>
            where TTile : Tile
        {
            public BasicSamplerContext<TTile> BasicSamplerContext { get; set; }
            public Sampler<Coordinate2D> StartCoordinateSampler { get; set; }

            public Sampler2D<TTile> Build()
            {
                Sampler2D<TTile> sampler = new ArealSampler2D<TTile>(
                    BasicSamplerContext.TileContext.TileInfo,
                    BasicSamplerContext.ElementSampler,
                    StartCoordinateSampler,
                    BasicSamplerContext.CoordinateSampler,
                    BasicSamplerContext.TileContext.CoordinateFactory);
                sampler.UpdateDomain(BasicSamplerContext.Domain);
                return sampler;
            }
        }

        public class ConstraintSamplerContext<TTile>
            where TTile : Tile
        {
            public BasicSamplerContext<TTile> BasicSamplerContext { get; set; }
            public TTile DefaultTile { get; set; }
            public Constraint<TileSamplingContext<TTile>> Constraint { get; set; }

            public Sampler2D<TTile> Build()
            {
                ConstraintSampler2D<TTile> sampler = DefaultTile == null
                    ? new ConstraintSampler2D<TTile>(
                        BasicSamplerContext.TileContext.TileInfo,
                        BasicSamplerContext.CoordinateSampler,
                        BasicSamplerContext.ElementSampler,
                        Constraint,
                        BasicSamplerContext.TileContext.CoordinateFactory)
                    : new ConstraintSampler2D<TTile>(
                        BasicSamplerContext.TileContext.TileInfo,
                        BasicSamplerContext.CoordinateSampler,
                        BasicSamplerContext.ElementSampler,
                        Constraint,
                        DefaultTile,
                        BasicSamplerContext.TileContext.CoordinateFactory);
                sampler.UpdateDomain(BasicSamplerContext.Domain);
                return sampler;
            }
        }

        public class BasicSamplerContext<T>
        {
            public TileContext TileContext { get; set; }
            public Sampler<Coordinate2D> CoordinateSampler { get; set; }
            public Sampler<T> ElementSampler { get; set; }
            public IList<T> Domain { get; set; }

            public Sampler2D<T> Build()
            {
                BasicSampler2D<T> sampler =
                    new BasicSampler2D<T>(ElementSampler, CoordinateSampler, TileContext.CoordinateFactory);
                sampler.UpdateDomain(Domain);
                return sampler;
            }
        }

        public class TileContext
        {
            public Grid2DCoordinateFactory CoordinateFactory { get; set; }
            public TileInfo TileInfo { get; set; }
        }
    }
}