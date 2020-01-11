using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject bullet;
    public float fireDelay = 1f;

    private float canFireTime = 0;

    public void Fire()
    {
        if (Time.time >= canFireTime)
        {
            Debug.Log("Pew Pew");
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            canFireTime = Time.time + fireDelay;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
