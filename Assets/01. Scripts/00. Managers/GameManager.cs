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
    public LogInManager _logIn;
    public string id;

    private InputManager _input;
    private ItemManager _item;
    [SerializeField]
    private StoneManager _stone;

    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }
    public static ItemManager Item { get { return Instance._item; } }

    public MAP_TYPE nowMap { get; private set; }

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
        _item = new ItemManager();

        #endregion
        /*_stone.OnAwake();*/
        if(SceneManager.GetActiveScene().name.Equals("MainTitle")) nowMap = MAP_TYPE.MainTitle;
        else if (SceneManager.GetActiveScene().name.Equals("Sauna")) nowMap = MAP_TYPE.Sauna;
        else if (SceneManager.GetActiveScene().name.Equals("Tub")) nowMap = MAP_TYPE.Tub;
        else if (SceneManager.GetActiveScene().name.Equals("Bulgama")) nowMap = MAP_TYPE.Bulgama;
        else if (SceneManager.GetActiveScene().name.Equals("MiniGame")) nowMap = MAP_TYPE.MiniGame;
        else if (SceneManager.GetActiveScene().name.Equals("CollectingBook")) nowMap = MAP_TYPE.CollectingBook;
    }

    void Start()
    {
        _item.OnStart();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    public void ChangeMap(MAP_TYPE map)
    {
        this.nowMap = map;
        if (nowMap.Equals(MAP_TYPE.MainTitle))
        {
            SceneManager.LoadScene("MainTitle");
        }
        else if (nowMap.Equals(MAP_TYPE.Sauna))
        {
            SceneManager.LoadScene("Sauna");
        }
        else if (nowMap.Equals(MAP_TYPE.Tub))
        {
            SceneManager.LoadScene("Tub");
        }
        else if(nowMap.Equals(MAP_TYPE.Bulgama))
        {
            SceneManager.LoadScene("Bulgama");
        }
        else if (nowMap.Equals(MAP_TYPE.MiniGame))
        {
            SceneManager.LoadScene("MiniGame");
        }
        else if (nowMap.Equals(MAP_TYPE.CollectingBook))
        {
            SceneManager.LoadScene("CollectingBook");
        }
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

public enum MAP_TYPE
{
    MainTitle, Sauna, Bulgama, Tub, MiniGame, CollectingBook
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


