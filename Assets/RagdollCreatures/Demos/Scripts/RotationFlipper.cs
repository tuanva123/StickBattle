using UnityEngine;

namespace RagdollCreatures
{
	/// <summary>
	/// Flip GameObjects by specified rotation and axes.
	/// 
	/// Example: Sword always faced the correct direction.
	/// </summary>
	public class RotationFlipper : MonoBehaviour
	{
		public enum FlipMode { X, Y, Z }

		#region Properties
		public bool activeRotationFlip = false;

		[Range(-180.0f, 180.0f)]
		public float minRotation = -90.0f;

		[Range(-180.0f, 180.0f)]
		public float endRotation = 90.0f;

		public FlipMode flipMode;
		#endregion

		void Update()
		{
			if (activeRotationFlip)
			{
				float rotation = Vector2.SignedAngle(transform.right, Vector2.right);
				if (rotation < minRotation || rotation > endRotation)
				{
					switch (flipMode)
					{
						case FlipMode.X:
							transform.localScale = new Vector3(
								1,
								-1,
								1);
							break;

						case FlipMode.Y:
							transform.localScale = new Vector3(
								-1,
								1,
								1);
							break;

						case FlipMode.Z:
							transform.localScale = new Vector3(
								1,
								1,
								-1);
							break;
					}
				}
				else
				{
					transform.localScale = new Vector3(
						1,
						1,
						1);
				}
			}
		}
	}
}