using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

//Singleton GameManager class to handle game state and transitions
public class GameManager : MonoBehaviour
{
    #region Init

    //Singleton instance
    public static GameManager Instance { get; private set; }

    //References to various managers in the game
    public ControllerManager _controller;
    public MinigameManager _minigame;
    public LogInManager _logIn;
    public CollectingBookManager _book;
    public SearchingUserManager _search;
    
    //Player information
    public string id;
    public string nickname;
    private bool minigameEnter = true;

    public bool MinigameEnter { get { return minigameEnter; } private set { minigameEnter = value; }}

    //Input, item and stone managers
    private InputManager _input;
    private ItemManager _item;
    [SerializeField]
    private StoneManager _stone;

    //Public access to input,stone and item managers
    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }
    public static ItemManager Item { get { return Instance._item; } }

    //Current map type
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

        //Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        #endregion
        /*_stone.OnAwake();*/

        //Determine the current map based on the active scene
        if(SceneManager.GetActiveScene().name.Equals("MainTitle")) nowMap = MAP_TYPE.MainTitle;
        else if (SceneManager.GetActiveScene().name.Equals("Sauna")) nowMap = MAP_TYPE.Sauna;
        else if (SceneManager.GetActiveScene().name.Equals("Tub")) nowMap = MAP_TYPE.Tub;
        else if (SceneManager.GetActiveScene().name.Equals("Bulgama")) nowMap = MAP_TYPE.Bulgama;
        else if (SceneManager.GetActiveScene().name.Equals("MiniGame")) nowMap = MAP_TYPE.MiniGame;
        else if (SceneManager.GetActiveScene().name.Equals("CollectingBook")) nowMap = MAP_TYPE.CollectingBook;
    }

    void Start()
    {
        //Initialize the item manager and load game data if the player ID is set
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

    //Change the current map and load the corresponding scene
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
        else if (nowMap.Equals(MAP_TYPE.HotSoup))
        {
            SceneManager.LoadScene("HotSoup");
        }
        else if (nowMap.Equals(MAP_TYPE.ColdSoup))
        {
            SceneManager.LoadScene("ColdSoup");
        }
    }

    //Coroutine to handle cooldown for re-entering the minigame
    public IEnumerator MinigameReEntryCooldown()
    {
        minigameEnter = false;
        yield return new WaitForSeconds(10f);
        minigameEnter = true;
    }

    //Called when a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Sauna")
            StartCoroutine(MinigameReEntryCooldown());
    }

    //Method to quit the game
    public void GameQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    //Called when the application quits
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

    //Save the current game data
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

    //Apply loaded game data to the current game state
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

//Enum to define different map types
public enum MAP_TYPE
{
    MainTitle, Sauna, Bulgama, Tub, MiniGame, CollectingBook, HotSoup, ColdSoup
}

//Enum to define different game states
public enum STATE_TYPE
{
    Start, Normal, Speed, Pause, Exit
}

//Interface for defining game state behavior
public interface IGameState
{
    public abstract void EnterState(); // Do setting in event
    public abstract void ExitState(); // Do reset in same event
}