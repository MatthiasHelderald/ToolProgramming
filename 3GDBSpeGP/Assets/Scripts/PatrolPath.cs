using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] public Vector3[] waypoints;
    public Vector3[] Waypoints => waypoints;

    
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length <= 1)
        {
            return;
        }
            
        Vector3 lastPos = waypoints[^1];
        Handles.color = new Color(0.55f, 1f, 0.44f);
        Vector3 tPos = transform.position;
        Handles.DrawDottedLine(tPos + waypoints[0],tPos + lastPos,10f);
            
        for (var i = 0; i < waypoints.Length; i++)
        {
            Vector3 wPos = tPos + waypoints[i];
            Handles.DrawWireDisc(wPos,Vector3.up ,0.5f);
            Handles.PositionHandle(wPos, Quaternion.identity);
            lastPos = waypoints[i];
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length <= 1)
            return;

        Vector3 tPos = transform.position;
        for (var i = 0; i < waypoints.Length; i++)
        {
            Vector3 wPos = tPos + waypoints[i];
            waypoints[i] = Handles.PositionHandle(wPos, Quaternion.identity) - tPos;
        }
    }
}
