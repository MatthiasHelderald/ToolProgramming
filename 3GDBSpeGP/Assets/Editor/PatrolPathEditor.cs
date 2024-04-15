using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PatrolPath))]
    public class PatrolPathEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            PatrolPath t = target as PatrolPath;
            Vector3 tPos = t.transform.position;
            for (var i = 0; i < t.Waypoints.Length; i++)
            {
                t.Waypoints[i] = Handles.PositionHandle(tPos + t.Waypoints[i], Quaternion.identity) - tPos;
            }
        }
    }
}