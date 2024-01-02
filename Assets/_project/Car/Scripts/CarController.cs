using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float acceleration;
    [SerializeField] private float brakeForce;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float alignToGroundTime;
    [Header("Physics")]
    [SerializeField] private Rigidbody colliderRb;
    [SerializeField] private Rigidbody motorRigidbody;
    [SerializeField] private LayerMask groundLayer;
    [Header("Wheels")]
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform rearLeftWheel;
    [SerializeField] private Transform rearRightWheel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxWheelTurn = 35f;
    [Header("VFX")]
    [SerializeField] private TrailRenderer[] skidMarks;
    [SerializeField] private ParticleSystem[] driftSmoke;

    private Transform t;
    private Transform motorTransform;
    private Transform colliderTransform;

    private Vector2 input;
    private float thrust;

    private float driftFactor;
    private float currentSteerAngle;
    private float originalDrag;

    private bool isOnGround;

    public void GetMoveInputs(InputAction.CallbackContext callbackContext)
    {
        input = callbackContext.ReadValue<Vector2>();
    }

    private void Awake()
    {
        t = transform;
        motorTransform = motorRigidbody.transform;
        colliderTransform = colliderRb.transform;
    }

    private void Start()
    {
        motorTransform.parent = null;
        colliderTransform.parent = null;
    }

    private void Update()
    {
        DoMovement();
        DoRotateWheels();
        UpdateDrift();
    }

    

    private void FixedUpdate()
    {
        if(isOnGround)
        {
            motorRigidbody.AddForce(t.forward * thrust * acceleration, ForceMode.Acceleration);
            return;
        }

        motorRigidbody.AddForce(t.up * -10);

        colliderRb.MoveRotation(t.rotation);

        ApplyDrift();
    }

    private void DoMovement()
    {
        thrust = Convert.ToInt32(input.y);

        thrust *= input.y > 0 ? acceleration : brakeForce;

        t.position = motorTransform.position;

        float newRotation = Convert.ToInt32(input.x) * turnSpeed * Time.deltaTime * Convert.ToInt32(input.y);
        t.Rotate(0, newRotation, 0, Space.World);

        isOnGround = Physics.Raycast(t.position, -t.up, out RaycastHit hit, 1f, groundLayer);

        Quaternion desiredRotation = Quaternion.FromToRotation(t.up, hit.normal) * t.rotation;
        t.rotation = Quaternion.Slerp(t.rotation, desiredRotation, alignToGroundTime * Time.deltaTime);

        if (isOnGround)
        {
            float driftInput = Mathf.Abs(input.x);
            float driftMultiplier = (1.0f - driftInput) + 1.0f;
            driftFactor = Mathf.Lerp(driftFactor, driftInput * driftMultiplier, Time.deltaTime * 5f);

            currentSteerAngle = Mathf.Lerp(currentSteerAngle, input.x * (thrust > 0 ? turnSpeed : -turnSpeed), Time.deltaTime * 5f);
        }
        else
        {
            driftFactor = 0f;
            currentSteerAngle = 0f;
        }

        motorRigidbody.drag = isOnGround ? 4 : 0.1f;
    }

    private void DoRotateWheels()
    {
        //foreach (Transform w in wheels)
        //{
        //    w.Rotate(Time.deltaTime * input.y * rotationSpeed, 0, 0, Space.Self);
        //}

        rearLeftWheel.Rotate(Time.deltaTime * input.y * rotationSpeed, 0, 0, Space.Self);
        rearRightWheel.Rotate(Time.deltaTime * input.y * rotationSpeed, 0, 0, Space.Self);
        frontLeftWheel.Rotate(Time.deltaTime * input.y * rotationSpeed, 0, 0, Space.Self);
        frontRightWheel.Rotate(Time.deltaTime * input.y * rotationSpeed, 0, 0, Space.Self);
    }

    private void UpdateDrift()
    {
        bool isDrifting = driftFactor > 0.7f && Mathf.Abs(input.y) > 0.1f;

        foreach (TrailRenderer skidMark in skidMarks)
        {
            skidMark.emitting = isDrifting;
        }

        foreach (ParticleSystem smoke in driftSmoke)
        {
            if(isDrifting)
            {
                smoke.Play();
            }
            else
            {
                smoke.Stop();
            }
        }
    }

    private void ApplyDrift()
    {
        if (isOnGround)
        {
            float driftTorque = currentSteerAngle * 1000f * driftFactor;
            motorRigidbody.AddTorque(t.up * driftTorque);
        }
    }


    public Vector2 GetInputVector() => input;
}
