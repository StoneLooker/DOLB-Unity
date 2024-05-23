using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    #region Init
    public static GameManager Instance { get; private set; }

    public ControllerManager _controller;
    public MinigameManager _minigame;

    private InputManager _input;
    private StoneManager _stone;
    private ItemManager _item;

    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }
    public static ItemManager Item { get { return Instance._item; } }

    public String sceneName { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _input = new InputManager();
        _stone = new StoneManager();
        _item = new ItemManager();

        #endregion
        _stone.OnAwake();
        sceneName = "Sauna";
    }

    void Start()
    {
    }

    void Update()
    {
        _input.OnUpdate();
    }

    public void ChangeScene(String sceneName)
    {
        this.sceneName = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}

public enum MAP_TYPE
{
    MainTitle, Sauna, Bulgama
}

public enum STATE_TYPE
{
    Start, Normal, Speed, Pause, Exit
}

public interface IGameState
{
    public abstract void EnterState(); // Do setting in event
    public abstract void ExitState(); // Do reset in same event
}


