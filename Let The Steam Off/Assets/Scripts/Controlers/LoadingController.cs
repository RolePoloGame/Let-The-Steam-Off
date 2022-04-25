using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneControler;

public class LoadingController : MonoBehaviour
{
    public static float Progress = 0.0f;
    public static bool isDone = false;
    public static LoadingController instance;
    public static GameScenes SceneToLoad = GameScenes.sc_MainMenu;

    private void Awake()
    {
        LoadScene(SceneToLoad);
    }
    /// <summary>
    /// Closes previously loaded scene, and loads <paramref name="scene"/>.
    /// </summary>
    /// <param name="scene"><see cref="GameScenes"/> to load</param>
    public void LoadScene(GameScenes scene = GameScenes.sc_MainMenu)
    {
        Debug.Log($"Loading: {SceneToLoad}...");
        _ = StartCoroutine(LoadAsync());
    }

    /// <summary>
    /// Asynchronously loads <see cref="SceneToLoad"/>. Needs to be started as Coroutine.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadAsync()
    {
        Progress = 0.0f;
        isDone = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad.ToString());
        Debug.Log($"Async loading: {SceneToLoad}");
        while (!operation.isDone)
        {
            Progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log($"Loading... [{Progress}]");
            yield return null;
        }
        Debug.Log($"Loaded: {SceneToLoad}");
        isDone = true;
        Progress = 1.0f;
    }



}
