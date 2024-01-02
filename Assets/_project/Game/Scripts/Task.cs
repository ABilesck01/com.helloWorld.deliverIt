using UnityEngine;

[System.Serializable]
public class Task
{
	[SerializeField] private Bag.BagType type;
	[SerializeField] private int amount;
	[SerializeField] private HousePlacedObject house;

    public Task()
    {
    }

    public Task(Bag.BagType type, int amount, HousePlacedObject house)
    {
        this.type = type;
        this.amount = amount;
        this.house = house;
    }

    public Bag.BagType Type
	{
		get { return type; }
		set { type = value; }
	}

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public HousePlacedObject House
    {
        get { return house; }
        set { house = value; }
    }
}
