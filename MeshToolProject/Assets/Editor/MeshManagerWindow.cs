using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class MeshManagerWindow : UnityEditor.EditorWindow
    {
        private MeshList meshList;
        private MeshData meshData;
        private PrefabData prefabData;
        private int i;
        Vector2 scrollPos;
        
        [UnityEditor.MenuItem("Tools/MeshManager")]
        private static void ShowWindow()
        {
            var window = GetWindow<MeshManagerWindow>();
            window.titleContent = new UnityEngine.GUIContent("MeshManager");
            window.Show();
        }

        public string[] Strings = 
        {"MeshMaterial","prefabTemplate","prefabCount"};
        private void OnGUI()
        {
            LoadAllAssetsOfType(out MeshList[] meshLists);
            if (meshLists == null || meshLists.Length == 0)
                return;
            meshList = meshLists[0];
            
            LoadAllAssetsOfType(out MeshData[] meshDatas);
            if (meshDatas == null || meshLists.Length == 0)
                return;

            LoadAllAssetsOfType(out PrefabData[] prefabDatas);
            if (prefabDatas == null || prefabDatas.Length == 0)
                return;

            Mesh[] meshes = Resources.LoadAll<Mesh>("Mesh");
            GameObject[] meshObjects = Resources.LoadAll<GameObject>("Mesh");
            SerializedObject so = null;

            foreach (var gameObject in meshObjects)
            {
                meshLists[0].UpdateMesh(gameObject.GetComponent<MeshFilter>().name,gameObject.GetComponent<MeshFilter>().sharedMesh,gameObject.transform);
                //if (GUILayout.Button("Prefab")) PrefabThis(meshdata.prefabTemplate,gameObject);
            }

            EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.BeginHorizontal();
            
            foreach (var meshdata in meshDatas)
            {
                EditorGUILayout.BeginVertical();
                
                EditorGUILayout.LabelField(meshdata.displayName);
                GUILayout.Box( AssetPreview.GetAssetPreview(meshdata.model));
                
                var buttonColor = GUI.backgroundColor;
                if (meshdata.MeshPrefabState)
                {
                    GUI.backgroundColor = Color.yellow;
                }

                else GUI.backgroundColor = Color.cyan;
                
                if (GUILayout.Button("Prefab"))
                {
                    PrefabThis(meshdata.prefabTemplate, meshdata.meshObject);
                    meshdata.MeshPrefabState = true;
                }
                
                GUI.backgroundColor = buttonColor;

                so = new SerializedObject(meshdata);
                so.Update();
                    
                foreach (var property in Strings)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(so.FindProperty(property), GUIContent.none, GUILayout.Width(135));
                    so.ApplyModifiedProperties();
                    EditorGUILayout.EndHorizontal();
                }

                var originalColor = GUI.backgroundColor;
                if (meshdata.MeshAxis != 0) GUI.backgroundColor = Color.yellow;
                else GUI.backgroundColor = Color.cyan;
                EditorGUILayout.PropertyField(so.FindProperty("MeshAxis"), GUIContent.none, true,
                    GUILayout.Width(135));
                GUI.backgroundColor = originalColor;
                {
                    so.ApplyModifiedProperties();
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
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

        private void PrefabThis(GameObject template ,GameObject meshObject)
        {
            string localPath = "Assets/Prefab/" + meshObject.name + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            //MeshData meshData = AssetDatabase.LoadAssetAtPath<MeshData>("Assets/Data/MeshData/" + meshObject.GetComponent<MeshFilter>().sharedMesh + ".asset");
            
            Debug.Log(localPath);
            //Instantiate(meshObject);
            
             GameObject go = PrefabUtility.SaveAsPrefabAsset(template, localPath);
             go.name = meshObject.name;
             go.transform.Find("_replace_").GetComponent<MeshFilter>().sharedMesh = meshObject.GetComponent<MeshFilter>().sharedMesh;
             // foreach (var child in go.transform) 
             // {
             //     
             // }
        }
    }
}