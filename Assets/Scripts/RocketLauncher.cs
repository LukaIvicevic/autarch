using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IWeapon
{
    public CharacterController2D owner;
    public Transform firePoint;
    public GameObject rocket;
    public float fireDelay = 1f;

    private float canFireTime = 0;

    public void Fire()
    {
        if (Time.time >= canFireTime)
        {
            AudioManager.instance.Play("Rocket_Fire");
            var firedRocket = Instantiate(rocket, firePoint.position, firePoint.rotation);
            firedRocket.GetComponent<Rocket>().firedBy = owner;
            canFireTime = Time.time + fireDelay;
        }
    }
}
