using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 30;
    public float speed = 20f;
    public float knockbackModifier = 1;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;
    public float explosionRadius = 5f;
    public GameObject explosionPrefab;
    public float explosionDuration = 0.5f;

    private Vector3 originalPosition;

    private void Awake()
    {
        LoadConfig();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        originalPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        RocketRotation();
    }

    private void OnTriggerEnter2D()
    {
        Explode();
    }

    private void Explode()
    {
        AudioManager.instance.Play("Rocket_Explosion");

        var collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var c in collisions)
        {
            var playerController = c.GetComponent<CharacterController2D>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage, firedBy);

                Vector2 heading = playerController.transform.position - transform.position;
                playerController.Knockback(GetKnockback(heading));
            }
        }

        Destroy(gameObject);

        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.localScale = new Vector2(explosionRadius * 2, explosionRadius * 2);
        Destroy(explosion, explosionDuration);
    }

    private Vector2 GetKnockback(Vector2 heading)
    {
        float xSign;
        float ySign;

        if (heading.x > 0)
        {
            xSign = 1;
        }
        else
        {
            xSign = -1;
        }
        if (heading.y > 0)
        {
            ySign = 1;
        }
        else
        {
            ySign = -1;
        }

        return new Vector2(knockbackModifier * xSign, knockbackModifier * ySign);
    }

    private void RocketRotation()
    {
        if (rb.velocity.x < 0.0001f && rb.velocity.x > -0.0001f && rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            return;
        }
        else if (rb.velocity.x < 0.0001f && rb.velocity.x > -0.0001f && rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            return;
        }

        var moveDirection = gameObject.transform.position - originalPosition;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void LoadConfig()
    {
        damage = RocketLauncherConfig.damage;
        speed = RocketLauncherConfig.speed;
        knockbackModifier = RocketLauncherConfig.knockbackModifier;
        explosionRadius = RocketLauncherConfig.explosionRadius;
        explosionDuration = RocketLauncherConfig.explosionDuration;
    }
}
