using System;
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
            
            Vector3 lastPos = t.waypoints[^1];
            Vector3 tPos = t.transform.position;
            Handles.color = new Color(0.55f, 1f, 0.44f);
            Handles.DrawDottedLine(tPos + t.waypoints[0],tPos + lastPos,10f);
            
            for (var i = 0; i < t.Waypoints.Length; i++)
            {
                t.Waypoints[i] = Handles.PositionHandle(tPos + t.Waypoints[i], Quaternion.identity) - tPos;
                
                var waypoint = tPos+t.Waypoints[i];
                Handles.DrawWireDisc(waypoint, Vector3.up, 0.2f, 0.5f);
                Handles.DrawDottedLine(waypoint, tPos+lastPos, screenSpaceSize:10f);
                lastPos = t.Waypoints[i];
            }
        }
    }
}