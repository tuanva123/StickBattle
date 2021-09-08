using UnityEngine.InputSystem;
using UnityEngine;

namespace RagdollCreatures
{
	/// <summary>
	/// Switches all RagdollCreatures in Scene from ragdoll to active ragdoll or otherwise.
	/// </summary>
	public class RagdollSwitcher : MonoBehaviour
	{
		private bool switcher = false;

		public void OnRagdollSwitch(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				RagdollCreature[] ragdolls = FindObjectsOfType<RagdollCreature>();
				foreach (RagdollCreature ragdoll in ragdolls)
				{
					if (switcher)
					{
						ragdoll.ActivateAllMuscles();
					}
					else
					{
						ragdoll.DeactivateAllMuscles();
					}
				}
				switcher = !switcher;
			}
		}
	}
}
