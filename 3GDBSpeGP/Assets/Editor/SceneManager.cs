using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Object = UnityEngine.Object;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class SceneManager : EditorWindow
    {
        public bool playState;
        private SceneData sceneData;
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
            if (sceneData == null || sceneData.Length == 0)
                return;
            this.sceneData = sceneData[0];

            foreach (var sceneGUID in AssetDatabase.FindAssets("t:Scene"))
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
                var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                bool sceneBool = sceneData[0].GetPersistence(sceneGUID);
                
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

                if (GUILayout.Button("Persistent"))
                {
                    sceneBool = !sceneBool; 
                    //Debug.Log(sceneBool);
                    foreach( var x in sceneData[0].SceneBool) {
                        //Debug.Log( x.ToString());
                    }
                }
                
                sceneData[0].UpdateScene(sceneGUID,sceneBool);
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
        public void UnloadScenesAtPlaymode()
        {
            if(sceneData == null)
                return;
            var sceneGUIDs = AssetDatabase.FindAssets("t:Scene");
            foreach (var sceneGUID in sceneGUIDs)
            {
                var scenename = GetSceneName(sceneGUID);
                if(!sceneData.GetPersistence(sceneGUID))
                {
                    try
                    {
                        UnityEngine.SceneManagement.SceneManager.UnloadScene(
                                                UnityEngine.SceneManagement.SceneManager.GetSceneByName(scenename));
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                //else
                    //UnityEngine.SceneManagement.SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
            }
        }
        public void LoadScenesAtPlaymode()
        {
            if(sceneData == null)
                return;
            var sceneGUIDs = AssetDatabase.FindAssets("t:Scene");
            foreach (var sceneGUID in sceneGUIDs)
            {
                var scenename = GetSceneName(sceneGUID);
                if(sceneData.GetPersistence(sceneGUID) && !UnityEngine.SceneManagement.SceneManager.GetSceneByName(scenename).isLoaded)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
                }
            }
        }

        public void LoadScenesAtEditmode()
        {
            
        }
        public string GetSceneName(string sceneGUID)
        {
            var scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
            return System.IO.Path.GetFileNameWithoutExtension(scenePath);
        }
    }
}