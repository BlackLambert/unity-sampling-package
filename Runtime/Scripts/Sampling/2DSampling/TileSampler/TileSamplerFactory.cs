using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class TileSamplerFactory
    {
        private SingleSamplerBuilder _singleBuilder = new SingleSamplerBuilder();
        private TileSamplerBuilder _tileBuilder = new TileSamplerBuilder();
        
        public Sampler2D<TTile> CreateArealSampler<TTile>(Seed seed, IList<TTile> items)
            where TTile : Tile, Weighted
        {
            Sampler<TTile> singleSampler = new RingSampler<TTile>();
            singleSampler.UpdateDomain(items);

            return _tileBuilder
                .CreateHexTileSampler<TTile>()
                .WithRandomCoordinateSampler(seed.Random)
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .BuildArealSampler(new RandomSampler<Coordinate2D>(seed.Random));
        }
        
        public Sampler2D<TTile> CreateVoronoyArealSampler<TTile>(Seed seed, IList<TTile> items)
            where TTile : Tile, Weighted
        {
            Sampler<TTile> singleSampler = new RingSampler<TTile>();
            singleSampler.UpdateDomain(items);

            return _tileBuilder
                .CreateHexTileSampler<TTile>()
                .WithCoordinateSampler(new FirstElementSampler<Coordinate2D>())
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .BuildArealSampler(new RandomSampler<Coordinate2D>(seed.Random));
        }
        
        public Sampler2D<TTile> CreateWeightedBasicHexSampler<TTile>(Seed seed, IList<TTile> items)
            where TTile : Tile, Weighted
        {
            Sampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>()
                .With(seed)
                .And()
                .WithNoDomain();

            return _tileBuilder
                .CreateHexTileSampler<TTile>()
                .WithBasicCoordinateSampler()
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .BuildBasicSampler();
        }

        public Sampler2D<TTile> CreateWeightedBasicSampler<TTile>(Seed seed, IList<TTile> items)
            where TTile : Tile, Weighted
        {
            Sampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>()
                .With(seed)
                .And()
                .WithNoDomain();

            return _tileBuilder
                .CreateRectTileSampler<TTile>()
                .WithBasicCoordinateSampler()
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .BuildBasicSampler();
        }

        public Sampler2D<TTile> CreateWeightedNeighborConstraintSampler<TTile>(Seed seed, IList<TTile> items)
            where TTile : Tile, Weighted, NeighborConstraintTile
        {
            Sampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>()
                .With(seed)
                .And()
                .WithNoDomain();

            return _tileBuilder
                .CreateRectTileSampler<TTile>()
                .WithBasicCoordinateSampler()
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .WithConstraint(new TileNeighbourConstraint<TTile>())
                .Build();
        }

        public Sampler2D<TTile> CreateWeightedNeighborConstraintSamplerWithPrioritizedSelector<TTile>(Seed seed,
            IList<TTile> items, TTile defaultTile)
            where TTile : Tile, Weighted, NeighborConstraintTile
        {
            Sampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>()
                .With(seed)
                .And()
                .WithNoDomain();

            return _tileBuilder
                .CreateRectTileSampler<TTile>()
                .WithRandomCoordinateSampler(seed.Random)
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .WithConstraint(new TileNeighbourConstraint<TTile>())
                .WithDefaultTile(defaultTile);
        }

        public Sampler2D<TTile> CreateWeightedNeighborConstraintSamplerWithPrioritizedHexSelector<TTile>(Seed seed,
            IList<TTile> items, TTile defaultTile)
            where TTile : Tile, Weighted, NeighborConstraintTile
        {
            Sampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>()
                .With(seed)
                .And()
                .WithNoDomain();

            return _tileBuilder
                .CreateHexTileSampler<TTile>()
                .WithRandomCoordinateSampler(seed.Random)
                .WithSingleSampler(singleSampler)
                .WithDomain(items)
                .WithConstraint(new TileNeighbourConstraint<TTile>())
                .WithDefaultTile(defaultTile);
        }
    }
}