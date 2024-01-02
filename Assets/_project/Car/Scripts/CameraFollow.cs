using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;

    private Transform t;

    private void Awake()
    {
        t = transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothPosition = Vector3.Lerp(t.position, desiredPosition, smooth * Time.deltaTime);
        t.position = smoothPosition;
    }
}
