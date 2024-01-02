using UnityEngine;

[System.Serializable]
public class Bag
{
    public enum BagType
    {
        blue,
        red
    }
    public Transform bagPrefab;
    public BagType bagType;

    public static BagType GetRandomType()
    {
        var enumValues = System.Enum.GetValues(typeof(BagType));
        return (BagType)enumValues.GetValue(Random.Range(0, enumValues.Length));
    }
}
