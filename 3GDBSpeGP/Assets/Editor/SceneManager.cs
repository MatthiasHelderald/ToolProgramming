using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

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
            foreach (var sceneGUID in AssetDatabase.FindAssets("t:Scene"))
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                EditorGUILayout.LabelField(sceneName);
    
                // Add buttons or functionality here for managing scenes (e.g., open, close, load)
                if (GUILayout.Button("Open"))
                {
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                    //see EditorSceneManager documentation for further functionality 
                }
            }
        }
    }
}