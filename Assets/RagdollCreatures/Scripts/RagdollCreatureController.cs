using UnityEngine;
using UnityEngine.InputSystem;

namespace RagdollCreatures
{
	/// <summary>
	/// Generic controller for RagdollLimbs.
	/// 
	/// Contains many adjustment possibilities to cover as many use cases as possible.
	/// Play around with the settings in the inspector to get a feeling for what they do.
	/// For a more detailed description look at the documentation, code comments or video tutorials.
	/// </summary>
	[RequireComponent(typeof(RagdollCreature))]
	public class RagdollCreatureController : MonoBehaviour
	{
		#region Movement
		[Header("Movement")]
		[Range(0, 100)]
		public int movementSpeed = 18;

		// How fast should the direction be changed
		[Range(0, 100)]
		public int movementLerpFactor = 25;

		// How fast should the player move in the air
		[Range(0, 100)]
		public int jumpMovementSpeed = 8;

		// Interval in which jumping is allowed
		[Range(0f, 3.0f)]
		public float jumpDelay = 0.75f;
		private float lastJumpTime;

		[Range(0, 1000)]
		public int jumpForce = 200;
        [Range(0, 100)]
        public int addForceX = 20;

        // Modifies the gravity while jumping to get better jumping results.
        // Tipp: Use a value over 1 to get a smooth and good looking jump.
        [Range(-10.0f, 10.0f)]
		public float jumpGravityScale = 2f;

		// Modifies the gravity while falling.
		// Tipp: Use a value over 1 to speed up the fall. Feels better than the real physics. 
		[Range(-10.0f, 10.0f)]
		public float fallGravityScale = 2f;

		// Modifies the gravity while grounded.
		// Tipp: Use a low value between 0 and 1 for smoother walking.
		[Range(-10.0f, 10.0f)]
		public float groundGravityScale = 0.5f;

		public bool isRemoveYVelocityBeforeJumping = true;

		public bool isRemoveXVelocityBeforeDirectionSwitch = true;

		// This is not yet fully developed.
		// Actually you can already achieve good results with a low groundGravityScale.
		public bool isSmootherWalking = false;
		#endregion

		#region Internal
		private RagdollCreature creature;

		public Vector2 moveVector;
		#endregion

		void Awake()
		{
			creature = GetComponent<RagdollCreature>();
		}

		void OnDestroy() { }

		void Update()
		{
			// Animations
			UpdateAnimations();
		}
		void FixedUpdate()
		{
			Vector2 horizontalMove = new Vector2(moveVector.x, 0);
			float speed = creature.isGrounded ? movementSpeed : jumpMovementSpeed;

			Rigidbody2D centerOfMass = creature.centerOfMass?.rigidbody;
			if (null != centerOfMass)
			{
				// Reduce Y velocity of limbs for smoother walking
				// To achieve a similar effect you could also reduce the friction from the floor.
				// You could also adjust the groundGravityScale.
				if (isSmootherWalking && creature.isGrounded)
				{
					foreach (RagdollLimb limb in creature.ragdollLimbs)
					{
						if (limb.isMuscleActive && limb.isActiveGroundDetection)
						{
							Rigidbody2D limbRigidbody = limb.rigidbody;
							float velocityY = 0.0f;
							float smoothY = Mathf.SmoothDamp(limbRigidbody.velocity.y, 0, ref velocityY, Time.fixedDeltaTime);
							limbRigidbody.velocity = new Vector2(limbRigidbody.velocity.x, smoothY);
						}
					}
				}

				// Adjust gravity scaling.
				// Jump with normal gravity (gravityScale == 1) is more real but less fun :)
				foreach (RagdollLimb limb in creature.ragdollLimbs)
				{
					if (!creature.isGrounded)
					{
						if (limb.rigidbody.velocity.y < 0)
						{
							limb.rigidbody.gravityScale = fallGravityScale;
						}
						else if (limb.rigidbody.velocity.y > 0)
						{
							limb.rigidbody.gravityScale = jumpGravityScale;
						}
					}
					else
					{
						limb.rigidbody.gravityScale = groundGravityScale;
					}
				}

				if (isRemoveXVelocityBeforeDirectionSwitch && 
					(centerOfMass.velocity.x > 0 && horizontalMove.x < 0
					|| centerOfMass.velocity.x < 0 && horizontalMove.x > 0))
				{
					foreach (RagdollLimb limb in creature.ragdollLimbs)
					{
						limb.rigidbody.velocity = new Vector2(0, centerOfMass.velocity.y);
					}
				}

				// Move
				centerOfMass.velocity = Vector2.Lerp(
					centerOfMass.velocity,
					new Vector2(horizontalMove.x * speed, centerOfMass.velocity.y),
					Time.deltaTime * movementLerpFactor);

				// Only jump if Creature is grounded and the jump delay is over
				if (moveVector.y > 0 && creature.isGrounded && Time.time >= lastJumpTime + jumpDelay)
				{
					if (isRemoveYVelocityBeforeJumping)
					{
						foreach (RagdollLimb limb in creature.ragdollLimbs)
						{
							limb.rigidbody.velocity = new Vector2(centerOfMass.velocity.x, 0);
						}
					}

					// The actual jump
					centerOfMass.AddForce(new Vector2(horizontalMove.x, Vector2.up.y) * jumpForce, ForceMode2D.Impulse);
					lastJumpTime = Time.time;
                    if(gameObject.GetComponent<Character>().id == 0)
                    SoundController.Instance.PlaySfx(SoundController.Instance.playerJump);
                }
            }
		}

        //jump add force
        public void JumpAddForce(float _jumpForce = 200)
        {
            Vector2 horizontalMove = new Vector2(moveVector.x, 0);
            Rigidbody2D centerOfMass = creature.centerOfMass?.rigidbody;
            centerOfMass.AddForce(new Vector2(horizontalMove.x, Vector2.up.y) * _jumpForce, ForceMode2D.Impulse);
        }
        public void AddForce()
        {
            Vector2 horizontalMove = new Vector2(moveVector.x, 0);
            Rigidbody2D centerOfMass = creature.centerOfMass?.rigidbody;
            centerOfMass.AddForce(new Vector2(horizontalMove.x, Vector2.up.y) * jumpForce, ForceMode2D.Impulse);
        }
        public void Punch()
        {
            
        }

        void UpdateAnimations()
		{
			creature.PlayWalkAnimation(moveVector);
		}

		/// <summary>
		/// Get the move vector from the InputActions.
		/// </summary>
		/// <param name="context"></param>
		public void OnMove(InputAction.CallbackContext context)
		{
			moveVector = context.ReadValue<Vector2>();
			Debug.Log("moveVector:" + moveVector);
		}

		public void OnRagdollLimbCollisionEnter2D(object sender, Collision2D col)
		{
			//Debug.Log("OnRagdollLimbCollisionEnter2D: " + col.ToString());
		}

		public void OnRagdollLimbCollisionExit2D(object sender, Collision2D col)
		{
			//Debug.Log("OnRagdollLimbCollisionExit2D: " + col.ToString());
		}

		public void OnTriggerEnter2D(Collider2D col)
		{
			//Debug.Log("OnTriggerEnter2D: " + col.ToString());
		}

		public void OnTriggerExit2D(Collider2D col)
		{
			//Debug.Log("OnTriggerExit2D: " + col.ToString());
		}
	}
}
