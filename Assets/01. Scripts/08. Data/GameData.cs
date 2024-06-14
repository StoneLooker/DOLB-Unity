using System.Collections.Generic;

//Class to store game data
[System.Serializable]
public class GameData
{
    public string playerId;
    public Dictionary<ITEM_TYPE, int> itemInventory;

    //Method to get the item inventory, initializing it if necessary
    public Dictionary<ITEM_TYPE, int> GetItemInventory()
    {
        if (itemInventory == null)
        {
            itemInventory = new Dictionary<ITEM_TYPE, int>();
        }
        return itemInventory;
    }

    //Method to set the item inventory
    public void SetItemInventory(Dictionary<ITEM_TYPE, int> inventory)
    {
        itemInventory = inventory;
    }
}