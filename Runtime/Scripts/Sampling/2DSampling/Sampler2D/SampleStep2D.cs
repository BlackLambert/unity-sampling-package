namespace SBaier.Sampling
{
    public struct SampleStep2D<T>
    {
        public T Item { get; }
        public Coordinate2D GridCoordinate { get; }

        public SampleStep2D(T item, Coordinate2D gridCoordinate)
        {
            Item = item;
            GridCoordinate = gridCoordinate;
        }
    }
}