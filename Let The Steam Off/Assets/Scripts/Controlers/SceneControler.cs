using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    public static SceneControler Load { 
        get; 
        private set; 
    }

    private void Awake()
    {
        Load = this;
        Load.Scene(GameScenes.sc_MainMenu);
    }
    [HideInInspector]
    public float Progress = 0.0f;
    [HideInInspector]
    public bool isDone = false;

    private static GameScenes SceneToLoad;

    public void Scene(GameScenes scene)
    {
        if (scene == GameScenes.sc_LoadScreen)
        {
            Coroutine coroutine = StartCoroutine(LoadAsync());
            return;
        }

        SceneToLoad = scene;
        SceneManager.LoadScene(GameScenes.sc_LoadScreen.ToString());
    }

    private IEnumerator LoadAsync()
    {
        Progress = 0.0f;
        isDone = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad.ToString());

        while (!operation.isDone)
        {
            Progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }

        isDone = true;
        Progress = 1.0f;
    }


    public enum GameScenes
    {
        sc_MainMenu,
        sc_MainScene,
        sc_LoadScreen
    }


}
