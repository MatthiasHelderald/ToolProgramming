using ScriptableObjects;
using UnityEngine;

public class MeshContructor : MonoBehaviour
{
    public MeshData meshData;
    public Mesh mesh;
    public float axis;
    public bool prefabState;
    public Material material;

    void Start()
    {
        meshData = MeshData.CreateInstance(meshData.name);
        meshData.UpdateMesh(meshData.name,mesh);
    }
}

