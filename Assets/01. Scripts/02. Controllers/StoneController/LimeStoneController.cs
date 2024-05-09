using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimeStoneController : StoneController
{
    private LimeStone stone;
    
    new void Start()
    {
        base.Start();
        Debug.Log("hi");
    }
}

public class LimeStone : Stone
{
    public LimeStone(string scientificName, string nickName, int id, IStoneState state) : base(scientificName, nickName, id, state)
    {
    }
}