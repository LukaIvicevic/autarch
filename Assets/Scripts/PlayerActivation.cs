using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivation : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public CharacterController2D controller;
    public PlayerMovement pm;

    private void Awake()
    {
        if (PlayerManager.Players[controller.PlayerNumber - 1])
        {
            sr.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            cc.enabled = true;
            controller.enabled = true;
            pm.enabled = true;
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
