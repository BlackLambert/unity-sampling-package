using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public abstract class Sampler2DBase<TTile> : Sampler2D<TTile>
    {
        protected readonly Sampler<Coordinate2D> _coordinateSampler;
        protected readonly List<TTile> _domain = new();
        protected readonly Grid2DCoordinateFactory _grid2DCoordinateFactory;

        protected Sampler2DBase(Sampler<Coordinate2D> coordinateSampler, Grid2DCoordinateFactory grid2DCoordinateFactory)
        {
            _coordinateSampler = coordinateSampler;
            _grid2DCoordinateFactory = grid2DCoordinateFactory;
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
            InitSampling(_grid2DCoordinateFactory.Create(width, height));
            Grid2D<TTile> grid = new Grid2D<TTile>(width, height);
            return new Sampling2DHandle<TTile>(grid, GetHasNextSampleFunction(), GetSampleNextFunction(grid));
        }

        protected abstract void InitSampling(List<Coordinate2D> coordinates);
        protected abstract Func<SampleStep2D<TTile>> GetSampleNextFunction(Grid2D<TTile> grid);
        protected abstract Func<bool> GetHasNextSampleFunction();
    }
}