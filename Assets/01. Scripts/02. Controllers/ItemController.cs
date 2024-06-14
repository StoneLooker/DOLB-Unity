using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    [SerializeField] GameObject brush;
    [SerializeField] TMP_Text brushCountText;
    [SerializeField] TMP_Text towelCountText;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Sauna")
            UpdateBrushCount();
    }

    public void PickBrush()
    {
        if (GameManager.Item.GetItemNum(ITEM_TYPE.Brush) > 0)
        {
            brush.SetActive(true);
            GameManager.Item.ConsumeItem(ITEM_TYPE.Brush);
        }
        else Debug.Log("No Brush");
    }
    
    public void UpdateBrushCount()
    {
        int brushCount = GameManager.Item.GetItemNum(ITEM_TYPE.Brush);
        int towelCount = GameManager.Item.GetItemNum(ITEM_TYPE.Towel);
        brushCountText.text = brushCount.ToString();
        towelCountText.text = towelCount.ToString();
    }
}
