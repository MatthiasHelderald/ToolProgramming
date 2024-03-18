using System;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

        private PopupContainer _popupContainer;
        public WeaponData weapon;
        private bool even;

        public string[] Strings =
        {
            "displayName", "FireMode", "ShootType", "model", "modelSprite", "damage", "accuracy", "recoilForce",
            "maxAmmo",
        };

        private void OnGUI()
        {
            GUI.backgroundColor = new Color(0.3f, 0.1f, 0f);
            GUILayout.Box("WeaponDataTable", GUILayout.ExpandWidth(true), GUILayout.Height(30));

            LoadAllAssetsOfType(out WeaponData[] weapons);

            if (weapons.Length > 0)
            {
                SerializedObject so = null;

                EditorGUILayout.BeginHorizontal();

                foreach (var s in Strings)
                {
                    //GUIStyle style = new GUIStyle();
                    //style.alignment = TextAnchor.UpperLeft;
                    //style.fixedWidth = 200;
                    EditorGUILayout.SelectableLabel(s,GUILayout.Width(75));
                    this.Repaint();
                }

                EditorGUILayout.EndHorizontal();

                foreach (var weapon in weapons)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    so = new SerializedObject(weapon);
                    so.Update();
                    
                    even = !even;
                    GUI.backgroundColor = even ? new Color(0.39f, 0.46f, 0.5f) 
                        : new Color(1f, 0.51f, 0.47f);

                    foreach (var s in Strings)
                    {
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.PropertyField(so.FindProperty(s), GUIContent.none,GUILayout.Width(75));
                        so.ApplyModifiedProperties();
                        EditorGUILayout.EndVertical();
                    }

                    if (GUILayout.Button("Duplicate Weapon")) DuplicateWeapon(weapon);
                    if (GUILayout.Button("Delete Weapon")) DeleteWeapon(weapon);

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

        public void DeleteWeapon(WeaponData weapon)
        {
            _popupContainer = CreateWindow<PopupContainer>();
            this.weapon = weapon;
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