// IN YOUR EDITOR FOLDER, have SimpleEditorUtils.cs.
// paste in this text.
// to play, HIT COMMAND-ZERO rather than command-P
// (the zero key, is near the P key, so it's easy to remember)
// simply insert the actual name of your opening scene
// "__preEverythingScene" on the second last line of code below.

using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class SimpleEditorUtils
{
    // click command-0 to go to the prelaunch scene and then play
    //public static SceneControler.GameScenes Scene = SceneControler.GameScenes.sc_MainMenu;

    [MenuItem("Edit/Play-Unplay, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        _ = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(
                    $"Assets/Scenes/{GameManager.SceneToLoad}.unity");
        EditorApplication.isPlaying = true;
    }
}