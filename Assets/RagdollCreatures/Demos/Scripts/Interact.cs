using UnityEngine;
using UnityEngine.InputSystem;

namespace RagdollCreatures
{
	[RequireComponent(typeof(Collider2D))]
	public class Interact : MonoBehaviour
	{
		#region Settings
		public GameObject root;

		public Transform parent;
		#endregion

		#region Internal
		private GameObject nearestInteractable;
		private GameObject currentInteractable;

		private Vector2 aimPosition;
		#endregion

		public void OnAttack(InputAction.CallbackContext context)
		{
			if (null != currentInteractable)
			{
				IInteractable interactable = currentInteractable.GetComponent<IInteractable>();
				if (null != interactable)
				{
					interactable.interact();
				}
			}
		}

		public void OnAim(InputAction.CallbackContext context)
		{
			aimPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
		}

		public void OnInteract(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				if (null != currentInteractable)
				{
					foreach (Collider2D collider in root.GetComponentsInChildren<Collider2D>())
					{
						Physics2D.IgnoreCollision(currentInteractable.GetComponent<Collider2D>(), collider, false);
					}

					Rigidbody2D rb = currentInteractable.GetComponent<Rigidbody2D>();
					if (null != rb)
					{
						rb.isKinematic = false;
					}

					RotationFlipper rotationFlipper = currentInteractable.GetComponent<RotationFlipper>();
					if (null != rotationFlipper)
					{
						rotationFlipper.activeRotationFlip = false;
					}

					currentInteractable.transform.parent = null;
					currentInteractable = null;
				}

				if (null != nearestInteractable && null == currentInteractable)
				{
					foreach (Collider2D collider in root.GetComponentsInChildren<Collider2D>())
					{
						Physics2D.IgnoreCollision(nearestInteractable.GetComponent<Collider2D>(), collider);
					}

					nearestInteractable.transform.SetParent(parent, false);
					nearestInteractable.transform.position = parent.position;

					Rigidbody2D rb = nearestInteractable.GetComponent<Rigidbody2D>();
					if (null != rb)
					{
						rb.isKinematic = true;
					}

					Equipable equipable = nearestInteractable.GetComponent<Equipable>();
					if (null != equipable)
					{
						nearestInteractable.transform.rotation = Quaternion.Euler(0.0f, 0.0f, equipable.rotationOffset + parent.rotation.eulerAngles.z);
					}

					RotationFlipper rotationFlipper = nearestInteractable.GetComponent<RotationFlipper>();
					if (null != rotationFlipper)
					{
						rotationFlipper.activeRotationFlip = true;
					}

					currentInteractable = nearestInteractable;
				}
			}
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag("Equipable"))
			{
				nearestInteractable = col.gameObject;
			}
		}

		public void OnTriggerExit2D(Collider2D col)
		{
			if (col.CompareTag("Equipable") && col.gameObject == nearestInteractable)
			{
				nearestInteractable = null;
			}
		}
	}
}