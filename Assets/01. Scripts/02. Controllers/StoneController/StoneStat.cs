using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stone", menuName = "Add Stone/Stone")]
public class StoneStat : ScriptableObject
{
    [Header("Sprite")]
    [SerializeField] private Sprite mItemImage;
    public Sprite Image { get { return mItemImage; } }
}
