using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : MonoBehaviour, IWeapon
{
    public CharacterController2D owner;
    public Transform firePoint;
    public GameObject bullet;
    public float fireDelay = 1f;

    private float canFireTime = 0;

    private void Awake()
    {
        LoadConfig();
    }

    public void Fire()
    {
        if (Time.time >= canFireTime)
        {
            var firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            firedBullet.GetComponent<SniperBullet>().firedBy = owner;
            canFireTime = Time.time + fireDelay;
        }
    }

    private void LoadConfig()
    {
        fireDelay = SniperRifleConfig.fireDelay;
    }
}
