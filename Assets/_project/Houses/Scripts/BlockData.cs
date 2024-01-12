using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New Block data")]
public class BlockData : ScriptableObject
{
    public BuildingType[] mandatoryBuildings;
    public BlockDataItem[] possibleBuildings;

    public BlockDataItem GetRandomBlockItem(List<NeighbothoodItemCount<PlacedObject>> itemCount)
    {
        int totalWeight = 0;
        for (int i = 0; i < possibleBuildings.Length; i++)
        {
            totalWeight += possibleBuildings[i].weight;
        }

        int rnd = Random.Range(1, totalWeight + 1);
        int processedWeight = 0;
        for (int i = 0; i < possibleBuildings.Length; i++)
        {
            processedWeight += possibleBuildings[i].weight;
            if(rnd <= processedWeight)
            {
                if (possibleBuildings[i].maxPossibleBuilding == 0)
                    return possibleBuildings[i];

                var obj = itemCount.Find(p => p.item.GetBuildingType().Equals(possibleBuildings[i].building));
                if(obj == null)
                    return possibleBuildings[i];
                if (obj.amount < possibleBuildings[i].maxPossibleBuilding)
                    return possibleBuildings[i];
            }
        }

        return default(BlockDataItem);
    }
}
[System.Serializable]
public struct BlockDataItem
{
    public BuildingType building;
    public int weight;
    [Tooltip("0 for infinity")] public int maxPossibleBuilding;
}