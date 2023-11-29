namespace PCGToolkit.Sampling
{
    public interface TileSampler<T> : Sampler<T>
    {
        Sampling2DHandle<T> Sample(int size);
        Sampling2DHandle<T> Sample(int width, int height);
    }
}