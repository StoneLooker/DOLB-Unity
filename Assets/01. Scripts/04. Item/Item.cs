using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemStat brushStat;

    public void Start()
    {
        this.AddComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = brushStat.Image;
        GameManager.Input.keyAction += PutDownBrush;
    }

    private void OnEnable()
    {
        GameManager.Input.keyAction -= PutDownBrush;
        GameManager.Input.keyAction += PutDownBrush;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Input.mousePosUnity);
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

    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Moss cleand");
        if (Input.GetMouseButton(0))
        {
            if (other.gameObject.tag.Equals("Moss"))
            {
                this.Wash();
                Debug.Log("Moss cleand");
                other.gameObject.SetActive(false);
            }
        }        
    }
}
