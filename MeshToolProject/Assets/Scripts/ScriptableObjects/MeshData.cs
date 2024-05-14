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
        public string displayName;
        public float MeshAxis;
        public bool MeshPrefabState;
        public Material MeshMaterial;
        public Mesh model;
        public Texture texture;
        public GameObject prefabTemplate;
        public GameObject meshObject;

    }
}