using UnityEngine;
using UnityEditor;

public class PopupExample : PopupWindowContent
{
    Rect button_yesRect;
    Rect button_noRect;

    public override Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("button_yesRect", GUILayout.Width(100)))
            {
                PopupWindow.Show(button_yesRect, new PopupExample());
            }
            if (Event.current.type == EventType.Repaint) button_yesRect = GUILayoutUtility.GetLastRect();
        }
        
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("button_yesRect", GUILayout.Width(100)))
            {
                PopupWindow.Show(button_noRect, new PopupExample());
            }
            if (Event.current.type == EventType.Repaint) button_noRect = GUILayoutUtility.GetLastRect();
        }
    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
}