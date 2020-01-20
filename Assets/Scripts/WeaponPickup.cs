using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickupWeapon(collision);
    }

    private void PickupWeapon(Collider2D collision)
    {
        var playerObject = collision.gameObject;

        if (playerObject.tag == "Player")
        {
            playerObject.GetComponent<CharacterController2D>().SelectWeapon(weapon.name);

            // TODO: Respawn
            Destroy(gameObject);
        }
    }
}
