using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class HealthManager : EditorWindow
    {
        [MenuItem("Tools/HealthManager/Open HealthManager Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<HealthManager>();
            window.titleContent = new GUIContent("Health Manager");
            window.Show();
        }

        // 4 colognne avec le sparamètres de vie
        private void OnGUI()
        {
            GUI.backgroundColor = new Color(0.3f,0.1f,0f);
            GUILayout.Box("HealthManager",GUILayout.ExpandWidth(true),GUILayout.Height(30));

            HealthSystem[] healthSystems = FindObjectsOfType<HealthSystem>();
            if (healthSystems == null)
            {
                EditorGUILayout.LabelField("No HealthSystemFOund");
                return;
            }

            SerializedObject so = null;
            foreach (var healthSystem in healthSystems)
            {
                so = new SerializedObject(healthSystem);
                so.Update();
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(healthSystem.name);
                //EditorGUILayout.LabelField("Name :" + healthSystem.name);
                
                GUI.enabled = false;
                EditorGUILayout.PropertyField( so.FindProperty("currenthealth"));
                GUI.enabled = true;
            
                EditorGUILayout.PropertyField(so.FindProperty("maxHealth"));
                
                EditorGUILayout.Separator();

                if (GUILayout.Button("Refill Health"))
                {
                    Undo.RecordObject(healthSystem,"Refill health");
                    healthSystem.RefillHealth();
                }
                
                if (GUILayout.Button("TakeDamage"))
                {
                    Undo.RecordObject(healthSystem,"TakeDamage");
                    so.FindProperty("currenthealth").intValue -= 10;
                }
                
                EditorGUILayout.EndHorizontal();
                so.ApplyModifiedProperties();
            }
        }

        [MenuItem("Tools/HealthManager/Refill Health &o")]
        public static void RefillHealth()
        {
            HealthSystem hs = FindObjectOfType<HealthSystem>();
            if (hs != null)
            {
                Undo.RecordObject(hs,"Refill health");
                hs.RefillHealth();
            }
            Debug.Log("Refill Health");
        }
    }
}