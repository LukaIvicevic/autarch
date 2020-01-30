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
        if (Input.GetAxisRaw("Jump_P1") > 0)
        {
            PlayerManager.AddPlayer(1);
            Player1.SetActive(true);
        }

        if (Input.GetAxisRaw("Jump_P2") > 0)
        {
            PlayerManager.AddPlayer(2);
            Player2.SetActive(true);
        }

        if (Input.GetAxisRaw("Jump_P3") > 0)
        {
            PlayerManager.AddPlayer(3);
            Player3.SetActive(true);
        }

        if (Input.GetAxisRaw("Jump_P4") > 0)
        {
            PlayerManager.AddPlayer(4);
            Player4.SetActive(true);
        }

        if (Input.GetButtonDown("Submit_P1") || Input.GetButtonDown("Submit_P2") || Input.GetButtonDown("Submit_P3") || Input.GetButtonDown("Submit_P4"))
        {
            LoadManager.Load(LoadManager.Scenes.LevelSelectMenu);
        }
    }
}
