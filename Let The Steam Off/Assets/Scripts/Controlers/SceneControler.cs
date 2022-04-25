using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[InitializeOnLoad]
public static class SceneControler
{
    public static void LoadScene(GameScenes scene = GameScenes.sc_MainMenu)
    {
        if (scene == GameScenes.sc_LoadingScreen)
            return;
        LoadingController.SceneToLoad = scene;
        SceneManager.LoadScene(GameScenes.sc_LoadingScreen.ToString(), LoadSceneMode.Single);
    }

    public enum GameScenes
    {
        sc_MainMenu,
        sc_MainScene,
        sc_LoadingScreen,
        sc_MovementTestScene,
        sc_ScreenController
    }
}
