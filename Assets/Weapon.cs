using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    public GameObject weapon;
    // private IWeapon weapon;
    private IWeapon wc;
    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<CharacterController2D>();
        //weapon = GetComponent<IWeapon>();
        wc = weapon.GetComponent<IWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1_P" + controller.playerNumber))
        {
            wc.Fire();
        }
    }
}
