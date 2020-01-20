using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    public GameObject weapon;
    private IWeapon wc;
    private CharacterController2D controller;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<CharacterController2D>();
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        wc = weapon.GetComponent<IWeapon>();
        transform.rotation = rotation;
        RotateWeapon();

        if (Input.GetAxisRaw("Fire1_P" + controller.playerNumber) > 0)
        {
            wc.Fire();
        }
    }

    private void RotateWeapon()
    {
        var horizontalAim = Input.GetAxisRaw("HorizontalAim_P" + controller.playerNumber);
        if (horizontalAim != 0)
        {
            if (horizontalAim == 1)
            {
                rotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = rotation;
            }
            else if (horizontalAim == -1)
            {
                rotation = Quaternion.Euler(0, 180, 0);
                transform.rotation = rotation;
            }
        }

        var verticalAim = Input.GetAxisRaw("VerticalAim_P" + controller.playerNumber);
        if (verticalAim != 0)
        {
            if (verticalAim == 1)
            {
                rotation = Quaternion.Euler(0, 0, 90);
                transform.rotation = rotation;
            }
            else if (verticalAim == -1)
            {
                rotation = Quaternion.Euler(0, 0, 270);
                transform.rotation = rotation;
            }
        }
    }
}
