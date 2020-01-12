﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public float damage = 10;
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterController2D>();
        
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
