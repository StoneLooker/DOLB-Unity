using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class StoneManager : MonoBehaviour 
{
    [SerializeField] public LimeStone limeStoneData;
    [SerializeField] public Granite GraniteData;

    [SerializeField] public Stone growingStone;

    public List<Stone> collectingBook = new();

    public int stoneNum = 0;

    public Stone MakeStone(STONE_TYPE stoneType)
    {
        if(stoneType.Equals(STONE_TYPE.LimeStone))
        {
            return new LimeStone("LimeStone", limeStoneData.HP, limeStoneData.loveGage, limeStoneData.nextEvolutionPercentage, limeStoneData.stoneInfo) ;
        }
        else if (stoneType.Equals(STONE_TYPE.Granite))
        {
            return new Granite("Granite", GraniteData.HP, GraniteData.loveGage, GraniteData.nextEvolutionPercentage, GraniteData.stoneInfo);
        }
        return null;
    }

    public void Awake()
    {

        /*stateInfo.Clear();
        stateInfo.Add(StateType.NotPet, new NotPet());
        stateInfo.Add(StateType.Normal, new Normal());

        stoneInfo.Clear();
        stoneInfo.Add("LimeStone", new LimeStone("LimeStone", "Information"));*/

        collectingBook.Clear();
    }

    public void OnUpdate()
    {

    }

    /*public void AddStone(Stone stone)
    {
        stones.Add(stone);
    }*/

    public void DeleteStone()
    {

    }

    public void WhenPlayerDecideGrowingNewStoneInBulgama(Stone stone)
    {
        growingStone = stone;
    }

    public void GrowingFinished()
    {
        collectingBook.Add(growingStone);
        GameManager.Instance._book.AddStone(growingStone.id, growingStone.nickName);
        growingStone = null;
    }

    public void StoneDie(Stone stone)
    {
        growingStone = null;
    }
}

public enum STONE_TYPE
{
    LimeStone, Granite
}
[Serializable]
public abstract class Stone
{
    public int id;
    public StoneStat stoneStat;
    public string nickName;
    public IStoneState state;

    public float HP = 100F;
    public float loveGage = 0F;
    public float nextEvolutionPercentage = 100F;

    public string stoneInfo;

    public Stone(string nickName, float HP, float loveGage, float evolution, string stoneinfo)
    {
        this.nickName = nickName;
        this.HP = HP;
        this.loveGage = loveGage;
        this.nextEvolutionPercentage = evolution;
        this.stoneInfo = stoneinfo;
    }

    public void SetNickName(String nickName)
    {
        this.nickName = nickName;   
    }

    public void ChangeState(IStoneState _state)
    {
        this.state.ExitState();
        this.state = _state;
        this.state.EnterState();
    }

    /*public string GetScientificName()
    {
        return this.scientificName;
    }*/

    public abstract void Washing();
    public abstract void UpdateHP(float HP);
    public abstract void UpdateLoveGage(float Gage);
    public abstract void CheckEvolution();
}

public enum StateType
{
    NotPet, Normal, Dead, Forgotten
}

public interface IStoneState
{
    public abstract void EnterState();
    public abstract void ExitState();
}

public class NotPet : IStoneState
{
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }
}

public class Normal: IStoneState
{
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }
}