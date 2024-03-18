using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PopupContainer : EditorWindow
    {
        private static void ShowWindow()
        {
            var window = GetWindow<PopupContainer>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }

        private void CreateGUI()
        {
            
        }
    }
}