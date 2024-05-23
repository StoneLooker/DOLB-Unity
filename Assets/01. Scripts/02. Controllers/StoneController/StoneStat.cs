using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stone", menuName = "Add Stone/Stone")]
public class StoneStat : ScriptableObject
{
    [Header("돌의 이미지")]
    [SerializeField] private Sprite mItemImage;
    public Sprite Image { get { return mItemImage; } }
}
