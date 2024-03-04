using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

    [CustomPropertyDrawer(typeof(InventorySlot))]
    public class InventorySlotDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            var wdProp = new PropertyField(property.FindPropertyRelative("data"));
            root.Add(wdProp);

            var currentAmmoProp = new PropertyField(property.FindPropertyRelative("currentAmmoInclip"));
            root.Add(currentAmmoProp);
            
            var currentProp = new PropertyField(property.FindPropertyRelative("ammo"));
            root.Add(currentProp);

            return root;
        }
    }