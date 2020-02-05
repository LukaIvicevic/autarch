using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.LWRP;

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
	private Transform wallCheck;
	[SerializeField]
	private GameObject ws;
	[SerializeField]
	private Transform deadPosition;
	[SerializeField]
	private Transform[] respawnPositions;
	[SerializeField]
	private TextMeshProUGUI scoreText;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private Light2D light;
	[SerializeField]
	private float minLightIntensity = 0f;
	[SerializeField]
	private float maxLightIntensity = 0.8f;
	[SerializeField]
	private float minLightRadius = 1;
	[SerializeField]
	private float maxLightRadius = 4;
	[SerializeField]
	private Transform healthBar;
	[SerializeField]
	private GameObject healthBarMain;

	private SpriteRenderer sr;
	private CircleCollider2D cc;
	private PlayerMovement pm;

	private bool grounded;
	private bool isTouchingWall;
	private Rigidbody2D rb;
	private bool isFacingRight = true;
	private Vector3 velocity = Vector3.zero;
	private bool isDead = false;
	private float canRespawnTime = 0;
	private float health = 100f;
	private bool isWallSliding = false;
	private Transform weaponSlot;
	private float healthBarInitialWidth = 0;

	const float groundedRadius = .1f;
	const float wallCheckRadius = .2f;
	const float wallSlideFriction = .8f;

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
		light.color = GetPlayerColor();

		weaponSlot = transform.Find("WeaponSlot");

		if (scoreText != null)
		{
			scoreText.text = "Player " + playerNumber + ": " + ScoreManager.GetScore(playerNumber);
		}

		ActivateCharacter();

		health = maxHealth;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		OnLandEvent.AddListener(StopJumpAnimation);

		healthBarInitialWidth = healthBar.localScale.x;
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
		isTouchingWall = false;
		GroundCheck(wasGrounded);
		WallCheck();
	}

	private void GroundCheck(bool wasGrounded)
	{
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

	private void WallCheck()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isTouchingWall = true;
			}
		}
	}

	public void Move(float move)
	{
		// Only control the player if grounded or airControl is turned on
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

		// Increase friction for wall sliding if touching a wall and moving in that direction
		if (isTouchingWall && move != 0 && rb.velocity.y < 0)
		{
			cc.sharedMaterial.friction = wallSlideFriction;

			// Turn collider off then on to reset friction cache
			cc.enabled = false;
			cc.enabled = true;

			isWallSliding = true;
			animator.SetBool("isJumping", false);
		} else
		{
			cc.sharedMaterial.friction = 0f;

			// Turn collider off then on to reset friction cache
			cc.enabled = false;
			cc.enabled = true;

			isWallSliding = false;

			if (!grounded)
			{
				animator.SetBool("isJumping", true);
			}
		}

		animator.SetBool("isWallSliding", isWallSliding);
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

			// Play jump sound
			AudioManager.instance.Play("Player_Jump");
		}

		if (isWallSliding)
		{
			grounded = false;
			isWallSliding = false;

			// Set Y velocity to jump force
			var newVelocity = rb.velocity;
			newVelocity.y = jumpForce;
			if (isFacingRight)
			{
				newVelocity.x = -jumpForce;
			} else
			{
				newVelocity.x = jumpForce;
			}
			rb.velocity = newVelocity;

			// Play jump sound
			AudioManager.instance.Play("Player_Jump");
		}

		if (!grounded && !isWallSliding)
		{
			animator.SetBool("isJumping", true);
		}
	}

	public void TakeDamage(float damage, CharacterController2D damagedByPlayer)
	{
		AudioManager.instance.Play("Player_Hurt");

		health -= damage;

		Debug.Log("Player " + playerNumber + " took " + damage + " damage from Player " + damagedByPlayer.playerNumber + ". Remaining health: " + health + ".");

		if (health <= 0)
		{
			Die();

			if (damagedByPlayer.playerNumber == playerNumber)
			{
				ScoreManager.Suicide(playerNumber);
				UpdateScoreText();
			} else
			{
				ScoreManager.Kill(damagedByPlayer.playerNumber);
				damagedByPlayer.UpdateScoreText();
				damagedByPlayer.UpdateLight();
			}
		}

		UpdateHealthBar();
	}

	public void Die()
	{
		Debug.Log("Player " + playerNumber + " died.");
		weaponSlot.gameObject.GetComponent<Weapon>().rotation = Quaternion.Euler(0, 0, 0);
		SelectWeapon(Weapons.Pistol);
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
		UpdateHealthBar();
	}

	public void UpdateScoreText()
	{
		scoreText.text = "Player " + playerNumber + ": " + ScoreManager.GetScore(playerNumber);
	}

	public void SelectWeapon(string weaponName)
	{
		foreach (Transform weapon in weaponSlot)
		{
			if (weapon.name == weaponName)
			{
				weapon.gameObject.SetActive(true);
				weaponSlot.GetComponent<Weapon>().weapon = weapon.gameObject;
			}
			else
			{
				weapon.gameObject.SetActive(false);
			}
		}
	}

	public void UpdateLight()
	{
		var scoreLimit = ScoreManager.ScoreLimit;
		var playerScore = ScoreManager.GetScore(playerNumber);
		if (scoreLimit > 0 && playerScore >= 0)
		{
			var scorePercentage = (float)playerScore / (float)scoreLimit;

			var intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, scorePercentage);
			light.intensity = intensity;

			var outerRadius = Mathf.Lerp(minLightRadius, maxLightRadius, scorePercentage);
			light.pointLightOuterRadius = outerRadius;
		}
	}

	private void StopJumpAnimation()
	{
		animator.SetBool("isJumping", false);
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
			scoreText.enabled = true;
			light.enabled = true;
			healthBarMain.SetActive(true);
			SetPlayerColor();
			UpdateLight();
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
			light.enabled = false;
			healthBarMain.SetActive(false);
			UpdateLight();
		}
	}

	private void SetPlayerColor()
	{
		var color = GetPlayerColor();
		sr.color = color;
		if (scoreText)
			scoreText.color = color;
	}

	private Color32 GetPlayerColor()
	{
		switch (playerNumber)
		{
			case 1:
				return PlayerManager.PlayerColor1;
			case 2:
				return PlayerManager.PlayerColor2;
			case 3:
				return PlayerManager.PlayerColor3;
			case 4:
				return PlayerManager.PlayerColor4;
			default:
				return PlayerManager.PlayerColor1;
		}
	}

	private void UpdateHealthBar()
	{
		var newScale = Mathf.Lerp(0, healthBarInitialWidth, health / maxHealth);
		healthBar.localScale = new Vector3(newScale, healthBar.localScale.y, healthBar.localScale.z);
	}
}
