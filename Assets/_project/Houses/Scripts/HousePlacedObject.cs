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

    private void OnEnable()
    {
        GameManager.OnLostTask += GameManager_OnLostTask;
    }

    private void OnDisable()
    {
        GameManager.OnLostTask -= GameManager_OnLostTask;
    }

    private void GameManager_OnLostTask(object sender, Task e)
    {
        if (!hasTask)
        {
            return;
        }

        if(task.House.Equals(this))
        {
            hasTask = false;
            task = null;
            taskInidcator.SetActive(false);
        }
    }

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
