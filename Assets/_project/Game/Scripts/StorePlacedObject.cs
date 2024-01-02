using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlacedObject : PlacedObject
{
    [SerializeField] private Bag bag;

    public Bag GetBag() => bag;

}
