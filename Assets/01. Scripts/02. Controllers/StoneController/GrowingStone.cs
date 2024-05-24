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
            if (GameManager.Stone.growingStone.stoneStat.StoneType.Equals(STONE_TYPE.LimeStone))
            {
                this.AddComponent<LimeStoneController>();
                controller = this.GetComponent<LimeStoneController>();
                controller.stone = GameManager.Stone.growingStone;
            }
            else
            {
                Debug.Log(GameManager.Stone.growingStone);
            }
            controller.Start();
        }
        else
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        controller.Update();
    }

    private void OnMouseDown()
    {
        controller.OnMouseDown();
    }

    private void OnMouseDrag()
    {
        controller.OnMouseDrag();
    }

    private void OnMouseUp()
    {
        controller.OnMouseUp();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.OnTriggerStay2D(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        controller.OnCollisionStay(collision);
    }

    public void Set(string nN)
    {
        controller.Set(nN);
    }
}
