using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using Data;
using ScriptableObjects;
using Object = UnityEngine.Object;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class SceneManager : EditorWindow
    {
        public bool playState;
        private PlayModeStateChange playModeStateChange;
        [MenuItem("Tools/SceneManager")]
        private static void ShowWindow()
        {
            var window = GetWindow<SceneManager>();
            window.titleContent = new GUIContent("SceneManager");
            window.Show();
        }
        private void OnGUI()
        {
            LoadAllAssetsOfType(out SceneData[] sceneData);
            SerializedObject SerializedSceneData = new SerializedObject(sceneData[0]);

            foreach (var sceneGUID in AssetDatabase.FindAssets("t:Scene"))
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                
                sceneData[0].UpdateAllScenesList(sceneGUID);

                EditorGUILayout.LabelField(sceneName);
                
                var ScenesList = SerializedSceneData.FindProperty("sceneLoaderDatas");
                
                
                //if(GUILayout.Toggle(.Persistence,"Persistent"))
                    
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
                
            }
            
        }
        private void LoadAllAssetsOfType<T>(out T[] assets) where T : Object
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
        }

    }
}