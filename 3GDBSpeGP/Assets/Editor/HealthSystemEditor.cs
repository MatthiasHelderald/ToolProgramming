using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomEditor(typeof(HealthSystem))]
    public class HealthSystemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //Assure que l'inspecteur ait les bonnes valeurs
            serializedObject.Update();

            //Recuperer l'instance de l'objet pour lequel on crée le custom Ediotr
            HealthSystem instance = (HealthSystem) target; 

            //Display Prop
            SerializedProperty currentHealthProp = serializedObject.FindProperty("currenthealth");
            GUI.enabled = false;
            EditorGUILayout.PropertyField(currentHealthProp);
            GUI.enabled = true;
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));

            EditorGUILayout.Separator();

            if (GUILayout.Button("Refill Health"))
            {
                Undo.RecordObject(instance,"Refill health");
                instance.RefillHealth();
            }

            if (GUILayout.Button("TakeDamage"))
            {
                currentHealthProp.intValue -= 10;
            }

            //Pour afficher des valeur cote à coteEditorGUILayout.BeginHorizontal(); EditorGUILayour.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            //DrawDefaultInspector(); affiche l'inspecteur normal
        }
    }
}