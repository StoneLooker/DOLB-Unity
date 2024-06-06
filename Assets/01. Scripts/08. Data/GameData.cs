using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string playerId;
    public Dictionary<ITEM_TYPE, int> itemInventory;
}