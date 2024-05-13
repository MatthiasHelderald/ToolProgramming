using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MeshData", menuName = "MeshManagement/new Mesh Data", order = 0)]
    public class MeshData : ScriptableObject
    {
        public List<string> MeshName = new List<string>();
        public List<bool> MeshPrefabState = new List<bool>();
        public List<float> MeshAxis = new List<float>();
        public List<Mesh> MeshObject = new List<Mesh>();
        public List<Material> MeshMaterial = new List<Material>();
        
        public void UpdateMesh(string meshName,Mesh meshObject)
        {
            if (!MeshName.Contains(meshName))
            {
                MeshName.Add(meshName);
                MeshObject.Add(meshObject);
            }
            else
                ReplaceBool(meshName);
        }
        public void ReplaceBool(string meshName)
        {
            //var idx = this.MeshName.FindIndex(s => s == meshName );
            //MeshPrefabState[idx] = meshBool;
        }
    }
}