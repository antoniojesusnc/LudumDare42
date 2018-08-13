using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField]
    int _inventoryCapacity;

    public Dictionary<ETypeAnimal, int> Inventory { get; private set; }
    public bool IsInventoryFull
    {
        get
        {
            int sum = 0;
            foreach (var item in Inventory)
            {
                sum += item.Value;
            }

            return sum >= _inventoryCapacity;
        }
    }

    private void Start()
    {
        Inventory = new Dictionary<ETypeAnimal, int>();

        Inventory = new Dictionary<ETypeAnimal, int>();
    }

    public void AddAnimal(ETypeAnimal typeAnimal)
    {
        if (!Inventory.ContainsKey(typeAnimal))
            Inventory.Add(typeAnimal, 0);

        ++Inventory[typeAnimal];
    }
}
