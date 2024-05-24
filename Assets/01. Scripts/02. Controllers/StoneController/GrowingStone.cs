using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingStone : MonoBehaviour
{
    StoneController controller;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Stone.growingStone.GetScientificName().Equals("LimeStone"))
        {
            controller = new LimeStoneController();
        }
        else
        {
            controller = new();
        }
        controller.Start();
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
