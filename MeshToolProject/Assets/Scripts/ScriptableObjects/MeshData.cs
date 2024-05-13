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
    public class MeshData : ScriptableObject
    {
        public string displayName;
        public float MeshAxis;
        public bool MeshPrefabState;
        public Material MeshMaterial;
        public Mesh model;
        public Texture texture;
        
        public List<string> MeshName = new List<string>();
        // public List<bool> MeshPrefabState = new List<bool>();
        // public List<float> MeshAxis = new List<float>();
        // public List<Mesh> MeshObject = new List<Mesh>();
        // public List<Material> MeshMaterial = new List<Material>();
        
        public void UpdateMesh(string meshName,Mesh mesh)
        {
            if (!MeshName.Contains(meshName))
            {
                MeshName.Add(meshName);
                MeshData data = MeshData.CreateInstance("zaza");
            }
            else
                ReplaceBool(meshName);
        }
        public void ReplaceBool(string meshName)
        {
            //var idx = this.MeshName.FindIndex(s => s == meshName );
            //MeshPrefabState[idx] = meshBool;
        }
        public void Init(string name)
        {
            this.name = name;
            //this.model = model;
        }
        public static MeshData CreateInstance(string name)
        {
            var data = MeshData.CreateInstance<MeshData>();
            Debug.Log("uh");
            //MeshData data = ScriptableObject.CreateInstance(name) as MeshData;
            data.Init(name);
            return data;
        }
    }
}