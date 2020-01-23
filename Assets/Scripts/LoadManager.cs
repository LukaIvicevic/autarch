using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadManager
{
    public enum Scenes
    {
        MainMenu,
        PlayerSelectMenu,
        LevelSelectMenu,
        Loading,
        Level1
    }

    private static Action onLoadManagerCallback;

    public static void LoadManagerCallback()
    {
        if (onLoadManagerCallback != null)
        {
            onLoadManagerCallback();
            onLoadManagerCallback = null;
        }
    }

    public static void Load(Scenes scene)
    {
        // Display loading screen
        onLoadManagerCallback = () =>
        {
            SceneManager.LoadScene(Scenes.Loading.ToString());
        };

        // Load target scene
        SceneManager.LoadScene(scene.ToString());
    }

    public static void Load(string scene)
    {
        // Display loading screen
        onLoadManagerCallback = () =>
        {
            SceneManager.LoadScene(Scenes.Loading.ToString());
        };

        // Load target scene
        SceneManager.LoadScene(scene);
    }
}
