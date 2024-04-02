using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using ScriptableObjects;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class SceneManager : EditorWindow
    {
        public bool playState;
        private PlayModeStateChange playModeStateChange;
        [MenuItem("MENUITEM/MENUITEMCOMMAND")]
        private static void ShowWindow()
        {
            var window = GetWindow<SceneManager>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }
        
        private void OnGUI()
        {
            SerializedProperty names;

            foreach (var sceneGUID in AssetDatabase.FindAssets("t:Scene"))
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                EditorGUILayout.LabelField(sceneName);

                if (GUILayout.Button("Open"))
                    if (playState)
                        UnityEngine.SceneManagement.SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
                    else
                        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                if (GUILayout.Button("Close"))
                    if (playState)
                        UnityEngine.SceneManagement.SceneManager.UnloadScene(
                            UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName));
                    else
                        EditorSceneManager.CloseScene(
                            UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName), true);

                //persistence = GUILayout.Toggle(persistence, "Load?");
            }
        }
    }
}