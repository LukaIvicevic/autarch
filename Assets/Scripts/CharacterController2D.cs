using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public int playerNumber = 1;
	[SerializeField]
	private float health = 100;
	[SerializeField]
	private float jumpForce = 5f;
	[Range(0, .3f)]
	[SerializeField]
	private float movementSmoothing = .05f;
	[SerializeField]
	private bool airControl = false;
	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private Transform groundCheck;

	private bool grounded;
	private Rigidbody2D rb;
	private bool isFacingRight = true;
	private Vector3 velocity = Vector3.zero;

	const float groundedRadius = .2f;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move)
	{
		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
	}

	public void Jump()
	{
		if (grounded)
		{
			grounded = false;

			// Set Y velocity to jump force
			var newVelocity = rb.velocity;
			newVelocity.y = jumpForce;
			rb.velocity = newVelocity;
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;

		Debug.Log("Player " + playerNumber + " took " + damage + " damage. Remaining health: " + health + ".");

		if (health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Debug.Log("Player " + playerNumber + " died.");
	}

	public void Respawn()
	{
		Debug.Log("Player " + playerNumber + " respawned.");
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		transform.Rotate(0f, 180f, 0f);
	}
}
