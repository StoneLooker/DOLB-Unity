using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimeStoneController : StoneController
{
    // Use Awake or Start for initialization, not constructors
    private void Awake()
    {
        // Ensure the base class initialization happens first
        base.InitializeStone();

        // Initialize the stone
        stone = (LimeStone)GameManager.Stone.MakeStone(STONE_TYPE.LimeStone);

        // Check if SpriteRenderer component is attached, add if missing
        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }

        // Set the sprite image
        GetComponent<SpriteRenderer>().sprite = GameManager.Stone.limeStoneData.stoneStat.Image;
    }

    private void Set(string nN)
    {
        stone.SetNickName(nN);
        GameManager.Stone.growingStone = stone;
    }

    private new void OnMouseDown()
    {
        base.MouseDown();
        if(GameManager.Instance.nowMap == MAP_TYPE.Sauna)
        {
            stone.UpdateLoveGage(20F);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Danger"))
        {
            stone.UpdateHP(-10F);
        }
    }

    private new void OnMouseUp()
    {
        base.MouseUp();
        if (GameManager.Instance.nowMap.Equals(MAP_TYPE.Bulgama))
        {
            GameManager.Stone.WhenPlayerDecideGrowingNewStoneInBulgama(stone);
            Debug.Log("Choose Stone!");
            gameObject.SetActive(false);
            GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
        }
    }

    private new void Update()
    {
        base.Update();
        GetInfo();
    }
}

[Serializable]
public class LimeStone : Stone
{
    public LimeStone(string nickName, float HP, float loveGage, float evolution, string stoneinfo) : base(nickName, HP, loveGage, evolution, stoneinfo)
    {
        this.stoneStat = GameManager.Stone.limeStoneData.stoneStat;
    }

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
        if(GameManager.Instance._controller._ui != null) GameManager.Instance._controller._ui.UpdatHPSlider(this.loveGage);
        CheckEvolution();
    }

    public override void CheckEvolution()
    {
        if (nextEvolutionPercentage <= 0F)
        {
            Debug.Log("Evolution complete");
            GameManager.Stone.GrowingFinished();
            Debug.Log(GameManager.Stone.collectingBook);
        }
    }

   
}
