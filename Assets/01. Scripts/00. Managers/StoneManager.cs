using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class StoneManager
{
    public Dictionary<StateType, IStoneState> stateInfo = new();
    public Dictionary<string, Stone> stoneInfo = new();

    public List<Stone> stones = new(); 
    public List<Stone> allStoneHaveGrown = new();
    public Stone growingStone;

    public int stoneNum = 0;

    public void OnAwake()
    {
        stateInfo.Clear();
        stateInfo.Add(StateType.NotPet, new NotPet());
        stateInfo.Add(StateType.Normal, new Normal());

        stoneInfo.Clear();
        stoneInfo.Add("LimeStone", new LimeStone("LimeStone", "Information", stateInfo[StateType.NotPet]));

        allStoneHaveGrown.Clear();
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
        allStoneHaveGrown.Add(stone);
    }

    public void StoneDie(Stone stone)
    {
        growingStone = null;
    }
}

public abstract class Stone
{
    int id = GameManager.Stone.stoneNum++;
    string scientificName;
    string nickName;
    public IStoneState state;
    Transform transform;

    public Stone(string scientificName, string nickName, IStoneState state)
    {
        this.scientificName = scientificName;
        this.nickName = nickName;
        this.state = state;
    }

    public void ChangeState(IStoneState _state)
    {
        this.state.ExitState();
        this.state = _state;
        this.state.EnterState();
    }
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