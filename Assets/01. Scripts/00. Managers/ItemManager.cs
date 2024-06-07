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
        if (itemInventory == null)
            itemInventory = new Dictionary<ITEM_TYPE, int>();

        // itemInventory = new Dictionary<ITEM_TYPE, int>();
        // itemInventory.Add(ITEM_TYPE.Brush, 1);
        // itemInventory.Add(ITEM_TYPE.Towel, 1);
    }

    public Dictionary<ITEM_TYPE, int> GetItemInventory()
    {
        return itemInventory;
    }

    public void SetItemInventory(Dictionary<ITEM_TYPE, int> inventory)
    {
        if (inventory == null)
        {
            Debug.LogError("Attempted to set item inventory to null!");
            return;
        }
        itemInventory = inventory;
    }

    public int GetItemNum(ITEM_TYPE item)
    {
        return itemInventory[item];
    }

    public void AcquireItem(ITEM_TYPE item)
    {
        itemInventory[item]+=1;
    }

    public void ConsumeItem(ITEM_TYPE item)
    {
        itemInventory[item]-=1;
    }
}

public enum ITEM_TYPE
{
    Brush, Towel
}