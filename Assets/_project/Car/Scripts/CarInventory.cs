using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInventory : MonoBehaviour
{
    [SerializeField] private float timeToFill = 1f;
    [SerializeField] private int capacity;
    [SerializeField] private List<Bag> bags;
    [Header("Settings")]
    [SerializeField] private LayerMask storeLayer;
    [SerializeField] private LayerMask houseLayer;
    [SerializeField] private LayerMask trashLayer;
    [SerializeField] private float radius = 2f;
    [Header("Visual")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spacing;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Slider progressSlider;

    private StorePlacedObject store;
    private HousePlacedObject house;
    private TrashPlacedObject trash;

    private CarController carController;
    private Transform t;

    private bool isSearching = false;
    private float currentTimeToFill = 0;

    public static event EventHandler<Bag> OnUpdateInventory;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        bags = new List<Bag>();
        t = transform;
        progressSlider.maxValue = timeToFill;
    }

    private void Update()
    {
        SwitchSearch();
        SearchForStore();
        SearchForHouse();
        SearchForTrash();
    }

    private void SwitchSearch()
    {
        if (Mathf.Abs(carController.GetAccel()) < 0.1f && !isSearching)
        {
            isSearching = true;
            progressSlider.value = 0;
        }
        if(Mathf.Abs(carController.GetAccel()) > 0.1f && isSearching)
        {
            isSearching = false;
            store = null;
            house = null;
            trash = null;
            currentTimeToFill = 0;
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0;
        }
    }

    private void SearchForStore()
    {
        if(!isSearching)
        {
            return;
        }

        if(store == null)
        {
            Collider[] stores = Physics.OverlapSphere(t.position, radius, storeLayer);
            if (stores.Length <= 0)
            {
                return;
            }
            store = stores[0].GetComponentInParent<StorePlacedObject>();
            currentTimeToFill = 0;
        }

        if(bags.Count >= capacity)
        {
            return;
        }
        progressSlider.gameObject.SetActive(true);
        currentTimeToFill += Time.deltaTime;
        progressSlider.value = currentTimeToFill;

        if (currentTimeToFill > timeToFill)
        {
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0;
            currentTimeToFill = 0;
            bags.Add(store.GetBag());
            OnUpdateInventory?.Invoke(this, store.GetBag());
            UpdateBagsInInventory();
        }
    }

    private void SearchForHouse()
    {
        if (!isSearching)
        {
            return;
        }

        if (house == null)
        {
            Collider[] houses = Physics.OverlapSphere(t.position, radius, houseLayer);
            if (houses.Length <= 0)
            {
                return;
            }
            house = houses[0].GetComponentInParent<HousePlacedObject>();
            currentTimeToFill = 0;
        }

        if (bags.Count <= 0)
        {
            return;
        }

        if(!house.HasTask())
        { 
            return; 
        }

        progressSlider.gameObject.SetActive(true);
        currentTimeToFill += Time.deltaTime;
        progressSlider.value = currentTimeToFill;
        if (currentTimeToFill >= timeToFill)
        {
            currentTimeToFill = 0;
            //bags.Add(store.GetBag());
            foreach (Bag bag in bags)
            {
                bool needBag = house.ReceiveBag(bag);
                Debug.Log(needBag);
                if (needBag)
                {
                    bags.Remove(bag);
                    OnUpdateInventory?.Invoke(this, bag);
                    break;
                }
            }
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0;
            UpdateBagsInInventory();
        }
    }

    private void SearchForTrash()
    {
        if (!isSearching)
        {
            return;
        }

        if (trash == null)
        {
            Collider[] trashes = Physics.OverlapSphere(t.position, radius, trashLayer);
            if (trashes.Length <= 0)
            {
                return;
            }
            trash = trashes[0].GetComponentInParent<TrashPlacedObject>();
            currentTimeToFill = 0;
        }

        if (bags.Count <= 0)
        {
            return;
        }

        progressSlider.gameObject.SetActive(true);
        currentTimeToFill += Time.deltaTime;
        progressSlider.value = currentTimeToFill;
        if (currentTimeToFill >= timeToFill)
        {
            bags.RemoveAt(0);
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0;
            UpdateBagsInInventory();
        }
    }


    private void UpdateBagsInInventory()
    {
        foreach (Transform t in spawnPosition)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < bags.Count; i++)
        {
            Vector3 position = spawnPosition.position + i * spacing * direction;
            var newBag = Instantiate(bags[i].bagPrefab, position, spawnPosition.rotation);
            newBag.parent = spawnPosition;
        }
    }

    public bool HasBagTypeInInventory(Bag.BagType bagType)
    {
        foreach(var bag in bags)
        {
            if(bag.bagType == bagType)
                return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
