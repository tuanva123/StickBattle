using UnityEngine;
using UnityEngine.InputSystem;

namespace RagdollCreatures
{
	/// <summary>
	/// Simple script to let GameObject follow the mouse position.
	/// Uses the new Input system.
	/// </summary>
	public class FollowMouse : MonoBehaviour
	{
		#region Internal
		private Vector2 position;
		#endregion

		//void Update()
		//{
		//	transform.position = position;
		//}

		//public void OnMouseMove(InputAction.CallbackContext context)
		//{
		//	position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
		//}
	}
}
