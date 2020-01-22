using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundOverPanel : MonoBehaviour
{
    public GameObject Container;
    public TextMeshProUGUI FirstPlaceText;
    public TextMeshProUGUI SecondPlaceText;
    public TextMeshProUGUI ThirdPlaceText;
    public TextMeshProUGUI FourthPlaceText;


    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.ScoreLimitReached)
        {
            Container.SetActive(true);
        }
    }
}
