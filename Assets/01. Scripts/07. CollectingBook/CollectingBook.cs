using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CollectingBook
{
    public string stoneName;
    public int stoneNumber;
    public string memberNickName;
}

public class CollectingBookList
{
    public List<CollectingBook> stones;
}