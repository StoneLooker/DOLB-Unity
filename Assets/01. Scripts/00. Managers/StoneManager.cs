using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class StoneManager : MonoBehaviour 
{
    public Dictionary<StateType, IStoneState> stateInfo = new();
    public Dictionary<string, Stone> stoneInfo = new();
    [SerializeField] public LimeStone limeStoneData;

    public List<Stone> stones = new(); 
    public List<Stone> collectingBook = new();
    public Stone growingStone;

    public int stoneNum = 0;

    public Stone MakeStone(STONE_TYPE stoneType)
    {
        if(stoneType.Equals(STONE_TYPE.LimeStone))
        {
            return new LimeStone(limeStoneData.GetScientificName(), "Empty");
        }
        return null;
    }

    public void Awake()
    {
        growingStone = null;

        stateInfo.Clear();
        stateInfo.Add(StateType.NotPet, new NotPet());
        stateInfo.Add(StateType.Normal, new Normal());

        stoneInfo.Clear();
        stoneInfo.Add("LimeStone", new LimeStone("LimeStone", "Information"));

        collectingBook.Clear();
    }

    public void OnUpdate()
    {

    }

    public void AddStone(Stone stone)
    {
        stones.Add(stone);
    }

    public void DeleteStone()
    {

    }

    public void WhenPlayerDecideGrowingNewStoneInBulgama(Stone stone)
    {
        growingStone = stone;
    }

    public void GrowingFinished(Stone stone)
    {
        collectingBook.Add(stone);
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
    int id;
    public StoneStat stoneStat;
    public string scientificName;
    public string nickName;
    public IStoneState state;

    public Stone(string scientificName, string nickName)
    {
        this.scientificName = scientificName;
        this.nickName = nickName;
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

    public string GetScientificName()
    {
        return this.scientificName;
    }

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