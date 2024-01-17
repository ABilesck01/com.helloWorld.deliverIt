using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Stats/New Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public Transform visual;
    public int cost;
    public SerializableDictionary<Stat, float> statsToAply;
    public bool isPercentage;
    public Upgrade replacement;
}
