using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private Waypoint target;
    [SerializeField] private float angleToSteer = 10;
    [SerializeField] private float distanceToStop = 1;
    [SerializeField] private float distance = 1;

    [SerializeField] private Vector2 inputs;

    private void Awake()
    {
        CarController controller = GetComponent<CarController>();
        controller.SetMoveFunc(() => inputs.y, () => inputs.x);
    }

    private void Update()
    {
        if(target == null) return;

        Vector3 position = transform.position;
        Vector3 targetPosition = target.GetPosition();
        targetPosition.y = position.y;

        Vector3 delta = targetPosition - position;
        float dot = Vector3.Dot(delta, transform.right);
        float angle = Vector3.Angle(delta, transform.forward);
        if(angle < angleToSteer)
        {
            inputs.x = 0;
        }
        else
        {
            inputs.x = dot < 0 ? -1 : 1;
        }

        distance = Vector3.Distance(targetPosition, position);
        if(distance >= distanceToStop)
        {
            inputs.y = 1;
        }
        else
        {
            inputs.y = 0;
            target = target.GetPossibleWaypoint();
        }
    }
}
