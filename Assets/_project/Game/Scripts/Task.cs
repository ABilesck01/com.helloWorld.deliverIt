using UnityEngine;

[System.Serializable]
public class Task
{
    [SerializeField] private string id;
    [SerializeField] private Bag.BagType type;
    [SerializeField] private float maxTime;
    [SerializeField] private HousePlacedObject house;

    private StatAsset stats;


    private int money;
    private float currentTimeInSeconds;

    public Task()
    {
        this.money = 10;
        this.maxTime = 30;
    }

    public Task(Bag.BagType type, int amount, HousePlacedObject house, int money, float maxTime)
    {
        this.id = ExtensionMethods.RandomID();
        this.type = type;
        this.house = house;
        this.money = (int)stats.GetStat(global::Stat.Money);
        this.maxTime = maxTime;
    }

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public Bag.BagType Type
	{
		get { return type; }
		set { type = value; }
	}

    public HousePlacedObject House
    {
        get { return house; }
        set { house = value; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public float MaxTime
    {
        get { return maxTime; }
        set { maxTime = value; }
    }

    public float CurrentTimeInSeconds
    {
        get { return currentTimeInSeconds; }
        set { currentTimeInSeconds = value; }
    }

    public StatAsset Stat
    {
        get { return stats; }
        set { stats = value; }
    }
}
