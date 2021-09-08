using UnityEngine;

namespace RagdollCreatures
{
	public class Bullet : MonoBehaviour
	{ 
		void OnCollisionEnter2D(Collision2D collider)
		{
			Destroy(gameObject);
		}
	}
}
