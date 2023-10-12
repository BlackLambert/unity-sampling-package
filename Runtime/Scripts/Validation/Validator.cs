using UnityEngine;

namespace PCGToolkit.Sampling
{
    public interface Validator<T>
    {
        void Validate(T obj);
    }
}