using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public float damage = 10;
    public float speed = 20f;
    public float knockbackModifier = 1;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        AudioManager.instance.Play("Pistol_Shoot");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagedPlayer = collision.GetComponent<CharacterController2D>();
        
        if (damagedPlayer != null)
        {
            damagedPlayer.TakeDamage(damage, firedBy);
            damagedPlayer.Knockback(rb.velocity * knockbackModifier);
        }

        Destroy(gameObject);
    }
}
