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
    [CreateAssetMenu(fileName = "MeshList", menuName = "MeshManagement/new Mesh List", order = 0)]
    public class MeshList: ScriptableObject
    {
        public List<string> MeshName = new List<string>();
        private GameObject uh;
        public void UpdateMesh(string meshName,Mesh mesh,Transform transform)
        {
            if (!MeshName.Contains(meshName))
            {
                MeshName.Add(meshName);
                var data = ScriptableObject.CreateInstance<MeshData>();
                data.displayName = meshName;
                data.model = mesh;
                data.MeshAxis = transform.rotation.x;
                data.meshObject = transform.gameObject;
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
            MeshData meshData = AssetDatabase.LoadAssetAtPath<MeshData>("Assets/Data/MeshData/"+ meshName+".asset");
            meshData.displayName = meshName;
            meshData.model = mesh;
            meshData.MeshAxis = Quaternion.Inverse(transform.rotation).eulerAngles.x;
            meshData.meshObject = transform.gameObject;
            transform.GetComponent<MeshRenderer>().material = meshData.MeshMaterial;
            transform.position = Vector3.zero;
        }
        
        /*public static MeshData Init(MeshData data)
        {
            // var data = ScriptableObject.CreateInstance<MeshData>();
            // MeshData data = ScriptableObject.CreateInstance(name) as MeshData;
            // data.Init(name);
            // return data;
            
            data = ScriptableObject.CreateInstance<MeshData>();
            data.displayName = transform.name;
            data.model = transform.GetComponent<MeshFilter>().sharedMesh;
            data.MeshAxis = Quaternion.Inverse(transform.transform.rotation).eulerAngles.x;
            return data;
        }*/
        
    }
}