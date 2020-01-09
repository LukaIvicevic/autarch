using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    // Update is called once per frame
    void Update()
    {
        // Add players when they press fire button
        if (Input.GetButtonDown("Fire1_P1"))
        {
            PlayerManager.AddPlayer(1);
            Player1.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1_P2"))
        {
            PlayerManager.AddPlayer(2);
            Player2.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1_P3"))
        {
            PlayerManager.AddPlayer(3);
            Player3.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1_P4"))
        {
            PlayerManager.AddPlayer(4);
            Player4.SetActive(true);
        }

        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
