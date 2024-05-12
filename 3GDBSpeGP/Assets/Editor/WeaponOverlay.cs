using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

[Overlay(typeof(SceneView),id: ID_OVERLAY ,displayName: "WeaponManager")]
//[Icon()]
public class WeaponOverlay : Overlay
{
    private const string ID_OVERLAY = "WeaponOverlay";
    
    public override VisualElement CreatePanelContent()
    {
        var root = new VisualElement();
        var titleLabel = new Label(text: "Weapon Manager");
        root.Add(titleLabel);
        return root;
    }
}