using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraniteController : StoneController
{
    [SerializeField]
    public new Granite stone;

    public GraniteController(Stone stone)
    {
        this.stone = (Granite)stone;
    }
    
    new void Start()
    {
        if(this.GetComponent<SpriteRenderer>() == null) this.AddComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = GameManager.Stone.GraniteData.stoneStat.Image;
        stone = (Granite)GameManager.Stone.MakeStone(STONE_TYPE.Granite);
        base.Start();
    }

    void Set(string nN)
    {
        stone.SetNickName(nN);
        GameManager.Stone.growingStone = stone;
    }

    new void OnMouseDown()
    {
        base.MouseDown();
        stone.UpdateLoveGage(20F);
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Danger"))
        {
            stone.UpdateHP(-10F);
        }
    }

    new void OnMouseUp()
    {
        base.MouseUp();
        if(GameManager.Instance.nowMap.Equals(MAP_TYPE.Bulgama))
        {
            this.Set("ss?");
            Debug.Log("Choose Stone!");
            this.gameObject.SetActive(false);
            GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
        }
    }
}

[Serializable]
public class Granite : Stone
{

    public override void Washing()
    {
        Debug.Log("Washed");
    }

    public override void UpdateHP(float HP)
    {
        this.HP += HP;
        this.nextEvolutionPercentage -= HP;
        CheckEvolution();
    }

    public override void UpdateLoveGage(float loveGage)
    {
        this.loveGage += loveGage;
        this.nextEvolutionPercentage -= loveGage;
        Debug.Log(this.nextEvolutionPercentage);
        CheckEvolution();
    }

    public override void CheckEvolution()
    {
        if(nextEvolutionPercentage <= 0F)
        {
            Debug.Log("Evloution complete");
            GameManager.Stone.GrowingFinished();
            Debug.Log(GameManager.Stone.collectingBook);
        }
    }

    public Granite(string nickName) : base(nickName)
    {
        this.stoneStat = GameManager.Stone.GraniteData.stoneStat;
        HP = 100F;
        loveGage = 0F;
        nextEvolutionPercentage = 100F;
    }
}