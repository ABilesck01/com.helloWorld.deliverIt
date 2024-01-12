using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputs : MonoBehaviour
{
    private Vector2 moveInputs;
    private float accel;
    private float steer;

    public void GetAcceleratorInput(InputAction.CallbackContext context)
    {
        accel = context.ReadValue<float>();
    }

    public void GetSteeringWheelInput(InputAction.CallbackContext context)
    {
        steer = context.ReadValue<float>();
    }

    private void Start()
    {
        CarController controller = GetComponentInChildren<CarController>();
        controller.SetMoveFunc(
            () => accel,
            () => steer
            );
    }
}
