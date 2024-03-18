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
            GUILayout.Box("WeaponDataTable", GUILayout.ExpandWidth(true), GUILayout.Height(30));
            
            LoadAllAssetsOfType(out WeaponData[] weapons);

            if (weapons.Length > 0)
            {
                SerializedObject so = null;
                foreach (var weapon in weapons)
                {
                    so = new SerializedObject(weapon);
                    so.Update();

                    EditorGUILayout.BeginHorizontal();

                    GUILayout.Label(weapon.name);
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.PropertyField(so.FindProperty("FireMode"), GUIContent.none);
                    EditorGUILayout.PropertyField(so.FindProperty("ShootType"), GUIContent.none);

                    EditorGUILayout.PropertyField(so.FindProperty("model"), GUIContent.none);
                    EditorGUILayout.PropertyField(so.FindProperty("modelSprite"), GUIContent.none);

                    EditorGUILayout.PropertyField(so.FindProperty("damage"), GUIContent.none);
                    EditorGUILayout.PropertyField(so.FindProperty("accuracy"), GUIContent.none);
                    EditorGUILayout.PropertyField(so.FindProperty("recoilForce"), GUIContent.none);
                    EditorGUILayout.PropertyField(so.FindProperty("maxAmmo"), GUIContent.none);

                    if (GUILayout.Button("Create Weapon")) DuplicateWeapon(weapon);

                    if (GUILayout.Button("Delete Weapon")) DeleteWeapon(weapon);

                    so.ApplyModifiedProperties();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        private void DuplicateWeapon(WeaponData weapon)
        {
            // Duplicate the weapon
            var duplicatedWeapon = Instantiate(weapon);
            AssetDatabase.CreateAsset(duplicatedWeapon,
                AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Weapons/" + weapon.name + "_Copy.asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Duplicated " + weapon.name);
        }

        private void DeleteWeapon(WeaponData weapon)
        {
            CreateWindow<PopupContainer>();
            var path = AssetDatabase.GetAssetPath(weapon);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.Refresh();
            Debug.Log("Deleted");
        }

        private void LoadAllAssetsOfType<T>(out T[] assets) where T : Object
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
        }
    }
}