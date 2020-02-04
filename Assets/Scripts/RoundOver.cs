using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoundOver : MonoBehaviour
{
    public GameObject container;
    public GameObject firstPlaceGameObject;
    public GameObject secondPlaceGameObject;
    public GameObject thirdPlaceGameObject;
    public GameObject fourthPlaceGameObject;
    public TextMeshProUGUI firstPlaceText;
    public TextMeshProUGUI secondPlaceText;
    public TextMeshProUGUI thirdPlaceText;
    public TextMeshProUGUI fourthPlaceText;

    private float slowMotionTimeScale = 0.25f;
    private bool isShowing = false;

    private void Awake()
    {
        firstPlaceText = firstPlaceGameObject.GetComponent<TextMeshProUGUI>();
        secondPlaceText = secondPlaceGameObject.GetComponent<TextMeshProUGUI>();
        thirdPlaceText = thirdPlaceGameObject.GetComponent<TextMeshProUGUI>();
        fourthPlaceText = fourthPlaceGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.ScoreLimitReached && !isShowing)
        {
            container.SetActive(true);
            Time.timeScale = slowMotionTimeScale;
            SetScoreText();
            isShowing = true;
        }
    }

    public void GoAgain()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.LastLoadedLevel);
        isShowing = false;
    }

    public void LevelSelect()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.Scenes.LevelSelectMenu);
        isShowing = false;
    }

    public void Quit()
    {
        ScoreManager.Initialize();
        LoadManager.Load(LoadManager.Scenes.MainMenu);
        isShowing = false;
    }

    private void SetScoreText()
    {
        EnableText();
        int score;
        int playerNumber;
        if (firstPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            firstPlaceText.text = "1st: Player " + playerNumber + " (" + score + ")";
        }
        if (secondPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(1).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            secondPlaceText.text = "2nd: Player " + playerNumber + " (" + score + ")";
        }
        if (thirdPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(2).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            thirdPlaceText.text = "3rd: Player " + playerNumber + " (" + score + ")";
        }
        if (secondPlaceText.enabled)
        {
            score = ScoreManager.Scores.OrderByDescending(x => x).Skip(3).FirstOrDefault();
            playerNumber = Array.IndexOf(ScoreManager.Scores, score) + 1;
            fourthPlaceText.text = "4th: Player " + playerNumber + " (" + score + ")";
        }
    }

    private void EnableText()
    {
        if (PlayerManager.Players[0])
        {
            firstPlaceGameObject.SetActive(true);
        }
        if (PlayerManager.Players[1])
        {
            secondPlaceGameObject.SetActive(true);
        }
        if (PlayerManager.Players[2])
        {
            thirdPlaceGameObject.SetActive(true);
        }
        if (PlayerManager.Players[3])
        {
            fourthPlaceGameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
