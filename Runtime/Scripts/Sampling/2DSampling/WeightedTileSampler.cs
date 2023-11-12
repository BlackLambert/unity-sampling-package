using System;

namespace PCGToolkit.Sampling
{
    public class WeightedTileSampler<T> : WeightedSampler<T> where T : Weighted
    {
        public WeightedTileSampler(Random random) : base(random)
        {
            
        }

        public Grid2D<T> Sample(int size)
        {
            return Sample(size, size);
        }

        public Grid2D<T> Sample(int width, int height)
        {
            Grid2D<T> result = new Grid2D<T>(height, width);
            int tilesAmount = width * height;

            for (int i = 0; i < tilesAmount; i++)
            {
                int column = i % height;
                int row = i / height;
                result[row, column] = _weightedDomain.GetRandomItem();
            }
            
            return result;
        }
    }
}
