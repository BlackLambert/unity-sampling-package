namespace PCGToolkit.Sampling
{
    public interface SetSamplingValidationContext<T>
    {
        T CurrentDomainElementToValidate { get; set; }
        void AddSample(T sample);
        void Reset();
    }
}
