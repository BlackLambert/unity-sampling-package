namespace PCGToolkit.Sampling
{
    public interface Constraint<T>
    {
        bool IsValid(T item);
    }
}
