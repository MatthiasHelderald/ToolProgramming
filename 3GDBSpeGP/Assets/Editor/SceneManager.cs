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
                
                if (GUILayout.Button("Open")) EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                if (GUILayout.Button("Close"))
                    EditorSceneManager.CloseScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName),
                        true);

                //persistence = GUILayout.Toggle(persistence, "Load?");
            }
        }
    }
}