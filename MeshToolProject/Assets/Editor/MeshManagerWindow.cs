using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class MeshManagerWindow : UnityEditor.EditorWindow
    {
        private MeshData meshData;
        [UnityEditor.MenuItem("Tools/MeshManager")]
        private static void ShowWindow()
        {
            var window = GetWindow<MeshManagerWindow>();
            window.titleContent = new UnityEngine.GUIContent("MeshManager");
            window.Show();
        }

        private void OnGUI()
        {
            LoadAllAssetsOfType(out MeshData[] meshDatas);
            if (meshDatas == null || meshDatas.Length == 0)
                return;
            this.meshData = meshDatas[0];
            
            Mesh[] meshes =  = AssetDatabase.FindAssets("co l:architecture t:texture2D", new[] {"Assets/MyAwesomeProps"});

            foreach (var meshID in meshes)
            {
                EditorGUILayout.LabelField(meshID.name);
                //meshDatas[0].UpdateMesh(meshObject.name);
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