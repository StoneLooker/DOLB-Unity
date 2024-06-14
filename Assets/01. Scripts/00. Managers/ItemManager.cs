using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    Dictionary<ITEM_TYPE, int> itemInventory;

    // Start is called before the first frame update
    public void OnStart()
    {   
        // Initialize the item inventory if it is null
        if (itemInventory == null)
            itemInventory = new Dictionary<ITEM_TYPE, int>();
    }

    // Method to get the current item inventory
    public Dictionary<ITEM_TYPE, int> GetItemInventory()
    {
        return itemInventory;
    }

    // Method to set the item inventory
    public void SetItemInventory(Dictionary<ITEM_TYPE, int> inventory)
    {
        if (inventory == null)
        {
            Debug.LogError("Attempted to set item inventory to null!");
            return;
        }
        itemInventory = inventory;
    }

    // Method to get the number of a specific item
    public int GetItemNum(ITEM_TYPE item)
    {
        return itemInventory[item];
    }

    // Method to acquire an item (increase its count)
    public void AcquireItem(ITEM_TYPE item)
    {
        itemInventory[item]+=1;
    }

    // Method to consume an item (decrease its count)
    public void ConsumeItem(ITEM_TYPE item)
    {
        itemInventory[item]-=1;
    }
}

// Enumeration for item types
public enum ITEM_TYPE
{
    Brush, Towel
}