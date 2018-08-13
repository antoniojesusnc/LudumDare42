using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField]
    int _inventoryCapacity;
    public int InventoryCapacity
    {
        get { return _inventoryCapacity; }
        set { _inventoryCapacity = value; }
    }

    public Dictionary<ETypeAnimal, int> InventoryInPegi { get; private set; }
    public bool IsInventoryFull
    {
        get
        {
            int sum = 0;
            foreach (var item in InventoryInPegi)
            {
                sum += item.Value;
            }

            return sum >= _inventoryCapacity;
        }
    }
    public Dictionary<ETypeAnimal, int> InventorySent { get; private set; }

    private void Start()
    {
        InventoryInPegi = new Dictionary<ETypeAnimal, int>();
        InventorySent = new Dictionary<ETypeAnimal, int>();

        for (int i = (int)ETypeAnimal.Size - 1; i >= 0; --i)
        {
            InventoryInPegi.Add((ETypeAnimal)i, 0);
            InventorySent.Add((ETypeAnimal)i, 0);
        }
    }

    public void AddAnimalToPegi(ETypeAnimal typeAnimal)
    {
        ++InventoryInPegi[typeAnimal];
    }
}

