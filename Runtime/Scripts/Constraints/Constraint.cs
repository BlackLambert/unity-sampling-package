namespace SBaier.Sampling
{
    public interface Constraint<in T>
    {
        bool IsValid(T samplingStep);
    }
}
