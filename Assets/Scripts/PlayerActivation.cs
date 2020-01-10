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
            SetPlayerColor();
        }
    }

    private void SetPlayerColor()
    {
        switch (controller.PlayerNumber)
        {
            case 1:
                sr.color = PlayerManager.PlayerColor1;
                break;
            case 2:
                sr.color = PlayerManager.PlayerColor2;
                break;
            case 3:
                sr.color = PlayerManager.PlayerColor3;
                break;
            case 4:
                sr.color = PlayerManager.PlayerColor4;
                break;
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
