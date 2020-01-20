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
        var playerObject = collision.gameObject;

        if (playerObject.tag == "Player")
        {
            var weaponSlot = playerObject.transform.Find("WeaponSlot");
            foreach (Transform playerWeapon in weaponSlot)
            {
                if (playerWeapon.name == weapon.name)
                {
                    playerWeapon.gameObject.SetActive(true);
                    weaponSlot.GetComponent<Weapon>().weapon = playerWeapon.gameObject;
                }
                else
                {
                    playerWeapon.gameObject.SetActive(false);
                }
            }

            Debug.Log("Pickup " + weapon.name);


            Destroy(gameObject);
        }
    }
}
