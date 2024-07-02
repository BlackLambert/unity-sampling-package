namespace SBaier.Sampling
{
    public interface Validator<in T>
    {
        void Validate(T obj);
    }
}