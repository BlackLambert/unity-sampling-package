using System.Collections;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class TileSamplerFactory
    {
        private SingleSamplerBuilder _singleBuilder = new SingleSamplerBuilder();
        private TileSamplerBuilder _tileBuilder = new TileSamplerBuilder();
        
        public TileSampler<TTile> CreateWeightedBasicSampler<TTile>(Seed seed, IList<TTile> items) 
            where TTile : Weighted
        {
            SingleSampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>().With(seed).And().WithNoDomain();
            return _tileBuilder.CreateBasicSampler<TTile>().WithBasicSelector().WithSingleSampler(singleSampler).WithDomain(items);
        }
        
        public TileSampler<TTile> CreateWeightedNeighborConstraintSampler<TTile>(Seed seed, IList<TTile> items) 
            where TTile : Weighted, Tile
        {
            SingleSampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>().With(seed).And().WithNoDomain();
            return _tileBuilder.CreateConstraintSampler<TTile, BasicTileSamplingValidationContext<TTile>>()
                .WithConstraint(new TileNeighbourConstraint<TTile>())
                .And()
                .WithBasicSelector()
                .WithSingleSampler(singleSampler)
                .WithDomain(items);
        }
        
        public TileSampler<TTile> CreateWeightedNeighborConstraintSamplerWithPrioritizedSelector<TTile>(Seed seed, 
            IList<TTile> items, TTile defaultTile) 
            where TTile : Weighted, Tile
        {
            SingleSampler<TTile> singleSampler = _singleBuilder.CreateWeighted<TTile>().With(seed).And().WithNoDomain();
            return _tileBuilder.CreateConstraintSampler<TTile, BasicTileSamplingValidationContext<TTile>>()
                .WithConstraint(new TileNeighbourConstraint<TTile>())
                .WithDefaultTile(defaultTile)
                .WithRandomSelector(seed.Random)
                .WithSingleSampler(singleSampler)
                .WithDomain(items);
        }
    }
}