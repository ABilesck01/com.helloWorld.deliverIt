using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField, Tooltip("Day duration in seconds")] private float dayDuration = 600;

    private float currentDayDuration = 0;
    private int day;
    private bool isCounting = true;

    public static event EventHandler<int> OnDayOver;

    private void Start()
    {
        day = 1;
        currentDayDuration = 0;
        isCounting = true;
    }

    private void Update()
    {
        if (!isCounting) return;

        currentDayDuration += Time.deltaTime;
        if (currentDayDuration >= dayDuration)
        {
            currentDayDuration = dayDuration;
            day++;
            isCounting = false;
            OnDayOver?.Invoke(this, day);
        }
    }
}
