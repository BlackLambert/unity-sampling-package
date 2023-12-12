using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace PCGToolkit.Sampling
{
    public class PoissonDiskSampling2D : PoissonDiskSampling<Vector2>
    {
		private readonly System.Random _random;
		private Validator<Parameters> _parametersValidator;

		public PoissonDiskSampling2D(
			System.Random random,
			int maxSamplingTries,
			Validator<Parameters> parametersValidator) : base(random, maxSamplingTries)
		{
			_random = random;
			_parametersValidator = parametersValidator;
		}

		protected override void Validate(Parameters parameters)
		{
			_parametersValidator.Validate(parameters);
		}

		protected override Vector2 GetRandomPointAround(Vector2 point, float minDistance)
		{
			float theta = (float)_random.NextDouble() * 2 * Mathf.PI;
			float x = Mathf.Cos(theta);
			float y = Mathf.Sin(theta);
			return new Vector2(x, y) * minDistance + point;
		}

		protected override float GetMagnitude(Vector2 point, Vector2 sample)
		{
			return (sample - point).magnitude;
		}
    }
}
