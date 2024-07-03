namespace SBaier.Sampling
{
    public interface Bounds<in T>
    {
        bool Contains(T point);
    }
}