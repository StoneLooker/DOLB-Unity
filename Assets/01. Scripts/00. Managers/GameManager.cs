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
    public CollectingBookManager _book;
    public SearchingUserManager _search;

    public string id;
    public string nickname;
    private bool minigameEnter = true;

    public bool MinigameEnter { get { return minigameEnter; } private set { minigameEnter = value; }}

    private InputManager _input;
    private ItemManager _item;
    [SerializeField]
    private StoneManager _stone;

    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }
    public static ItemManager Item { get { return Instance._item; } }

    public MAP_TYPE nowMap { get; set; }

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

        SceneManager.sceneLoaded += OnSceneLoaded;

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
        if (!string.IsNullOrEmpty(id))
        {
            DataManager.Instance.LoadGameData(id);
            ApplyGameData(DataManager.Instance.data);
        }
        _item.SetItemInventory(DataManager.Instance.data.GetItemInventory());
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

    public IEnumerator MinigameReEntryCooldown()
    {
        minigameEnter = false;
        yield return new WaitForSeconds(10f);
        minigameEnter = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Sauna")
            StartCoroutine(MinigameReEntryCooldown());
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void OnApplicationQuit()
    {
        if (!string.IsNullOrEmpty(id))
        {
            GameData data = new GameData();
            data.playerId = id;
            data.SetItemInventory(_item.GetItemInventory());
            DataManager.Instance.data = data;
            DataManager.Instance.SaveGameData(id);
        }
    }

    public void Save()
    {
        if (!string.IsNullOrEmpty(id))
        {
            GameData data = new GameData();
            data.playerId = id;
            data.SetItemInventory(_item.GetItemInventory());
            DataManager.Instance.data = data;
            DataManager.Instance.SaveGameData(id);
        }
    }

    public void ApplyGameData(GameData data)
    {
        Dictionary<ITEM_TYPE, int> inventory = data.GetItemInventory();

        if (!inventory.ContainsKey(ITEM_TYPE.Brush))
            inventory[ITEM_TYPE.Brush] = 1;
        if (!inventory.ContainsKey(ITEM_TYPE.Towel))
            inventory[ITEM_TYPE.Towel] = 1;
        
        _item.SetItemInventory(inventory);
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


