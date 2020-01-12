using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public int playerNumber = 1;
	[SerializeField]
	private float maxHealth = 100f;
	[SerializeField]
	private float respawnTime = 5f;
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
	[SerializeField]
	private GameObject ws;
	[SerializeField]
	private Transform deadPosition;
	[SerializeField]
	private Transform[] respawnPositions;

	private SpriteRenderer sr;
	private CircleCollider2D cc;
	private PlayerMovement pm;

	private bool grounded;
	private Rigidbody2D rb;
	private bool isFacingRight = true;
	private Vector3 velocity = Vector3.zero;
	private bool isDead = false;
	private float canRespawnTime = 0;
	private float health = 100f;

	const float groundedRadius = .2f;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		cc = GetComponent<CircleCollider2D>();
		pm = GetComponent<PlayerMovement>();

		ActivateCharacter();

		health = maxHealth;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Update()
	{
		if (isDead && Time.time >= canRespawnTime)
		{
			Respawn();
		}
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
		isDead = true;
		DeactivateCharacter();
		transform.position = deadPosition.position;
		canRespawnTime = Time.time + respawnTime;
	}

	public void Respawn()
	{
		Debug.Log("Player " + playerNumber + " respawned.");
		isDead = false;
		health = maxHealth;
		var respawnPositionIndex = Random.Range(0, respawnPositions.Length);
		transform.position = respawnPositions[respawnPositionIndex].position;
		ActivateCharacter();
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		transform.Rotate(0f, 180f, 0f);
	}

	private void ActivateCharacter()
	{
		if (PlayerManager.Players[playerNumber - 1])
		{
			sr.enabled = true;
			rb.bodyType = RigidbodyType2D.Dynamic;
			cc.enabled = true;
			pm.enabled = true;
			ws.SetActive(true);
			SetPlayerColor();
		}
	}

	private void DeactivateCharacter()
	{
		if (PlayerManager.Players[playerNumber - 1])
		{
			sr.enabled = false;
			rb.bodyType = RigidbodyType2D.Kinematic;
			cc.enabled = false;
			pm.enabled = false;
			ws.SetActive(false);
		}
	}

	private void SetPlayerColor()
	{
		switch (playerNumber)
		{
			case 1:
				sr.color = PlayerManager.PlayerColor1;
				break;
			case 2:
				sr.color = PlayerManager.PlayerColor2;
				break;
			case 3:
				sr.color = PlayerManager.PlayerColor3;
				break;
			case 4:
				sr.color = PlayerManager.PlayerColor4;
				break;
		}
	}
}
