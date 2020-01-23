using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string levelName;
    public TextMeshProUGUI ButtonText;

    private void Awake()
    {
        ButtonText.text = levelName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel()
    {
        LoadManager.Load(levelName);
    }
}
