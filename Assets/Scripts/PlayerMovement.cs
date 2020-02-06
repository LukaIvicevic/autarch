﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

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
        if (!ScoreManager.ScoreLimitReached)
        {
            HandleMovementInput();
        } else
        {
            horizontalMove = 0;
            controller.Move(horizontalMove * Time.fixedDeltaTime);
        }
        HandleAnimation();
    }

    void FixedUpdate()
    {
        if (!ScoreManager.ScoreLimitReached)
        {
            HandleMovementAndJump();
        }
    }

    private void HandleMovementInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal_P" + controller.playerNumber) * runSpeed;
        jumpHeld = Input.GetAxisRaw("Jump_P" + controller.playerNumber) > 0;
    }

    private void HandleAnimation()
    {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 1)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (Input.GetAxisRaw("Jump_P" + controller.playerNumber) > 0)
        {
            jump = true;
        }
    }

    private void HandleMovementAndJump()
    {
        // Move our character
        if (PlayerManager.CanControl)
        {
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
}