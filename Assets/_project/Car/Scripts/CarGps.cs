using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarGps : MonoBehaviour
{
    //gettasks
    //check inventory for color
    //if dont, point to store
    //if yes, point to house destination

    [SerializeField] private Transform arrow;
    
    private NeighborhoodController neighborhoodController;
    private CarInventory inventory;
    [SerializeField] private Transform target;

    private Task task;

    private void Awake()
    {
        inventory = GetComponent<CarInventory>();
        neighborhoodController = FindObjectOfType<NeighborhoodController>();
    }

    private void OnEnable()
    {
        GameManager.OnNewTask += GameManager_OnNewTask;
        GameManager.OnLostTask += GameManager_OnLostTask;
        CarInventory.OnUpdateInventory += CarInventory_OnUpdateInventory;
        HousePlacedObject.OnTaskDelivered += HousePlacedObject_OnTaskDelivered;
    }

    private void OnDisable()
    {
        GameManager.OnNewTask -= GameManager_OnNewTask;
        GameManager.OnLostTask -= GameManager_OnLostTask;
        CarInventory.OnUpdateInventory -= CarInventory_OnUpdateInventory;
        HousePlacedObject.OnTaskDelivered -= HousePlacedObject_OnTaskDelivered;
    }

    private void Update()
    {
        if (target == null)
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        PointTowardsTarget();
    }

    private void PointTowardsTarget()
    {
        Vector3 targetDirection = new Vector3(target.position.x - arrow.position.x, 0f, target.position.z - arrow.position.z);

        // Rotate only around the Y-axis
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Apply the rotation to the object
        arrow.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }

    Transform GetNearestTransform(List<StorePlacedObject> targets)
    {
        Transform nearestTransform = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (StorePlacedObject target in targets)
        {
            float distance = Vector3.Distance(currentPosition, target.GetGpsPoint().position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTransform = target.GetGpsPoint();
            }
        }

        return nearestTransform;
    }

    private void GameManager_OnNewTask(object sender, Task e)
    {
        task = e;
        arrow.gameObject.SetActive(true);
        if (inventory.HasBagTypeInInventory(e.Type))
        {
            target = e.House.GetGpsPoint();
        }
        else
        {
            target = GetNearestTransform(neighborhoodController.GetStoreListOfColor(e.Type));
        }

    }

    private void GameManager_OnLostTask(object sender, Task e)
    {
        target = null;
        arrow.gameObject.SetActive(false);
    }

    private void CarInventory_OnUpdateInventory(object sender, Bag e)
    {
        if (inventory.HasBagTypeInInventory(task.Type))
        {
            target = task.House.GetGpsPoint();
        }
        else
        {
            target = GetNearestTransform(neighborhoodController.GetStoreListOfColor(task.Type));
        }
    }

    private void HousePlacedObject_OnTaskDelivered(object sender, HousePlacedObject e)
    {
        target = null;
        arrow.gameObject.SetActive(false);
    }
}
