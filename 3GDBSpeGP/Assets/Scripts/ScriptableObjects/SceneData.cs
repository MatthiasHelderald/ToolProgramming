using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "sceneData", menuName = "Scene/new Scene Data", order = 0)]
    public class SceneData : ScriptableObject
    {
        public bool persistence;
    }
}