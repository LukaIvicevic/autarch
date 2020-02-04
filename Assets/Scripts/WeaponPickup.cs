using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;
    public int respawnTime = 2;
    public float pickupScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        gameObject.transform.localScale = new Vector3(pickupScale, pickupScale, pickupScale);
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

            DisablePickup();

            Invoke("EnablePickup", respawnTime);
        }
    }

    private void DisablePickup()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void EnablePickup()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
