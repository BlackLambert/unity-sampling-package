using System;
using UnityEngine;

namespace PCGToolkit.Sampling
{
	public class PoissonDiskSamplingParameterValidator<T> : Validator<PoissonDiskSampling<T>.Parameters>
	{
		public void Validate(PoissonDiskSampling<T>.Parameters parameters)
		{
			ValidateAmount(parameters.Amount);
			ValidateMinDistance(parameters.MinDistance);
			ValidateStartPosition(parameters.Bounds, parameters.StartPosition);
		}

		private void ValidateAmount(int amount)
		{
			if (amount <= 0)
				throw new InvalidAmountException();
		}

		private void ValidateMinDistance(float minDistance)
		{
			if (minDistance < 0)
				throw new InvalidMinDistanceException();
		}

		private void ValidateStartPosition(Bounds<T> bounds, T startPos)
		{
			if (!bounds.Contains(startPos))
				throw new InvalidStartPositionException();
		}

		public class InvalidAmountException : ArgumentException { }
		public class InvalidMinDistanceException : ArgumentException { }
		public class InvalidStartPositionException : ArgumentException { }
	}
}
