using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimeStoneController : StoneController
{
    public new LimeStone stone;

    public LimeStoneController(Stone stone)
    {
        this.stone = (LimeStone)stone;
    }
    
    new void Start()
    {
        if(this.GetComponent<SpriteRenderer>() == null) this.AddComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = GameManager.Stone.limeStoneData.stoneStat.Image;
        stone = (LimeStone)GameManager.Stone.MakeStone(STONE_TYPE.LimeStone);
        base.Start();
    }

    new void Set(string nN)
    {
        stone.SetNickName(nN);
        GameManager.Stone.growingStone = stone;
    }

    new void OnMouseDown()
    {
        base.OnMouseDown();
        stone.UpdateLoveGage(20F);
    }

    new void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Danger"))
        {
            stone.UpdateHP(-10F);
        }
    }

    new void OnMouseUp()
    {
        base.OnMouseUp();
        if(GameManager.Instance.nowMap.Equals(MAP_TYPE.Bulgama))
        {
            this.Set("ss?");
            Debug.Log("Choose Stone!");
        }
    }
}

[Serializable]
public class LimeStone : Stone
{
    public float HP = 100F;
    public float loveGage = 0F;
    public float nextEvolutionPercentage = 100F;

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
        }
    }

    public LimeStone(string scientificName, string nickName) : base(scientificName, nickName)
    {
        HP = 100F;
        loveGage = 0F;
        nextEvolutionPercentage = 100F;
    }
}