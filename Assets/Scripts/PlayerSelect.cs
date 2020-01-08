using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Add players when they press fire button
        if (Input.GetButtonDown("Fire1_P1"))
        {
            PlayerManager.AddPlayer(1);
            Debug.Log("Player1 Added");
        }
        if (Input.GetButtonDown("Fire1_P2"))
        {
            PlayerManager.AddPlayer(2);
            Debug.Log("Player2 Added");
        }
        if (Input.GetButtonDown("Fire1_P3"))
        {
            PlayerManager.AddPlayer(3);
            Debug.Log("Player3 Added");
        }
        if (Input.GetButtonDown("Fire1_P4"))
        {
            PlayerManager.AddPlayer(4);
            Debug.Log("Player4 Added");
        }
    }
}
