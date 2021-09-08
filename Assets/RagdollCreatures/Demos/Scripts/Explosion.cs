using UnityEngine;
using UnityEngine.InputSystem;

namespace RagdollCreatures
{
	/// <summary>
	/// Simple explosion script to fling ragdolls around.
	/// </summary>
	public class Explosion : MonoBehaviour
	{
		#region Settings
		[Range(0.0f, 10.0f)]
		public float explosionRadius = 2;

		[Range(0, 150)]
		public float explosionForce = 100;
		#endregion

		public void OnExplode(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
				foreach (Collider2D collider in colliders)
				{
					// Only RagdollCreatures are affected by the explosion
					// You can extend the script to every Rigidbody you want
					RagdollLimb limb = collider.GetComponent<RagdollLimb>();
					if (null != limb && limb.isCenterOfRagdoll)
					{
						Vector2 dir = limb.rigidbody.transform.position - transform.position;
						limb.rigidbody.AddForce(dir * explosionForce, ForceMode2D.Impulse);
					}
				}
			}
		}
	}
}
