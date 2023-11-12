namespace PCGToolkit.Sampling
{
    public interface TileSampler<T> : Sampler<T>
    {
        Grid2D<T> Sample(int size);
        Grid2D<T> Sample(int width, int height);
    }
}