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
        wc = weapon.GetComponent<IWeapon>();
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Rotate weapon based on input
        transform.rotation = rotation;

        if (Input.GetButtonDown("Fire1_P" + controller.playerNumber))
        {
            wc.Fire();
        }
    }
}
