using PCGToolkit.Sampling;

namespace PCGToolkit.Sampling
{
    public interface SetConstraint<T> : Constraint<T>
    {
        void AddResultSample(T sample);
        void ClearResultSample();
    }
}
