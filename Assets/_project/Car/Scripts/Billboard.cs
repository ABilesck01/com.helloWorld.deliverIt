using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        // Ensure the object faces the camera
        FaceCamera();
    }

    void FaceCamera()
    {
        if (mainCamera != null)
        {
            // Calculate the direction from the object to the camera
            Vector3 toCamera = mainCamera.transform.position - transform.position;

            // Ensure the object faces the camera only on the y-axis (upwards)
            //toCamera.y = 0f;

            // Rotate the object to face the camera
            transform.rotation = Quaternion.LookRotation(-toCamera);
        }
        else
        {
            Debug.LogWarning("Main camera not found. Make sure you have a camera in the scene.");
        }
    }
}
