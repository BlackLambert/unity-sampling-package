using System;

namespace PCGToolkit.Sampling
{
    public class BasicTileSampler<T> : TileSampleBase<T>
    {
        private SingleSampler<T> _baseSampler;
        
        public BasicTileSampler(SingleSampler<T> baseSampler, Selector<Coordinate2D> selector) : base(selector)
        {
            _baseSampler = baseSampler;
            _selector = selector;
        }

        protected override Func<SampleStep2D<T>> GetSampleNextFunction(Grid2D<T> grid2D) => SampleNext;

        private SampleStep2D<T> SampleNext()
        {
            Coordinate2D coordinate = _selector.GetNext();
            return new SampleStep2D<T>(_baseSampler.Sample(), coordinate);
        }
    }
}
