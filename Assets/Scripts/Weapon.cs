using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    public GameObject weapon;
    public Quaternion rotation;
    private IWeapon wc;
    private CharacterController2D controller;

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
        if (!ScoreManager.ScoreLimitReached)
        {
            RotateAndFireWeapon();
        }
    }

    private void RotateAndFireWeapon()
    {
        if (Input.GetAxisRaw("Fire1_P" + controller.playerNumber) > 0)
        {
            wc.Fire();
        }

        Debug.Log(Input.GetAxisRaw("HorizontalAim_P" + controller.playerNumber) + " - " + Input.GetAxisRaw("VerticalAim_P" + controller.playerNumber));

        var horizontalAim = Input.GetAxisRaw("HorizontalAim_P" + controller.playerNumber);
        var verticalAim = Input.GetAxisRaw("VerticalAim_P" + controller.playerNumber);

        // Right
        if (horizontalAim == 1 && verticalAim == 0)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = rotation;
            return;
        }
        
        // Left
        if (horizontalAim == -1 && verticalAim == 0)
        {
            rotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = rotation;
            return;
        }

        // Up
        if (verticalAim == 1 && horizontalAim == 0)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            transform.rotation = rotation;
            return;
        }

        // Down
        if (verticalAim == -1 && horizontalAim == 0)
        {
            rotation = Quaternion.Euler(0, 0, 270);
            transform.rotation = rotation;
            return;
        }

        // Up Right
        if (verticalAim > 0.4 && verticalAim < 0.8 && horizontalAim > 0.4 && horizontalAim < 0.8)
        {
            rotation = Quaternion.Euler(0, 0, 45);
            transform.rotation = rotation;
            return;
        }

        // Up Left
        if (verticalAim > 0.4 && verticalAim < 0.8 && horizontalAim < -0.4 && horizontalAim > -0.8)
        {
            rotation = Quaternion.Euler(0, 0, 135);
            transform.rotation = rotation;
            return;
        }

        // Down Right
        if (verticalAim < -0.4 && verticalAim > -0.8 && horizontalAim > 0.4 && horizontalAim < 0.8)
        {
            rotation = Quaternion.Euler(0, 0, -45);
            transform.rotation = rotation;
        }

        // Down Left
        if (verticalAim < -0.4 && verticalAim > -0.8 && horizontalAim < -0.4 && horizontalAim > -0.8)
        {
            rotation = Quaternion.Euler(0, 0, -135);
            transform.rotation = rotation;
        }
    }
}
