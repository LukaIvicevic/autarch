using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string LevelName;
    public TextMeshProUGUI ButtonText;

    private void Awake()
    {
        ButtonText.text = LevelName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel()
    {
        // TODO: Use LoadManager
        SceneManager.LoadScene(LevelName);
    }
}
