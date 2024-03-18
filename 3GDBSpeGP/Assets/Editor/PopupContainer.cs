using UnityEngine;
using UnityEditor;

public class PopupContainer : EditorWindow
{
    Rect button_yesRect;
    Rect button_noRect;
    
    public Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }
    void OnGUI()
    {
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("button_yesRect", GUILayout.Width(100)))
            {
                //windowYes = true;
            }
            if (Event.current.type == EventType.Repaint) button_yesRect = GUILayoutUtility.GetLastRect();
        }
        
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("button_yesRect", GUILayout.Width(100)))
            {
                //windowYes = false;
            }
            if (Event.current.type == EventType.Repaint) button_noRect = GUILayoutUtility.GetLastRect();
        }
    }

    public void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
    
}