using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class HealthManager : EditorWindow
    {
        [MenuItem("MENUITEM/MENUITEMCOMMAND")]
        private static void ShowWindow()
        {
            var window = GetWindow<HealthManager>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}