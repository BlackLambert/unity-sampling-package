namespace PCG.Toolkit
{
    public interface SetConstraint<T> : Constraint<T>
    {
        void AddResultSample(T sample);
        void ClearResultSample();
    }
}
