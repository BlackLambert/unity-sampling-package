namespace PCG.Toolkit
{
    public interface Constraint<T>
    {
        bool IsValid(T item);
    }
}
