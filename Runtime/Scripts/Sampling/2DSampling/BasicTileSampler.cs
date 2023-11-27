using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public class BasicTileSampler<T> : TileSampler<T>
    {
        public IReadOnlyCollection<T> Domain => _domain;
        private SingleSampler<T> _baseSampler;
        private List<T> _domain;
        
        public BasicTileSampler(SingleSampler<T> baseSampler)
        {
            _baseSampler = baseSampler;
        }
        
        public void UpdateDomain(IList<T> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }

        public Grid2D<T> Sample(int size)
        {
            return Sample(size, size);
        }

        public Grid2D<T> Sample(int width, int height)
        {
            Grid2D<T> result = new Grid2D<T>(width, height);
            int tilesAmount = width * height;
            List<T> samples = _baseSampler.Sample(tilesAmount);

            for (int i = 0; i < tilesAmount; i++)
            {
                int column = i % height;
                int row = i / height;
                result[column, row] = samples[i];
            }
            
            return result;
        }
    }
}
