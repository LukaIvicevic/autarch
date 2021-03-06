﻿using UnityEngine;

public class Spikes : MonoBehaviour, IWeapon
{
    public float damage = 15;
    public float knockbackModifier = 50;
    public float canDamageRate = 0.5f;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;

    private float[] canDamageTime = { 0, 0, 0, 0 };

    private void Awake()
    {
        LoadConfig();
    }

    public void Fire()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagedPlayer = collision.GetComponent<CharacterController2D>();

        if (damagedPlayer != null && canDamageTime[damagedPlayer.playerNumber - 1] < Time.time)
        {
            damagedPlayer.TakeDamage(damage, firedBy);
            damagedPlayer.Knockback(rb.velocity * knockbackModifier);
            canDamageTime[damagedPlayer.playerNumber - 1] = Time.time + canDamageRate;
        }
    }

    private void LoadConfig()
    {
        damage = SpikesConfig.damage;
        knockbackModifier = SpikesConfig.knockbackModifier;
        canDamageRate = SpikesConfig.canDamageRate;
    }
}
