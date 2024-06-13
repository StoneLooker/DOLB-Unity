using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraniteController : StoneController
{
    // Use Awake or Start for initialization, not constructors
    private void Awake()
    {
        // Ensure the base class initialization happens first
        base.InitializeStone();

        // Initialize the stone
        stone = (Granite)GameManager.Stone.MakeStone(STONE_TYPE.Granite);

        // Check if SpriteRenderer component is attached, add if missing
        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }

        // Set the sprite image
        GetComponent<SpriteRenderer>().sprite = GameManager.Stone.GraniteData.stoneStat.Image;
    }

    private void Set(string nN)
    {
        stone.SetNickName(nN);
        GameManager.Stone.growingStone = stone;
    }

    private new void OnMouseDown()
    {
        base.MouseDown();
        if (GameManager.Instance.nowMap == MAP_TYPE.Sauna)
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
public class Granite : Stone
{
    public Granite(string nickName, float maxHp, float maxLoveGage, float maxEvolutionGage, string stoneinfo) : base(nickName, maxHp, maxLoveGage, maxEvolutionGage, stoneinfo)
    {
        this.stoneStat = GameManager.Stone.GraniteData.stoneStat;
    }

    public override void Washing()
    {
        Debug.Log("Washed");
    }

    public override void UpdateHP(float HP)
    {
        this.HP += HP;
        if (HP < 0) UpdateLoveGage(HP);
        CheckEvolution();
    }

    public override void UpdateLoveGage(float loveGage)
    {
        this.loveGage += loveGage;
        this.evolutionGage += loveGage;

        if (this.loveGage > maxLoveGage) this.loveGage = maxLoveGage;
        if (this.evolutionGage > maxEvolutionGage) evolutionGage = maxEvolutionGage;
        Debug.Log(this.evolutionGage);
        if (GameManager.Instance._controller._ui.LoveGageSlider != null)
            GameManager.Instance._controller._ui.CallUpdateSlider(SLIDER_TYPE.LoveGage, this.loveGage);
        if (GameManager.Instance._controller._ui.EvolutionGageSlider != null)
            GameManager.Instance._controller._ui.CallUpdateSlider(SLIDER_TYPE.Evolution, this.evolutionGage);
        else Debug.Log("No Hp Slider in GraniteController");
        CheckEvolution();
    }

    public override void CheckEvolution()
    {
        if (evolutionGage == maxEvolutionGage)
        {
            if (loveGage == maxLoveGage)
            {
                Debug.Log("Evolution complete");
                GameManager.Stone.GrowingFinished();
                GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
            }
        }
    }


}
