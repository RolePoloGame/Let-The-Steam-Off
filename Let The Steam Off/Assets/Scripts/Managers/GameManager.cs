using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[RequireComponent(typeof(PAR))]
[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    private static bool InitLoad = false;
    public bool initLoad;

    [ShowIf("initLoad")]
    public SceneControler.GameScenes scene = SceneControler.GameScenes.sc_MainMenu;
    public static SceneControler.GameScenes SceneToLoad;
    private void Start()
    {
        InitiateGameManager();
    }

    [MenuItem("Edit/Play-Unplay, But From Prelaunch Scene %0")]
    private void InitiateGameManager()
    {
        InitLoad = initLoad;
        if (!InitLoad)
        {
            SceneToLoad = SceneControler.GameScenes.sc_MainMenu;
            return;
        }

        SceneToLoad = scene;
    }
}
