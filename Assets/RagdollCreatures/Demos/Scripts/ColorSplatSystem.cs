using System.Collections.Generic;
using UnityEngine;

namespace RagdollCreatures
{
	public class ColorSplatSystem : MonoBehaviour
	{
		#region Settings
		public ParticleSystem hitParticleSystemPrefab;
		#endregion

		#region Internal
		private Dictionary<Color, ParticleSystem> systems = new Dictionary<Color, ParticleSystem>();
		#endregion

		public void doColorSplat(RagdollLimb limb, Collision2D col)
		{
			if (col.collider.tag == "Bullet")
			{
				SpriteRenderer spriteRenderer = limb.GetComponent<SpriteRenderer>();
				if (null != spriteRenderer)
				{
					Color color = spriteRenderer.color;
					ParticleSystem system;
					if (!systems.TryGetValue(color, out system))
					{
						system = Instantiate(hitParticleSystemPrefab);
						systems.Add(color, system);
					}
					ParticleSystem.MainModule main = system.main;
					main.startColor = color;
					ContactPoint2D point = col.GetContact(0);
					system.transform.position = point.point;
					system.transform.rotation = col.collider.gameObject.transform.rotation;
				
					system.Play();
				}

			}
			
		}
	}
}
