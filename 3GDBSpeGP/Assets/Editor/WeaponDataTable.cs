using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class WeaponDataTable : EditorWindow
    {
        [MenuItem("Tools/WeaponDataTables")]
        private static void ShowWindow()
        {
            var window = GetWindow<WeaponDataTable>();
            window.titleContent = new GUIContent("WeaponDataTable");
            window.Show();
        }

        private void OnGUI()
        {
            GUI.backgroundColor = new Color(0.3f, 0.1f, 0f);
            GUILayout.Box("HealthManager", GUILayout.ExpandWidth(true), GUILayout.Height(30));

            EditorGUILayout.BeginHorizontal();
            HealthSystem[] healthSystems = FindObjectsOfType<HealthSystem>();
            if (healthSystems.Length > 0)
            {
                SerializedObject so = null;
                foreach (var healthSystem in healthSystems)
                {
                    so = new SerializedObject(healthSystem);
                    so.Update();

                    EditorGUILayout.BeginVertical();
                    GUILayout.Label(healthSystem.name);
                    //EditorGUILayout.LabelField("Name :" + healthSystem.name);

                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(so.FindProperty("currenthealth"));
                    GUI.enabled = true;

                    EditorGUILayout.PropertyField(so.FindProperty("maxHealth"));

                    EditorGUILayout.Separator();

                    if (GUILayout.Button("Refill Health"))
                    {
                        Undo.RecordObject(healthSystem, "Refill health");
                        healthSystem.RefillHealth();
                    }

                    if (GUILayout.Button("TakeDamage"))
                    {
                        Undo.RecordObject(healthSystem, "TakeDamage");
                        so.FindProperty("currenthealth").intValue -= 10;
                    }

                    EditorGUILayout.EndVertical();
                    so.ApplyModifiedProperties();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        
        private void LoadAllAssetsOfType<T>(out T[] assets) where T : Object
        {
            string[] guids = AssetDatabase.FindAssets("t:"+typeof(T));
            assets = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
        }
    }
}