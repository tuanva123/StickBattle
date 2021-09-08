using UnityEngine;

namespace RagdollCreatures
{
	public class Gun : MonoBehaviour, IInteractable
	{
		#region Settings
		public Transform startPosition;
		public GameObject bulletPrefab;

		[Range(10.0f, 200.0f)]
		public float bulletSpeed = 60.0f;
		#endregion

		public void interact()
		{
			Vector2 dir = startPosition.position - transform.position;
			float rotation = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = startPosition.position;
			bullet.transform.rotation = startPosition.rotation;
			dir.Normalize();
			bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
		}
	}
}
