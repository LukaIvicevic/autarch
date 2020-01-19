using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 10;
    public float speed = 20f;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        originalPosition = gameObject.transform.position;
    }

    private void Update()
    {
        var moveDirection = gameObject.transform.position - originalPosition;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Circle cast and to do damage as explosion
        Debug.Log("Boom");
        //var damagedPlayer = collision.GetComponent<CharacterController2D>();

        //if (damagedPlayer != null)
        //{
        //    damagedPlayer.TakeDamage(damage, firedBy);
        //}

        Destroy(gameObject);
    }
}
