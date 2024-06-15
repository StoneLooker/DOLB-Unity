using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    [SerializeField] GameObject Item;
    [SerializeField] ItemStat brushStat;
    [SerializeField] ItemStat towelStat;
    [SerializeField] TMP_Text brushCountText;
    [SerializeField] TMP_Text towelCountText;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Sauna")
            UpdateBrushCount();
    }

    public void PickBrush()
    {
        GameManager.Item.ConsumeItem(ITEM_TYPE.Brush);
        Item.GetComponent<Item>().itemStat = brushStat;
        Item.SetActive(true);
    }

    public void PickTowel()
    {
        if (GameManager.Item.GetItemNum(ITEM_TYPE.Towel) > 0)
        {
            GameManager.Item.ConsumeItem(ITEM_TYPE.Towel);
            Item.GetComponent<Item>().itemStat = towelStat;
            Item.SetActive(true);
        }
        else Debug.Log("No Towel");
    }

    public void UpdateBrushCount()
    {
        int brushCount = GameManager.Item.GetItemNum(ITEM_TYPE.Brush);
        int towelCount = GameManager.Item.GetItemNum(ITEM_TYPE.Towel);
        brushCountText.text = brushCount.ToString();
        towelCountText.text = towelCount.ToString();
    }
}
