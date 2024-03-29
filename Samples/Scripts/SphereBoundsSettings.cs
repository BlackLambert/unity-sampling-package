using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
	[CreateAssetMenu(fileName = "SphereBoundsSettings", menuName = "Sampling/Examples/SphereBoundsSettings")]
	public class SphereBoundsSettings : BoundsSettings3D
    {

        [SerializeField]
        private float _radius = 10;
		public float Radius => _radius;
		[SerializeField]
		private Vector3 _center = Vector3.zero;

		public override Bounds<Vector3> GetBounds()
		{
			return new SphereBounds(_center, _radius);
		}

		public override Vector3 GetCenter()
		{
			return _center;
		}
	}
}
