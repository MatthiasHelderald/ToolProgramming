using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "sceneData", menuName = "Scene/new Scene Data", order = 0)]
    public class SceneData : ScriptableObject
    {
        public List<string> SceneGUI = new List<string>();
        public List<bool> SceneBool = new List<bool>();
        
        public void UpdateScene(string sceneGUI,bool sceneBool)
        {
            if (!SceneGUI.Contains(sceneGUI))
            {
                SceneGUI.Add(sceneGUI);
                SceneBool.Add(sceneBool);
            }
            else
                ReplaceBool(sceneGUI, sceneBool);
        }

        public void ReplaceBool(string sceneGUID,bool sceneBool)
        {
            var idx = SceneGUI.FindIndex(s => s == sceneGUID );
            SceneBool[idx] = sceneBool;
        }
        public bool GetPersistence(string sceneGUID)
        {
            try
            {
                var idx = SceneGUI.FindIndex(s => s == sceneGUID );
                return SceneBool[idx];
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}