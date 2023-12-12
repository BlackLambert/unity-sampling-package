using UnityEngine;

namespace PCGToolkit.Sampling
{
	public class PoissonDiskSampling3D : PoissonDiskSampling<Vector3>
	{
		private readonly System.Random _random;
		private Validator<Parameters> _parametersValidator;

		public PoissonDiskSampling3D(System.Random random,
			int maxSamplingTries,
			Validator<Parameters> parametersValidator) : base(random, maxSamplingTries)
		{
			_random = random;
			_parametersValidator = parametersValidator;
		}
		
		protected override float GetMagnitude(Vector3 point, Vector3 sample)
		{
			return (sample - point).magnitude;
		}

		protected override void Validate(Parameters parameters)
		{
			_parametersValidator.Validate(parameters);
		}

		protected override Vector3 GetRandomPointAround(Vector3 point, float minDistance)
		{
			float lat = (float)_random.NextDouble() * 2 * Mathf.PI - Mathf.PI;
			float lon = Mathf.Acos(2 * (float)_random.NextDouble() - 1);
			float x = Mathf.Cos(lat) * Mathf.Cos(lon);
			float y = Mathf.Cos(lat) * Mathf.Sin(lon);
			float z = Mathf.Sin(lat);
			return new Vector3(x, y, z) * minDistance + point;
		}
	}
}