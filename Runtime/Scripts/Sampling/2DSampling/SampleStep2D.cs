namespace PCGToolkit.Sampling
{
    public struct SampleStep2D<T>
    {
        public T Item { get; }
        public Coordinate2D Coordinate { get; }

        public SampleStep2D(T item, Coordinate2D coordinate)
        {
            Item = item;
            Coordinate = coordinate;
        }
    }
}