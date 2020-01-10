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
    public GameObject w;

    private void Awake()
    {
        if (PlayerManager.Players[controller.PlayerNumber - 1])
        {
            sr.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            cc.enabled = true;
            controller.enabled = true;
            pm.enabled = true;
            w.SetActive(true);
            SetPlayerColor();
        }
    }

    private void SetPlayerColor()
    {
        Debug.Log("in");
        switch (controller.PlayerNumber)
        {
            case 1:
                Debug.Log("set1");
                sr.color = PlayerManager.PlayerColor1;
                break;
            case 2:
                Debug.Log("set2");
                sr.color = PlayerManager.PlayerColor2;
                break;
            case 3:
                Debug.Log("set3");
                sr.color = PlayerManager.PlayerColor3;
                break;
            case 4:
                Debug.Log("set4");
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
