using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PrefabData", menuName = "MeshManagement/new Prefab Data", order = 0)]
    public class PrefabData : ScriptableObject
    {
        public string displayName;
        public PrefabType prefabType;
        public Mesh model;
        public Texture texture;
    }
}
public enum PrefabType
{
    PropInteractable,
    PropEnviro,
}