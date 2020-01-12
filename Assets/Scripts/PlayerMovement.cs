using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    private float horizontalMove = 0f;
    private Rigidbody2D rb;

    private bool jump = false;
    private bool jumpHeld = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal_P" + controller.playerNumber) * runSpeed;

        if (Input.GetButtonDown("Jump_P" + controller.playerNumber))
        {
            jump = true;
        }

        jumpHeld = Input.GetButton("Jump_P" + controller.playerNumber);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime);

        if (jump)
        {
            controller.Jump();
            jump = false;
        }

        // "Better Jump" code
        if (rb.velocity.y < -0.01)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0.01 && !jumpHeld)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}