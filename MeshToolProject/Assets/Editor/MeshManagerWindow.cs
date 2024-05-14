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
        [UnityEditor.MenuItem("Tools/MeshManager")]
        private static void ShowWindow()
        {
            var window = GetWindow<MeshManagerWindow>();
            window.titleContent = new UnityEngine.GUIContent("MeshManager");
            window.Show();
        }

        public string[] Strings =
            {"MeshObject"};  
        //{"displayName", "prefabType", "model", "texture"};
        private void OnGUI()
        {
            LoadAllAssetsOfType(out MeshList[] meshLists);
            if (meshLists == null || meshLists.Length == 0)
                return;
            meshList = meshLists[0];
            
            LoadAllAssetsOfType(out PrefabData[] prefabDatas);
            if (prefabDatas == null || prefabDatas.Length == 0)
                return;
            prefabData = prefabDatas[0];
            
            Mesh[] meshes = Resources.LoadAll<Mesh>("Mesh");
            SerializedObject so = null;

            foreach (var mesh in meshes)
            {
                EditorGUILayout.LabelField(mesh.name);
                meshLists[0].UpdateMesh(mesh.name);
                
                /*foreach (var property in Strings)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    so = new SerializedObject(meshList);
                    so.Update();
                    
                    EditorGUILayout.PropertyField(so.FindProperty(property), GUIContent.none, GUILayout.Width(75));
                    so.ApplyModifiedProperties();


                    EditorGUILayout.EndHorizontal();
                }*/
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

        private void DuplicateMeshData(MeshData meshData)
        {
            var duplicatedmeshData = Instantiate(meshData);
            AssetDatabase.CreateAsset(duplicatedmeshData,AssetDatabase.GenerateUniqueAssetPath("Assets/Data/MeshData/" + meshData.name + "_Copy.asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Duplicated " + meshData.name);
        }
    }
}