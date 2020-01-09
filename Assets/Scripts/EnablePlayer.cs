using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayer : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Players[0])
        {
            Player1.SetActive(true);
        }

        if (PlayerManager.Players[1])
        {
            Player2.SetActive(true);
        }

        if (PlayerManager.Players[2])
        {
            Player3.SetActive(true);
        }

        if (PlayerManager.Players[3])
        {
            Player4.SetActive(true);
        }
    }
}
