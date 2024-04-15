using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Center of mass", typeof(Rigidbody))]
[Icon("AvatarPivot")]
public class CenterOfMassTool : EditorTool
{
    public override void OnToolGUI(EditorWindow window)
    {
        foreach (var target in targets)
        {
            if (!(target is Rigidbody rb))
            {
                return;
            }
            else
            {
                rb.centerOfMass = Handles.PositionHandle(rb.centerOfMass, Quaternion.identity);
            }
        }
    }
}