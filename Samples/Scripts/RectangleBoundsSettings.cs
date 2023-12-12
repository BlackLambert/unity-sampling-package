using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
    [CreateAssetMenu(fileName = "RectangleBoundsSettings", menuName = "Sampling/Examples/RectangleBoundsSettings")]
    public class RectangleBoundsSettings : BoundsSettings2D
    {
		[SerializeField]
		private Vector2 _bounds = new Vector2(10, 10);
		public Vector2 Size => _bounds;
		[SerializeField]
		private Vector2 _bottomLeft = Vector2.zero;

		public override Bounds<Vector2> GetBounds()
		{
			return new RectangleBounds(_bottomLeft, _bounds);
		}

		public override Vector2 GetCenter()
		{
			return _bottomLeft + _bounds / 2;
		}
	}
}
