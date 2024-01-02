using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskViewItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [Space]
    [SerializeField] private Sprite redIcon;
    [SerializeField] private Sprite blueIcon;
    [SerializeField] private Task task;

    public void SetTask(Task task)
    {
        this.task = task;

        icon.sprite = task.Type == Bag.BagType.red ? redIcon : blueIcon;
    }
}
