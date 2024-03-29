using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
	[CreateAssetMenu(fileName = "CubeBoundsSettings", menuName = "Sampling/Examples/CubeBoundsSettings")]
	public class CubeBoundsSettings : BoundsSettings3D
    {

        [SerializeField]
        private Vector3 _bounds = new Vector3(10, 10, 10);
		public Vector3 Size => _bounds;
		[SerializeField]
		private Vector3 _leftFront = Vector3.zero;

		public override Bounds<Vector3> GetBounds()
		{
			return new CubeBounds(_leftFront, _bounds);
		}

		public override Vector3 GetCenter()
		{
			return _leftFront + _bounds / 2;
		}
	}
}
