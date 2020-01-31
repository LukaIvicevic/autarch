using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoundOver : MonoBehaviour
{
    public GameObject Container;
    public TextMeshProUGUI FirstPlaceText;
    public TextMeshProUGUI SecondPlaceText;
    public TextMeshProUGUI ThirdPlaceText;
    public TextMeshProUGUI FourthPlaceText;

    private float slowMotionTimeScale = 0.25f;

    private void Awake()
    {
        if (PlayerManager.Players[0])
        {
            FirstPlaceText.enabled = true;
        }
        if (PlayerManager.Players[1])
        {
            SecondPlaceText.enabled = true;
        }
        if (PlayerManager.Players[2])
        {
            ThirdPlaceText.enabled = true;
        }
        if (PlayerManager.Players[3])
        {
            FourthPlaceText.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.ScoreLimitReached)
        {
            SetScoreText();
            Container.SetActive(true);
            Time.timeScale = slowMotionTimeScale;
        }
    }

    public void GoAgain()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.LastLoadedLevel);
    }

    public void LevelSelect()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.Scenes.LevelSelectMenu);
    }

    public void Quit()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.Scenes.MainMenu);
    }

    private void SetScoreText()
    {
        Debug.Log("Set");
        int score;
        int playerNumber;
        if (FirstPlaceText.enabled)
        {
            Debug.Log("Set1");
            score = ScoreManager.Scores.OrderByDescending(x => x).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            FirstPlaceText.text = "1st: Player " + playerNumber + " (" + score + ")";
        }
        if (SecondPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(1).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            FirstPlaceText.text = "2nd: Player " + playerNumber + " (" + score + ")";
        }
        if (ThirdPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(2).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            FirstPlaceText.text = "3rd: Player " + playerNumber + " (" + score + ")";
        }
        if (SecondPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(3).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            FirstPlaceText.text = "4th: Player " + playerNumber + " (" + score + ")";
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
