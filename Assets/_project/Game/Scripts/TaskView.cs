using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskView : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private TaskViewItem template;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        GameManager.OnNewTask += GameManager_OnNewTask;
        GameManager.OnLostTask += GameManager_OnNewTask;
        HousePlacedObject.OnTaskDelivered += HousePlacedObject_OnTaskDelivered;
    }

    private void OnDisable()
    {
        GameManager.OnNewTask -= GameManager_OnNewTask;
        GameManager.OnLostTask -= GameManager_OnNewTask;
        HousePlacedObject.OnTaskDelivered -= HousePlacedObject_OnTaskDelivered;
    }

    private void GameManager_OnNewTask(object sender, Task e)
    {
        //gameManager = (GameManager)sender;
        UpdateTasks();
    }

    private void HousePlacedObject_OnTaskDelivered(object sender, HousePlacedObject e)
    {
        UpdateTasks();
    }

    private void UpdateTasks()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }

        foreach (Task item in gameManager.GetTasks())
        {
            var taskItem = Instantiate(template, container);
            taskItem.SetTask(item);
        }
    }
}
