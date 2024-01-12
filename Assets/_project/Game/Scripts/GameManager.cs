using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxTaskCount = 1;
    [SerializeField] private float timeToNewTask = 5f;
    [SerializeField] private float taskTime = 60f;
    [Space]
    [SerializeField] private List<NeighborhoodController> neighborhoods;
    [SerializeField] private List<Task> tasks = new List<Task>();
    
    private float currentTimeToNewTask = 0;
    private int currentTaskCount = 0;

    public static event EventHandler<Task> OnNewTask;
    public static event EventHandler<Task> OnLostTask;

    private void OnEnable()
    {
        HousePlacedObject.OnTaskDelivered += HousePlacedObject_OnTaskDelivered;
    }

    private void OnDisable()
    {
        HousePlacedObject.OnTaskDelivered -= HousePlacedObject_OnTaskDelivered;
    }

    private void HousePlacedObject_OnTaskDelivered(object sender, HousePlacedObject e)
    {
        tasks.Remove(e.GetTask());
        if (currentTaskCount >= maxTaskCount)
        {
            currentTimeToNewTask = 0;
        }
        currentTaskCount--;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        HandleTasks();
        HandleTasksTimer();
    }

    private void HandleTasksTimer()
    {
        foreach (Task task in tasks.ToList())
        {
            task.CurrentTimeInSeconds += Time.deltaTime;
            if(task.CurrentTimeInSeconds >= task.MaxTime)
            {
                currentTaskCount--;
                tasks.Remove(task);
                OnLostTask?.Invoke(this, task);
            }
        }
    }

    private void HandleTasks()
    {
        if (currentTaskCount >= maxTaskCount) return;

        currentTimeToNewTask += Time.deltaTime;
        if (currentTimeToNewTask >= timeToNewTask)
        {
            GenerateTask();
            currentTimeToNewTask = 0;
        }
    }

    private void GenerateTask()
    {
        Bag.BagType bagType = Bag.GetRandomType();
        NeighborhoodController neighborhood = neighborhoods[Random.Range(0, neighborhoods.Count - 1)];
        HousePlacedObject house = neighborhood.GetRandomHouse();
        Task t = new Task(bagType, 1, house,10, taskTime);
        tasks.Add(t);
        house.AsignTask(t);
        OnNewTask?.Invoke(OnNewTask, t);
        currentTaskCount++;
    }

    public List<Task> GetTasks() => tasks;

    public void OnPlayerJoin()
    {
        maxTaskCount++;
        GenerateTask();
    }

    public void OnPlayerLeft()
    {
        maxTaskCount--;
    }

}
