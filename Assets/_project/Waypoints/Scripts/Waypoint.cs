using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint[] possibleWaypoints;

    private Transform t;

    public Waypoint[]  GetAllWaypoints() { return possibleWaypoints; }

    public Vector3 GetPosition() { return t.position; }

    private void Awake()
    {
        t = transform;
    }

    private void OnValidate()
    {
        t = transform;
    }

    public Waypoint GetPossibleWaypoint()
    {
        int rand = Random.Range(0, possibleWaypoints.Length);
        return possibleWaypoints[rand];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < possibleWaypoints.Length; i++)
        {
            if (possibleWaypoints[i] != null)
            {
                Gizmos.DrawLine(GetPosition(), possibleWaypoints[i].GetPosition());
                DrawArrow(GetPosition(), possibleWaypoints[i].GetPosition(), 2f);
            }
        }
    }

    private void DrawArrow(Vector3 from, Vector3 to, float arrowHeadSize = 0.25f)
    {
        Gizmos.DrawLine(from, to);

        Vector3 direction = to - from;
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 45, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 45, 0) * Vector3.forward;

        Gizmos.DrawRay(to, right * arrowHeadSize);
        Gizmos.DrawRay(to, left * arrowHeadSize);
    }
}
