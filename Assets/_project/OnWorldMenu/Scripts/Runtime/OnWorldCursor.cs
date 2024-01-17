using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnWorldCursor : MonoBehaviour
{
    [SerializeField] private IOnWorldInteraction interaction;
    [SerializeField] private CarController carController;

    public CarController GetCarController() { return carController; }

    public void TryInteract()
    {
        if (interaction == null) return;

        interaction.Interact(this);
    }

    public void SetInteraction(IOnWorldInteraction interaction)
    {
         this.interaction = interaction;
    }

    public void ClearInteraction()
    {
        //this.interaction = null;
    }

}
