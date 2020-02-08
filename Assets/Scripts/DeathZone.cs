using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagedPlayer = collision.GetComponent<CharacterController2D>();

        if (damagedPlayer != null)
        {
            damagedPlayer.TakeDamage(500);
        }
    }
}
