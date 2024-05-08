using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class StoneManager
{
    public Dictionary<StateType, IStoneState> stateInfo = new();
    public Dictionary<string, Stone> stoneInfo = new();

    public List<Stone> allStoneHaveGrown = new();
    public Stone growingStone;

    public void OnAwake()
    {
        stateInfo.Clear();
        stateInfo.Add(StateType.Normal, new Normal());

        stoneInfo.Clear();
        stoneInfo.Add("LimeStone", new LimeStone("LimeStone", "Information", 0, stateInfo[StateType.Normal]));

        allStoneHaveGrown.Clear();
    }

    public void OnUpdate()
    {

    }

    public void WhenPlayerDecideGrowingNewStoneInBulgama(Stone stone)
    {
        growingStone = stone;
    }

    public void GrowingFinished(Stone stone)
    {
        allStoneHaveGrown.Add(stone);
    }

    public void StoneDie(Stone stone)
    {
        growingStone = null;
    }
}

public abstract class Stone
{
    int id;
    float loveGage;
    string scientificName;
    string nickName;
    public IStoneState state;
    Transform transform;

    public Stone(string scientificName, string nickName, int id, IStoneState state)
    {
        this.scientificName = scientificName;
        this.nickName = nickName;
        this.loveGage = 0F;
        this.id = id;
        this.state = state;
    }

    public void ChangeState(IStoneState _state)
    {
        this.state.ExitState();
        this.state = _state;
        this.state.EnterState();
    }

    public void SetLoveGage(float loveGage)
    {
        this.loveGage += loveGage;
    } 
}

public enum StateType
{
    Normal
}

public interface IStoneState
{
    public abstract void EnterState();
    public abstract void ExitState();
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