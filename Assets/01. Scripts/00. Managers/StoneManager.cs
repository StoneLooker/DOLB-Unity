using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class StoneManager : MonoBehaviour
{
    public Dictionary<StateType, StoneState> stateInfo = new();
    public List<Sprite> spriteInfo = new();
    public Dictionary<string, Stone> stoneInfo = new();

    public void OnStart()
    {
        stateInfo.Clear();
        stateInfo.Add(StateType.Normal, new Normal());

        stoneInfo.Clear();
        stoneInfo.Add("LimeStone", new LimeStone("LimeStone", "Original", 0, stateInfo[StateType.Normal], spriteInfo[0]));
    }

    public void OnUpdate()
    {

    }
}

/// <summary>
/// Factory Desing Pattern..
/// </summary>

public abstract class Stone
{
    int id;
    string scientificName;
    string nickName;
    public StoneState state;
    Sprite sprite;

    public Stone(string scientificName, string nickName, int id, StoneState state, Sprite sprite)
    {
        this.scientificName = scientificName;
        this.nickName = nickName;
        this.id = id;
        this.state = state;
        this.sprite = sprite;
    }

    public void ChangeState(StateType stateType)
    {
        this.state.ExitState();
        //this.state = _state;

        this.state.EnterState();
    }

    public void Drag()
    {

    }

    public void Touch()
    {

    }
}

public class LimeStone : Stone
{
    public LimeStone(string scientificName, string nickName, int id, StoneState state, Sprite sprite) : base(scientificName, nickName, id, state, sprite)
    {
    }
}

public enum StateType
{
    Normal
}

public interface StoneState
{
    public abstract void EnterState();
    public abstract void ExitState();
}

public class Normal: StoneState
{
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }
}