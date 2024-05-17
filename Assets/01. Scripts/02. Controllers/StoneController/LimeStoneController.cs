using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimeStoneController : StoneController
{
    LimeStone stone;

    new void Start()
    {
        base.Start();
        stone = (LimeStone)GameManager.Stone.stoneInfo["LimeStone"];
        Debug.Log("hi");
    }

    public void Set(string nN)
    {
        stone = new LimeStone("LimeStone", nN, GameManager.Stone.stateInfo[StateType.Normal]);
    }

    new void OnMouseDown()
    {
        base.OnMouseDown();
        stone.UpdateLoveGage(20F);
    }

    public void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Danger"))
        {
            stone.UpdateHP(-10F);
        }
    }
}

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

    public LimeStone(string scientificName, string nickName, IStoneState state) : base(scientificName, nickName, state)
    {
    }
}