using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskViewItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Slider progress;
    [Space]
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;
    [Space]
    [SerializeField] private Sprite redIcon;
    [SerializeField] private Sprite blueIcon;
    [SerializeField] private Task task;

    public void SetTask(Task task)
    {
        this.task = task;

        icon.sprite = task.Type == Bag.BagType.red ? redIcon : blueIcon;
        progress.maxValue = task.MaxTime;
        progress.value = task.MaxTime;
    }

    private void Update()
    {
        progress.value = task.MaxTime - task.CurrentTimeInSeconds;
        fill.color = gradient.Evaluate(fill.fillAmount);
    }
}
