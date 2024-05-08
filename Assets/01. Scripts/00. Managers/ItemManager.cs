using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum ItemType
{
    TOWEL                   = 0b0, //0
    TOOTHBRUSH              = 0b1, //1
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
public class Item : ScriptableObject 
{
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField] private int mItemID;
    public int ItemID { get { return mItemID; } }

    [Header("아이템 중첩")]
    [SerializeField] private bool mCanOverlap;
    public bool CanOverlap { get { return mCanOverlap; } }

    [Header("사용(상호작용) 가능")]
    [SerializeField] private bool mIsInteractivity;
    public bool IsInteractivity { get { return mIsInteractivity; } }    

    [Header("아이템의 타입")]
    [SerializeField] private ItemType mItemType;
    public ItemType Type { get { return mItemType; } }

    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private Sprite mItemImage;
    public Sprite Image { get { return mItemImage; } }
}