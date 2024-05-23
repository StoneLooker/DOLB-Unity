using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] GameObject brush;

    public void PickBrush()
    {
        if (GameManager.Item.GetItemNum(ITEM_TYPE.Brush) > 0)
        {
            brush.SetActive(true);
            GameManager.Item.ConsumeItem(ITEM_TYPE.Brush);
        }
        else Debug.Log("No Brush");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
