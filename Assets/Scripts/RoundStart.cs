using TMPro;
using UnityEngine;

public class RoundStart : MonoBehaviour
{
    public bool roundStarted = false;
    public TextMeshProUGUI text;

    private float timer;
    private bool canCount = false;
    private float countdownTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0 && canCount)
        {
            timer -= Time.deltaTime;
            if (timer >= 0)
            {
                text.text = timer.ToString("F");
            }
        } else if (timer <= 0)
        {
            canCount = false;
            text.text = "0.00";
            roundStarted = true;
            text.enabled = false;
            PlayerManager.CanControl = true;
        }
    }

    private void StartTimer()
    {
        timer = countdownTime;
        text.text = timer.ToString("F");
        text.enabled = true;
        roundStarted = false;
        canCount = true;
        PlayerManager.CanControl = false;
    }
}
