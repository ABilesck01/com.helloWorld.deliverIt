using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Stats/New Stats")]
public class StatAsset : ScriptableObject
{
    public SerializableDictionary<Stat, float> stats = new SerializableDictionary<Stat, float>();
    public SerializableDictionary<Stat, float> instanceStats = new SerializableDictionary<Stat, float>();
    public List<Upgrade> upgrades = new List<Upgrade>();

    public float GetStat(Stat stat)
    {
        if(stats.TryGetValue(stat, out var value)) 
            return GetUpgradedValue(stat, value);
        else if(instanceStats.TryGetValue(stat, out float instanceValue))
            return GetUpgradedValue(stat, instanceValue);

        return 0;
    }

    private float GetUpgradedValue(Stat stat, float baseValue)
    {
        foreach(var upgrade in upgrades)
        {
            if (!upgrade.statsToAply.TryGetValue(stat, out float upgradeValue))
                continue;
            if (upgrade.isPercentage)
                baseValue *= (upgradeValue / 100f) + 1f;
            else
                baseValue += upgradeValue;
        }

        return baseValue;
    }

    public void UnlockUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);

        if (upgrade.replacement == null) return;

        if (!upgrades.Contains(upgrade.replacement)) return;

        upgrades.Remove(upgrade.replacement);
    }
}
