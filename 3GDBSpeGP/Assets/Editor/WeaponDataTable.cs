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
            
            LoadAllAssetsOfType<WeaponData>(out WeaponData[] weapons);
            PickableWeapon[] weaponDatas = FindObjectsOfType<PickableWeapon>();
            if (weaponDatas.Length > 0)
            {
                SerializedObject so = null;
                foreach (var weapon in weapons)
                {
                    so = new SerializedObject(weapon);
                    so.Update();
                    
                    EditorGUILayout.BeginVertical();
                    GUILayout.Label(weapon.name);

                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(so.FindProperty("FireMode"));
                    GUI.enabled = true;

                    EditorGUILayout.PropertyField(so.FindProperty("ShootType"));

                    EditorGUILayout.Separator();

                    if (GUILayout.Button("Create Weapon"))
                    {
                        
                    }

                    if (GUILayout.Button("Delete Weapon"))
                    {
                        
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