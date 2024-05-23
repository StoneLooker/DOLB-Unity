using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Input.keyAction -= PutDownBrush;
        GameManager.Input.keyAction += PutDownBrush;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.Input.mousePosUnity);
        transform.position = GameManager.Input.mousePosUnity;
    }

    public void PutDownBrush()
    {
        if(Input.GetMouseButtonDown(1)) 
        { 
            gameObject.SetActive(false);
        }
    }

    void Wash()
    {
        GameManager.Stone.growingStone.UpdateLoveGage(10F);
    }
}
