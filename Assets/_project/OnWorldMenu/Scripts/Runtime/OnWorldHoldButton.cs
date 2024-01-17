using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnWorldHoldButton : MonoBehaviour
{
    public Image fill;
    [Space]
    public UnityEvent OnCursorInteraction;

    private bool hasCursor;
    private float progress;

    public float Progress
    {
        get { return progress; }
        set 
        { 
            progress = value; 
            if(fill != null)
                fill.fillAmount = Mathf.Clamp01(progress);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out OnWorldCursor cursor) && !hasCursor)
        {
            hasCursor = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out OnWorldCursor cursor) && hasCursor)
        {
            hasCursor = false;
            Progress = 0;
        }
    }

    private void Update()
    {
        if (!hasCursor) return;

        Progress += Time.deltaTime * 0.33f;
        if(Progress >= 1)
        {
            hasCursor = false;
            Progress = 0;
            OnCursorInteraction?.Invoke();
        }
    }
}
