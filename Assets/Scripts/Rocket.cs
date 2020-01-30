using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 30;
    public float speed = 20f;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;
    public float explosionRadius = 5f;
    public GameObject explosionPrefab;
    public float explosionDuration = 0.5f;

    private Vector3 originalPosition;

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
            }
        }

        Destroy(gameObject);

        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.localScale = new Vector2(explosionRadius * 2, explosionRadius * 2);
        Destroy(explosion, explosionDuration);
    }

    private void RocketRotation()
    {
        if (rb.velocity.x < 0.0001f && rb.velocity.x > -0.0001f && rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            return;
        } else if (rb.velocity.x < 0.0001f && rb.velocity.x > -0.0001f && rb.velocity.y < 0)
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
}
