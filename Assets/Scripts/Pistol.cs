using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public CharacterController2D owner;
    public Transform firePoint;
    public GameObject bullet;
    public float fireDelay = 1f;

    private float canFireTime = 0;

    public void Fire()
    {
        if (Time.time >= canFireTime)
        {
            var firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            firedBullet.GetComponent<PistolBullet>().firedBy = owner;
            canFireTime = Time.time + fireDelay;
        }
    }
}
