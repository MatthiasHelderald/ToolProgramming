using ScriptableObjects;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

    [CustomPropertyDrawer(typeof(WeaponDataWrapper))]
    public class WeaponDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            var wpProp = new PropertyField(property.FindPropertyRelative("weaponData"));
            root.Add(wpProp);
            
            var dnProp = new PropertyField(property.FindPropertyRelative("displayName"));
            root.Add(dnProp);

            return root;
        }
    }