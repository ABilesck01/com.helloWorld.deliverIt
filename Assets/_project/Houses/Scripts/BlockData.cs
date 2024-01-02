using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New Block data")]
public class BlockData : ScriptableObject
{
    public BuildingType[] possibleBuildings;
}
