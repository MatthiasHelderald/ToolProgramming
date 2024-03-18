using Editor;
using UnityEngine;
using UnityEditor;

public class PopupContainer : EditorWindow
{
    Rect button_yesRect;
    Rect button_noRect;

    private WeaponDataTable _weaponDataTable;
    public Vector2 GetWindowSize()
    {
        return new Vector2(50, 50);
    }
    void OnGUI()
    {
        //GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("YES", GUILayout.Width(100)))
            {
                _weaponDataTable = GetWindow<WeaponDataTable>();
                var path = AssetDatabase.GetAssetPath(_weaponDataTable.weapon);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();
                Debug.Log("Deleted");
                Close();
            }
            if (Event.current.type == EventType.Repaint) button_yesRect = GUILayoutUtility.GetLastRect();
        }
        
        //GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        {
            if (GUILayout.Button("NO", GUILayout.Width(100)))
            {
                Close();
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