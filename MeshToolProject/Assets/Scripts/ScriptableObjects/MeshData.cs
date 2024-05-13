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
        public List<Material> MeshMaterial = new List<Material>();
        
        public void UpdateMesh(string meshName)
        {
            if (!this.MeshName.Contains(meshName))
            {
                this.MeshName.Add(meshName);
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