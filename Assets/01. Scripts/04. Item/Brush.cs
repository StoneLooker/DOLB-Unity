using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public ItemStat brushStat;

    public void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = brushStat.Image;
    }

    private void OnEnable()
    {
        if(GameManager.Input.keyAction != null) GameManager.Input.keyAction -= PutDownBrush;
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

    private void OnCollisionStay(Collision collision)
    {
        if(Input.GetMouseButton(0))
        {
            if (collision.gameObject.tag.Equals("Moss"))
            {
                this.Wash();
                collision.gameObject.SetActive(false);
            }
        }        
    }
}