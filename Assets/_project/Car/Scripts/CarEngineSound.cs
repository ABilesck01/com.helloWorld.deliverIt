using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    private AudioSource audioSource;
    private CarController carController;
    
    float currentSpeed;
    float pitchFromCar;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        carController = GetComponentInParent<CarController>();
    }

    private void Update()
    {
        currentSpeed = carController.GetMotor().velocity.magnitude;
        pitchFromCar = carController.GetMotor().velocity.magnitude / 75f;

        if(currentSpeed < minSpeed)
        {
            audioSource.pitch = minPitch;
        }
        else if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            audioSource.pitch = minPitch + pitchFromCar;
        }
        else if(currentSpeed > maxSpeed)
        {
            audioSource.pitch = maxPitch;
        }
    }
}
