using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float damage = 20;
    public float speed = 50f;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        AudioManager.instance.Play("Sniper_Shoot");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagedPlayer = collision.GetComponent<CharacterController2D>();

        if (damagedPlayer != null)
        {
            damagedPlayer.TakeDamage(damage, firedBy);
        }

        Destroy(gameObject);
    }
}
