using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePlacedObject : PlacedObject
{
    [SerializeField] private GameObject taskInidcator;
    private bool hasTask = false;
    //private Bag.BagType bagType;
    //private int bagAmount = 1;
    private Task task;

    public static event EventHandler<HousePlacedObject> OnTaskDelivered;

    public bool HasTask() => hasTask;

    public void AsignTask(Task task)
    {
        hasTask = true;
        this.task = task;
        taskInidcator.SetActive(true);
    }

    public bool ReceiveBag(Bag bag)
    {
        if (!hasTask)
        {
            return false;
        }

        if (bag.bagType != task.Type)
            return false;

        hasTask = false;
        OnTaskDelivered?.Invoke(this, this);
        task = null;
        taskInidcator.SetActive(false);
        return true;
    }

    public Task GetTask()
    {
        return task;
    }
}
