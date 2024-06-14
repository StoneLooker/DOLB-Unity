using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum to define different item types with bitwise flags
[System.Flags]
public enum ItemType
{
    TOWEL                   = 0b0, //0
    TOOTHBRUSH              = 0b1, //1
}

//ScriptableObject class to define item statistics and properties
[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
public class ItemStat : ScriptableObject 
{
    //Unique ID for the item (non-duplicable)
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField] private int mItemID;
    public int ItemID { get { return mItemID; } }

    //Flag to indicate if the item can overlap in inventory
    [Header("아이템 중첩")]
    [SerializeField] private bool mCanOverlap;
    public bool CanOverlap { get { return mCanOverlap; } }

    //Flag to indicate if the item is interactive
    [Header("사용(상호작용) 가능")]
    [SerializeField] private bool mIsInteractivity;
    public bool IsInteractivity { get { return mIsInteractivity; } }    

    //Type of the item
    [Header("아이템의 타입")]
    [SerializeField] private ItemType mItemType;
    public ItemType Type { get { return mItemType; } }

    //Image of the item to be displayed in the inventory
    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private Sprite mItemImage;
    public Sprite Image { get { return mItemImage; } }
}