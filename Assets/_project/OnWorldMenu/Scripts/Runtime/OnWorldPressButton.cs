using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnWorldPressButton : MonoBehaviour, IOnWorldInteraction
{
    public UnityEvent<OnWorldCursor> OnCursorHoverEnter;
    public UnityEvent<OnWorldCursor> OnCursorHoverExit;
    public UnityEvent<OnWorldCursor> OnCursorInteraction;

    public void Interact(OnWorldCursor onWorldCursor)
    {
        OnCursorInteraction?.Invoke(onWorldCursor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out OnWorldCursor cursor))
        {
            Debug.Log("On trigger enter", other);
            cursor.SetInteraction(this);
            OnCursorHoverEnter?.Invoke(cursor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out OnWorldCursor cursor))
        {
            Debug.Log("On trigger exit", other);
            cursor.ClearInteraction();
            OnCursorHoverExit?.Invoke(cursor);
        }
    }
}
