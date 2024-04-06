using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager
{
    public Stone[] StoneGroup { get; private set; }

    public void OnStart()
    {
        StoneGroup[0] = new Stone("test", 0, new Normal());
    }

    public void OnUpdate()
    {

    }
}

/// <summary>
/// 'class Stone' have all information about stone
/// </summary>

public class Stone
{
    string name;
    int id;
    StoneState state;

    Sprite sprite;

    public Stone(string name, int id, Normal state)
    {
        this.name = name;
        this.id = id;
        this.state = state;
    }
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