using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeManagerItem[] items;

    private Upgrade[] allUpgrades;
    private List<Upgrade> randomUpgrades;

    private void Awake()
    {
        allUpgrades = Resources.LoadAll<Upgrade>("Upgrades") as Upgrade[];
    }

    private void Start()
    {
        GenerateRandomUpgrade();
    }

    private void GenerateRandomUpgrade()
    {
        randomUpgrades = new List<Upgrade>();
        Upgrade upgrade = null;

        for (int i = 0; i < 3; i++)
        {
            do
            {
                upgrade = allUpgrades[Random.Range(0, allUpgrades.Length)];
            }
            while (randomUpgrades.Contains(upgrade));
            randomUpgrades.Add(upgrade);
            items[i].AsignUpgrade(upgrade);
        }
    }
}
