using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Waypoint[] allWaypoints = GetComponentsInChildren<Waypoint>();

        Gizmos.color = Color.red;
        for (int i = 0; i < allWaypoints.Length; i++)
        {
            if (allWaypoints[i] != null)
            {
                for (int j = 0; j < allWaypoints[i].GetAllWaypoints().Length; j++)
                {
                    if (allWaypoints[i].GetAllWaypoints()[j] != null)
                    {
                        //Gizmos.DrawLine(allWaypoints[i].GetPosition(), allWaypoints[i].GetAllWaypoints()[j].GetPosition());
                        DrawArrow(allWaypoints[i].GetPosition(), allWaypoints[i].GetAllWaypoints()[j].GetPosition(), 2f);
                    }
                }
            }
        }
    }
    
    private void DrawArrow(Vector3 from, Vector3 to, float arrowHeadSize = 0.25f)
    {
        Vector3 direction = to - from;
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 45, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 45, 0) * Vector3.forward;

        Gizmos.DrawRay(to, right * arrowHeadSize);
        Gizmos.DrawRay(to, left * arrowHeadSize);
    }
}
