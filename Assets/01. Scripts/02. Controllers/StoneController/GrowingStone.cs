using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingStone : MonoBehaviour
{
    [Serialize]
    StoneController controller;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Stone.growingStone == null)
        {
            Debug.Log("No Stone Choosed");
            this.gameObject.SetActive(false);
        }
        if(GameManager.Stone.growingStone != null)
        {
            switch (GameManager.Stone.growingStone.stoneStat.StoneType)
            {
                case STONE_TYPE.LimeStone:
                    this.AddComponent<LimeStoneController>();
                    controller = this.GetComponent<LimeStoneController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                case STONE_TYPE.Granite:
                    this.AddComponent<GraniteController>();
                    controller = this.GetComponent<GraniteController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                default:
                    Debug.Log(GameManager.Stone.growingStone);
                    break;
            }
        }
        else
        {
        }
    }
}
