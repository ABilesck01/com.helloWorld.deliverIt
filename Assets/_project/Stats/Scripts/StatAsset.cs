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
        if(stats.TryGetValue(stat, out var value)) return value;

        return 0;
    }
}
