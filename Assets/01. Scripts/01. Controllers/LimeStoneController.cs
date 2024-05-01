using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeStoneController : MonoBehaviour
{
    private Stone stone;
    
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnMouseDrag()
    {
        stone.Drag();
    }
}

public class LimeStone : Stone
{
    public LimeStone(string scientificName, string nickName, int id, StoneState state) : base(scientificName, nickName, id, state)
    {
    }
}
