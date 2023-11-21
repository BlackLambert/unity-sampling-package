using System.Collections;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public static class TileSamplerFactory
    {
        public static TileSampler<TTile> CreateWeightedBasicSampler<TTile>(int seed, IList<TTile> items) 
            where TTile : Weighted
        {
            System.Random random = new System.Random(seed);
            WeightedSingleSampler<TTile> baseSampler = new WeightedSingleSampler<TTile>(random);
            BasicTileSampler<TTile> tilesSampler = new BasicTileSampler<TTile>(baseSampler);
            tilesSampler.UpdateDomain(items);
            return tilesSampler;
        }
        
        public static TileSampler<TTile> CreateWeightedNeighborConstraintSampler<TTile>(int seed, IList<TTile> items) 
            where TTile : Weighted, Tile
        {
            System.Random random = new System.Random(seed);
            WeightedSingleSampler<TTile> baseSampler = new WeightedSingleSampler<TTile>(random);
            TileNeighbourConstraint<TTile> constraint = new TileNeighbourConstraint<TTile>();
            BasicCoordinateSelector selector = new BasicCoordinateSelector();
            ConstraintTileSampler<TTile, BasicTileSamplingValidationContext<TTile>> tilesSampler = 
                new ConstraintTileSampler<TTile,  BasicTileSamplingValidationContext<TTile>>(selector, baseSampler, constraint);
            tilesSampler.UpdateDomain(items);
            return tilesSampler;
        }
    }
}