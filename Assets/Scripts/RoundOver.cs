using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.ScoreLimitReached)
        {
            Container.SetActive(true);
            Time.timeScale = slowMotionTimeScale;
        }
    }

    public void GoAgain()
    {
        ScoreManager.Initialize();
        // TODO: Reset player positions
    }

    public void LevelSelect()
    {
        // TODO: Load LevelSelect scene
    }

    public void Quit()
    {
        // TODO: Load MainMenu scene
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
