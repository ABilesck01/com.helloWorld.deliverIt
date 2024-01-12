using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private Transform t;

    private void Awake()
    {
        t = transform;
    }

    private void LateUpdate()
    {
        t.position = target.position + offset;
    }
}
