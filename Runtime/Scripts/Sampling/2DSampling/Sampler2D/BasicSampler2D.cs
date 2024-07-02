using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class BasicSampler2D<T> : Sampler2DBase<T>
    {
        private Sampler<T> _baseSampler;

        public BasicSampler2D(
            Sampler<T> baseSampler,
            Sampler<Coordinate2D> coordinateSampler,
            Grid2DCoordinateFactory grid2DCoordinateFactory) :
            base(coordinateSampler, grid2DCoordinateFactory)
        {
            _baseSampler = baseSampler;
        }

        protected override void InitSampling(List<Coordinate2D> coordinates)
        {
            _coordinateSampler.UpdateDomain(coordinates);
        }

        protected override Func<SampleStep2D<T>> GetSampleNextFunction(Grid2D<T> grid2D) => SampleNext;
        protected override Func<bool> GetHasNextSampleFunction() => () => _coordinateSampler.HasSample;

        private SampleStep2D<T> SampleNext()
        {
            Coordinate2D coordinate = _coordinateSampler.Sample();
            return new SampleStep2D<T>(_baseSampler.Sample(), _grid2DCoordinateFactory.ToGridCoordinate(coordinate));
        }
    }
}