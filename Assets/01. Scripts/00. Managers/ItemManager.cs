using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    Dictionary<ITEM_TYPE, int> itemInventory;

    // Start is called before the first frame update
    void Start()
    {
        itemInventory = new Dictionary<ITEM_TYPE, int>();
        itemInventory.Add(ITEM_TYPE.Brush, 0);
        itemInventory.Add(ITEM_TYPE.Towel, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
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