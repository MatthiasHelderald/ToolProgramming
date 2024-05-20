using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MeshList", menuName = "MeshManagement/new Mesh List", order = 0)]
    public class MeshList: ScriptableObject
    {
        public List<string> MeshName = new List<string>();
        public List<GameObject> PrefabList = new List<GameObject>();
        public List<Mesh> PrefabMeshList = new List<Mesh>();
        public void UpdateMesh(string meshName,Mesh mesh,Transform transform,string path)
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
                Updatevalues(meshName,mesh,transform,path);
        }
        public void Updatevalues(string meshName,Mesh mesh,Transform transform,string path)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
            if (!PrefabList.Contains(prefab) && prefab != null)
            {
                PrefabList.Add(prefab);
                PrefabMeshList.Add(prefab.transform.Find("_replace_").GetComponent<MeshFilter>().sharedMesh);
            }

            MeshData meshData = AssetDatabase.LoadAssetAtPath<MeshData>("Assets/Data/MeshData/"+ meshName+".asset");
            meshData.displayName = meshName;
            meshData.model = mesh;
            meshData.MeshAxis = Quaternion.Inverse(transform.rotation).eulerAngles.x;
            meshData.meshObject = transform.gameObject;
            meshData.MeshPrefabState = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/" + meshName+".prefab");
            meshData.prefabCount = CountOccurenceOfValue(PrefabMeshList,mesh);

            foreach (var uh in PrefabList)
            {
                if (uh.name.Contains(meshName)) Debug.Log(uh);
            }
            
            transform.GetComponent<MeshRenderer>().material = meshData.MeshMaterial;
            transform.position = Vector3.zero;
        }
        
        static int CountOccurenceOfValue(List<Mesh> list, Mesh valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }

        // string[] files = Directory.GetFiles("Assets/Prefab/", "*.prefab", SearchOption.TopDirectoryOnly);
        // foreach (var file in files)
        // {
        //     GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(file);
        //     if (PrefabList.Add(prefab))
        //     {
        //         
        //     }
        // }

        //var test = Init();
        /*public static MeshData Init()
        {
            // var data = ScriptableObject.CreateInstance<MeshData>();
            // MeshData data = ScriptableObject.CreateInstance(name) as MeshData;
            // data.Init(name);
            // return data;
            
            var data = ScriptableObject.CreateInstance<MeshData>();
            data.displayName = transform.name;
            data.model = transform.GetComponent<MeshFilter>().sharedMesh;
            data.MeshAxis = Quaternion.Inverse(transform.transform.rotation).eulerAngles.x;
            return data;
        }*/
        
    }
}