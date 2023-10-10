using System;
using System.Collections;
using System.Collections.Generic;

namespace PCG.Toolkit
{
    public class WeightedList<T> : IDictionary<T, float>
    {
        public ICollection<T> Keys => _internalDictionary.Keys;
        public IReadOnlyCollection<T> ReadonlyKeys => _internalDictionary.Keys;
        public ICollection<float> Values => _internalDictionary.Values;
        public Random Random => _random;
        public int Count => _internalDictionary.Count;
        public bool IsReadOnly => internalCollection.IsReadOnly;

        private ICollection<KeyValuePair<T, float>> internalCollection => _internalDictionary;
        private readonly Dictionary<T, float> _internalDictionary;
        private readonly Random _random;
        private float _totalWeight = 0;

        public WeightedList(Random random)
        {
            _internalDictionary = new();
            _random = random;
        }

        public WeightedList(IEnumerable<KeyValuePair<T, float>> items, Random random)
        {
            _internalDictionary = new();
            _random = random;

            foreach (KeyValuePair<T,float> item in items)
            {
                _internalDictionary.Add(item.Key, item.Value);
            }
        }

        public float this[T key]
        {
            get => _internalDictionary[key];
            set
            {
                _totalWeight = _internalDictionary[key];
                _internalDictionary[key] = value;
                _totalWeight += value;
            }
        }

        public void Clear()
        {
            _internalDictionary.Clear();
            _totalWeight = 0;
        }

        public void Add(T key, float value)
        {
            _internalDictionary.Add(key, value);
            _totalWeight += value;
        }

        public void Add(IEnumerable<KeyValuePair<T, float>> items)
        {
            foreach (KeyValuePair<T,float> item in items)
            {
                Add(item.Key, item.Value);
            }
        }
        
        public bool Remove(T key)
        {
            float itemWeight = this[key];
            _totalWeight -= itemWeight;
            return _internalDictionary.Remove(key);
        }

        public T GetRandomItem()
        {
            float targetWeightValue = (float)_random.NextDouble() * _totalWeight;
            return GetTargetItem(targetWeightValue);
        }

        public T GetItem(float targetWeightValue)
        {
            if (targetWeightValue > _totalWeight)
            {
                throw new InvalidOperationException();
            }

            return GetTargetItem(targetWeightValue);
        }

        public bool ContainsKey(T key) => _internalDictionary.ContainsKey(key);
        public bool TryGetValue(T key, out float value) => _internalDictionary.TryGetValue(key, out value);

        bool ICollection<KeyValuePair<T, float>>.Contains(KeyValuePair<T, float> item)
        {
            return _internalDictionary.ContainsKey(item.Key) &&
                   _internalDictionary[item.Key] == item.Value;
        }

        void ICollection<KeyValuePair<T, float>>.CopyTo(KeyValuePair<T, float>[] array, int arrayIndex) =>
            internalCollection.CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<T, float>>.Remove(KeyValuePair<T, float> item) => Remove(item.Key);
        void ICollection<KeyValuePair<T, float>>.Add(KeyValuePair<T, float> item) => Add(item.Key, item.Value);
        IEnumerator IEnumerable.GetEnumerator() => _internalDictionary.GetEnumerator();
        IEnumerator<KeyValuePair<T, float>> IEnumerable<KeyValuePair<T, float>>.GetEnumerator() =>
            _internalDictionary.GetEnumerator();

        private T GetTargetItem(float target)
        {
            float currentValue = 0;
            
            foreach (KeyValuePair<T,float> pair in _internalDictionary)
            {
                currentValue += pair.Value;
                if (target <= currentValue)
                {
                    return pair.Key;
                }
            }

            throw new InvalidOperationException();
        }
    }
}