using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class NeighborhoodController : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private int initialBuildings;
    [Space]
    [SerializeField] private BlockController[] blocks;

    private float currentTimeToSpawn = 0;

    private List<StorePlacedObject> stores = new List<StorePlacedObject>();
    private List<HousePlacedObject> houses = new List<HousePlacedObject>();
    private List<PlacedObject> placedObjects = new List<PlacedObject>();

    private void Awake()
    {
        blocks = GetComponentsInChildren<BlockController>();
    }

    private void Start()
    {
        for (int i = 0; i < initialBuildings; i++)
        {
            int rand = Random.Range(0, blocks.Length);
            var b = blocks[rand].BuildRandom();

            if (b is StorePlacedObject)
                stores.Add((StorePlacedObject)b);
            else if (b is HousePlacedObject)
                houses.Add((HousePlacedObject)b);
            else
                placedObjects.Add(b);
        }
    }

    public bool HasStoreOfColor(Bag.BagType color)
    {
        foreach (var store in stores)
        {
            if(store.GetBag().bagType == color) return true;
        }

        return false;
    }

    public StorePlacedObject GetStoreOfColor(Bag.BagType type)
    {
        foreach (var store in stores)
        {
            if (store.GetBag().bagType == type) return store;
        }

        return null;
    }

    public List<StorePlacedObject> GetStoreListOfColor(Bag.BagType type)
    {
        List<StorePlacedObject> stores = new List<StorePlacedObject>();
        foreach (var store in this.stores)
        {
            if (store.GetBag().bagType == type)
            {
                stores.Add(store);
            }
        }

        return stores;
    }

    public HousePlacedObject GetRandomHouse()
    {
        HousePlacedObject house = houses[0];

        do
        {
            house = houses[Random.Range(0, houses.Count - 1)];
        }
        while (house.HasTask());

        return house;
    }
}
