using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PatrolPath)),CanEditMultipleObjects]
    public class PatrolPathEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            PatrolPath t = target as PatrolPath;

            Vector3[] waypointsCopy = t.waypoints.ToArray();
            
            Vector3 lastPos = t.waypoints[^1];
            Vector3 tPos = t.transform.position;
            Handles.color = new Color(0.55f, 1f, 0.44f);
            Handles.DrawDottedLine(tPos + t.waypoints[0],tPos + lastPos,10f);
            
            for (var i = 0; i < t.Waypoints.Length; i++)
            {
                EditorGUI.BeginChangeCheck();
                waypointsCopy[i] = Handles.PositionHandle(tPos + t.Waypoints[i], Quaternion.identity) - tPos;
                Handles.DrawDottedLine(tPos + waypointsCopy[i], tPos+lastPos, screenSpaceSize:10f);
                lastPos = waypointsCopy[i];
                var waypoint = tPos+t.Waypoints[i];
                Handles.DrawWireDisc(waypoint, Vector3.up, 0.2f, 0.5f);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(t,"Waypoint Changed");
                    t.Waypoints = waypointsCopy;
                }
            }
        }

        //Permet d'implenter ondrawgizmo ds editor
        //Todo draw dotted line between waypoints
        [DrawGizmo((GizmoType.Selected | GizmoType.NonSelected))]
        private static void DrawGizmo(PatrolPath t,GizmoType gizmoType)
        {
            Vector3 lastPos = t.waypoints[^1];
            Handles.color = new Color(0.55f, 1f, 0.44f);
            Vector3 tPos = t.transform.position;
            Handles.DrawDottedLine(tPos +t. waypoints[0],tPos + lastPos,10f);
            
            for (var i = 0; i < t.waypoints.Length; i++)
            {
                Vector3 wPos = tPos + t.waypoints[i];
                Handles.DrawWireDisc(wPos,Vector3.up ,0.5f);
                Handles.PositionHandle(wPos, Quaternion.identity);
                lastPos = t.waypoints[i];
            }
        }

    }
}