// For Unity versions newer than 2017.1

using Editor;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DetectPlayModeChanges {

    static DetectPlayModeChanges() {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state) {
        switch (state) {
            case PlayModeStateChange.ExitingEditMode:
                Debug.Log(EditorWindow.GetWindow<SceneManager>().playState);
                EditorWindow.GetWindow<SceneManager>().playState = true;
                break;
            case PlayModeStateChange.EnteredPlayMode:
                EditorWindow.GetWindow<SceneManager>().UnloadScenesAtPlaymode();
                Debug.Log(EditorWindow.GetWindow<SceneManager>().playState);
                EditorWindow.GetWindow<SceneManager>().playState = true;
                break;
            case PlayModeStateChange.ExitingPlayMode:
                Debug.Log(EditorWindow.GetWindow<SceneManager>().playState);
                EditorWindow.GetWindow<SceneManager>().playState = false;
                break;
            case PlayModeStateChange.EnteredEditMode:
                EditorWindow.GetWindow<SceneManager>().UnloadScenesAtPlaymode();
                Debug.Log(EditorWindow.GetWindow<SceneManager>().playState);
                EditorWindow.GetWindow<SceneManager>().playState = false;
                break;
        }
    }
}
