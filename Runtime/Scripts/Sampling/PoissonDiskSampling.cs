using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public abstract class PoissonDiskSampling<T>
    {
	    private const float _epsilon = 0.0001f;
		private readonly System.Random _random;
		private readonly List<int> _openList = new List<int>();
		private Bounds<T> _bounds;
		private float _minDistance;
		private int _amount;
		private List<T> _samples;
		private int _currentIndex;
		private int _maxSamplingTries;
		private T _startPosition;

		public PoissonDiskSampling(
			System.Random random,
			int maxSamplingTries)
		{
			ValidateTries(maxSamplingTries);

			_random = random;
			_maxSamplingTries = maxSamplingTries;
		}

		private void ValidateTries(int maxSamplingTries)
		{
			if (maxSamplingTries <= 0)
				throw new InvalidTriesAmountException();
		}

		public List<T> Sample(Parameters parameters)
		{
			Init(parameters);

			while (_currentIndex < _amount)
			{
				_samples.Add(CreateSample());
				_openList.Add(_currentIndex);
				_currentIndex++;
			}
			return _samples;
		}
		
		protected abstract void Validate(Parameters parameters);
		protected abstract T GetRandomPointAround(T point, float minDistance);
		protected abstract float GetMagnitude(T point, T sample);

		private bool HasMinDistanceToOtherSamples(T point)
		{
			foreach (T sample in _samples)
			{
				if (GetMagnitude(point, sample) + _epsilon < _minDistance)
					return false;
			}
			return true;
		}
		
		private void Init(Parameters parameters)
		{
			Validate(parameters);
			Reset();
			InitParameterFields(parameters);
		}

		private void Reset()
		{
			_openList.Clear();
			_samples = new List<T>(_amount);
			_currentIndex = 0;
		}

		private void InitParameterFields(Parameters parameters)
		{
			_bounds = parameters.Bounds;
			_minDistance = parameters.MinDistance;
			_amount = parameters.Amount;
			_startPosition = parameters.StartPosition;

		}

		private T CreateSample()
		{
			if (_currentIndex == 0)
				return CreateStartSample();
			return CreateSampleFromOpenList();
		}

		private T CreateSampleFromOpenList()
		{
			if (_openList.Count == 0)
				throw new SamplingException();

			int randomOpenIndex = _random.Next(_openList.Count);
			T openSample = _samples[_openList[randomOpenIndex]];
			for (int i = 0; i < _maxSamplingTries; i++)
			{
				T point = GetRandomPointAround(openSample, _minDistance);
				if (IsValid(point))
					return point;
			}

			_openList.RemoveAt(randomOpenIndex);
			return CreateSampleFromOpenList();
		}

		private T CreateStartSample()
		{
			return _startPosition;
		}

		private bool IsValid(T point)
		{
			return _bounds.Contains(point) &&
				HasMinDistanceToOtherSamples(point);
		}


		public class Parameters
		{
			public Parameters(int amount,
				float minDistance,
				Bounds<T> bounds,
				T startPosition)
			{
				Amount = amount;
				MinDistance = minDistance;
				Bounds = bounds;
				StartPosition = startPosition;
			}

			public int Amount { get; }
			public float MinDistance { get; }
			public Bounds<T> Bounds { get; }
			public T StartPosition { get; }
		}
        
        public class SamplingException : ArgumentException
        {
            public SamplingException() : base("Sampling failed. Please increase the amount of retries, " +
                                              "increase the size of the bounds or decrease the samples amount")
            { }
        }

        public class InvalidTriesAmountException : ArgumentException
        {
            public InvalidTriesAmountException() : base($"Please provide a valid amount of sampling tries.") { }
        }
    }
}
