using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MeshData", menuName = "MeshManagement/new Mesh Data", order = 0)]
    public class MeshList: ScriptableObject
    {
        public List<string> MeshName = new List<string>();
        // public List<bool> MeshPrefabState = new List<bool>();
        // public List<float> MeshAxis = new List<float>();
        // public List<Mesh> MeshObject = new List<Mesh>();
        // public List<Material> MeshMaterial = new List<Material>();
        
        public void UpdateMesh(string meshName,Mesh mesh,Transform transform)
        {
            if (!MeshName.Contains(meshName))
            {
                MeshName.Add(meshName);
                var data = ScriptableObject.CreateInstance<MeshData>();
                data.displayName = meshName;
                data.model = mesh;
                data.MeshAxis = transform.rotation.x;
                data.MeshPrefabState = false;
                UnityEditor.AssetDatabase.CreateAsset(data, AssetDatabase.GenerateUniqueAssetPath("Assets/Data/MeshData/"+ meshName+".asset"));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("Duplicated " + data.name);
            }
            else
                Updatevalues(meshName,mesh,transform);
        }
        public void Updatevalues(string meshName,Mesh mesh,Transform transform)
        {
            //var idx = this.MeshName.FindIndex(s => s == meshName );
            //MeshName[idx] = meshName;
            
            //var data = ScriptableObject.CreateInstance<MeshData>();
            //data.displayName = meshName;
            //data.model = mesh;
            //data.MeshAxis = transform.rotation.eulerAngles.x;
            //data.MeshPrefabState = false;

            MeshData meshData = AssetDatabase.LoadAssetAtPath<MeshData>("Assets/Data/MeshData/"+ meshName+".asset");
            meshData.displayName = meshName;
            meshData.model = mesh;
            meshData.MeshAxis = Quaternion.Inverse(transform.rotation).eulerAngles.x;
        }
        
        public static MeshData CreateInstance(string name)
        {
            // var data = ScriptableObject.CreateInstance<MeshData>();
            // Debug.Log("uh");
            // MeshData data = ScriptableObject.CreateInstance(name) as MeshData;
            // data.Init(name);
            // return data;
            
            var data = ScriptableObject.CreateInstance<MeshData>();
            data.displayName = name;
            return data;
        }
        
    }
}