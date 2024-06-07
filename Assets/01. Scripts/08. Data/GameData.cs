using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string playerId;
    public Dictionary<ITEM_TYPE, int> itemInventory;

    public Dictionary<ITEM_TYPE, int> GetItemInventory()
    {
        if (itemInventory == null)
        {
            itemInventory = new Dictionary<ITEM_TYPE, int>();
        }
        return itemInventory;
    }

    public void SetItemInventory(Dictionary<ITEM_TYPE, int> inventory)
    {
        itemInventory = inventory;
    }
}