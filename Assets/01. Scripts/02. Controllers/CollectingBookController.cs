using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class CollectingBookController : MonoBehaviour
{
    [SerializeField] GameObject stoneFrame;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance._book.StartSpawnStones();
    }
}
