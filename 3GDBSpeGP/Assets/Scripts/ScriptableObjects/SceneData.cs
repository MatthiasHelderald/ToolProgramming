using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "sceneData", menuName = "Scene/new Scene Data", order = 0)]
    public class SceneData : ScriptableObject
    {
        public List<SceneLoaderData> sceneLoaderDatas = new List<SceneLoaderData>();
        public void UpdateAllScenesList(string sceneGUID)
        {
            if (!sceneLoaderDatas.Any(s => s.SceneGUID == sceneGUID))
                sceneLoaderDatas.Add(new SceneLoaderData(false, sceneGUID));
            if (sceneLoaderDatas.Any(s => s.Persistence));
        }
    }
}