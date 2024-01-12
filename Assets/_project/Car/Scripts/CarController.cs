using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    public struct CarPhysics
    {
        public Rigidbody motor;
        public Rigidbody carCollider;
        public LayerMask groundLayer;
        public float groundDistance;
    }

    [System.Serializable]
    public struct CarVisuals
    {
        public Transform[] Wheels;
        public float wheelRotationSpeed;
        public TrailRenderer[] skidMarks;
    }

    [SerializeField] private StatAsset carStats;

    //[Header("Physics")]
    [SerializeField] private CarPhysics carPhysics;
    [SerializeField] private CarVisuals carVisuals;
    

    private Transform t;

    private float forwardAccel;
    private float brake;
    private float turnStrength;

    private float speed;
    private float turn;
    private bool isOnGround;
    private float drag;

    private Func<float> accel;
    private Func<float> steer;

    private SerializableDictionary<Stat, float> localStats;

    public float GetAccel() => accel();
    public float GetSteerVector() => steer();

    public Rigidbody GetMotor() => carPhysics.motor;

    public void SetMoveFunc(Func<float> accel, Func<float> steer)
    {
        this.accel = accel;
        this.steer = steer;
    }

    private void Awake()
    {
        t = transform;
        
        localStats = carStats.instanceStats;

        forwardAccel = carStats.GetStat(Stat.Acceleration);
        brake = carStats.GetStat(Stat.BrakeForce);
        turnStrength = carStats.GetStat(Stat.TurnStrengh);
    }

    private void Start()
    {
        drag = carPhysics.motor.drag;
        carPhysics.motor.transform.parent = null;
        carPhysics.carCollider.transform.parent = null;
    }

    private void Update()
    {
        if(accel() > 0)
        {
            speed = forwardAccel * 1000f;
        }
        else if (accel() < 0)
        {
            speed = brake * 1000f;
        }

        turn = steer() * turnStrength * Time.deltaTime * accel();

        if(isOnGround)
            t.rotation = Quaternion.Euler(t.rotation.eulerAngles + new Vector3(0f, turn, 0f));


        t.position = carPhysics.motor.transform.position;

        foreach (Transform wheel in carVisuals.Wheels)
        {
            wheel.Rotate(Time.deltaTime * carVisuals.wheelRotationSpeed * accel(), 0, 0, Space.Self);
        }

        foreach (TrailRenderer skid in carVisuals.skidMarks)
        {
            skid.emitting = MathF.Abs(turn) > 1f;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        isOnGround = Physics.Raycast(t.position, -t.up, out hit, carPhysics.groundDistance, carPhysics.groundLayer);


        if(isOnGround)
        {
            carPhysics.motor.drag = drag;
            t.rotation = Quaternion.FromToRotation(t.up, hit.normal) * t.rotation;

            if(MathF.Abs(speed) > 0)
            {
                carPhysics.motor.AddForce(t.forward * speed * accel());
            }
        }
        else
        {
            carPhysics.motor.drag = 0.1f;
            carPhysics.motor.AddForce(Vector3.up * -1000);
        }

        carPhysics.carCollider.MoveRotation(t.rotation);
    }
}
