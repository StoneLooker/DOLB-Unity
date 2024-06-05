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
        SpawnStone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnStone()
    {
        foreach(Stone element in GameManager.Stone.collectingBook)
        {
            Debug.Log(GameManager.Stone.collectingBook);
            Instantiate(stoneFrame, new Vector3(0, 10, 0), new Quaternion(0, 0, 0, 0) );
        }
    }
}
