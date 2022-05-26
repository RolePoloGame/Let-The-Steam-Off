using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public SceneControler.GameScenes SceneToLoad;
    public void LoadScene(SceneControler.GameScenes gameScene) => SceneControler.LoadScene(gameScene);
    public void LoadScene() => LoadScene(SceneToLoad);
    public void LoadSettings() => Debug.LogError("Settings are NOT implemented!");
    public void ExitScene() => Application.Quit();
}
