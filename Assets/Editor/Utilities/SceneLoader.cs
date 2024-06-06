using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneLoader
{
    [MenuItem("Scenes/Main Menu")]
    public static void LoadScene1()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu_scene.unity");
    }

    [MenuItem("Scenes/Test Scene")]
    public static void LoadScene2()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/TestScene.unity");
    }
}
