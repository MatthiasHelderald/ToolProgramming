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
        private Transform currentTransform;
        Vector2 scrollPos;
        
        [UnityEditor.MenuItem("Tools/MeshManager")]
        private static void ShowWindow()
        {
            var window = GetWindow<MeshManagerWindow>();
            window.titleContent = new UnityEngine.GUIContent("MeshManager");
            window.Show();
        }

        public string[] Strings = 
        {"displayName", "model","MeshAxis"};
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
                currentTransform = gameObject.transform;
            }
            
            foreach (var mesh in meshes)
            {
                meshLists[0].UpdateMesh(mesh.name,mesh,currentTransform);
            }

            EditorGUILayout.BeginHorizontal();
            
            foreach (var meshdata in meshDatas)
            {
                EditorGUILayout.BeginVertical();
                
                EditorGUILayout.LabelField(meshdata.displayName);
                so = new SerializedObject(meshdata);
                so.Update();
                    
                foreach (var property in Strings)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(so.FindProperty(property), GUIContent.none, GUILayout.Width(75));
                    so.ApplyModifiedProperties();
                    EditorGUILayout.EndHorizontal();
                } 
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndHorizontal();
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