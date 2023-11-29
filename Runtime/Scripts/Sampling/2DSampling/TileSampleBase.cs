using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public abstract class TileSampleBase<TTile> : TileSampler<TTile>
    {
        public IReadOnlyCollection<TTile> Domain => _domain;
        
        protected Selector<Coordinate2D> _selector;
        protected readonly List<TTile> _domain = new List<TTile>();

        protected TileSampleBase(Selector<Coordinate2D> selector)
        {
            _selector = selector;
        }
        
        public void UpdateDomain(IList<TTile> domain)
        {
            _domain.Clear();
            _domain.AddRange(domain);
        }

        public Sampling2DHandle<TTile> Sample(int size)
        {
            return Sample(size, size);
        }

        public Sampling2DHandle<TTile> Sample(int width, int height)
        {
            _selector.Init(CreateCoordinates(width, height));
            Grid2D<TTile> grid = new Grid2D<TTile>(width, height);
            return new Sampling2DHandle<TTile>(grid, _selector.HasNext, GetSampleNextFunction(grid));
        }

        protected abstract Func<SampleStep2D<TTile>> GetSampleNextFunction(Grid2D<TTile> grid);

        private ICollection<Coordinate2D> CreateCoordinates(int width, int height)
        {
            List<Coordinate2D> result = new List<Coordinate2D>(width * height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Add(new Coordinate2D { X = x, Y = y });
                }
            }

            return result;
        }
    }
}