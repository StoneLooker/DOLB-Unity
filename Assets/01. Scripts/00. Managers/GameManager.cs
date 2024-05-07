using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Init
    public static GameManager Instance { get; private set; }

    private InputManager _input;
    private StoneManager _stone;
    private ItemManager _item;
    [SerializeField] private CameraManager _camera;
    [SerializeField] private ButtonManager _button;
    [SerializeField] private UIManager _ui;

    public static InputManager Input { get { return Instance._input; } }
    public static CameraManager Camera { get { return Instance._camera; } }    
    public static ButtonManager Button { get { return Instance._button; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static StoneManager Stone { get { return Instance._stone; } }
    public static ItemManager Item { get { return Instance._item; } }

    public Game G { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        G = new();
        _input = new InputManager();
        _stone = new StoneManager();

    #endregion
        _stone.OnAwake();
    }

    private void Start()
    {
        _ui.OnStart();
        _button.enableSetting.onClick.AddListener(() => _ui.SwitchUI(_ui.main));
        _button.enableCollectingBook.onClick.AddListener(() => _ui.SwitchUI(_ui.collectingBook));
        _button.moveToSauna.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(0, 0, -10)));
        _button.moveToBulgama.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(-20, 0, -10)));
    }

    void Update()
    {
        _input.OnUpdate();
    }

    
}

public class Game
{
    public IGameState State {  get; private set; }
    public StateEvent Event {  get; private set; }
    public BoolCondition Condition { get; private set; }

    public void ChangeState(IGameState _state)
    {
        this.State.ExitState();
        this.State = _state;
        this.State.EnterState();
    }

    public class StateEvent
    {
        public Action Playing;
        public Action Stopped;
        public Action Bulgama;
    }

    public class BoolCondition
    {

    }

    public enum GameStateType
    {
        Playing, Stopped, TimeSpeed, Setting, Bulgama, CollectingBook
    }

    public interface IGameState
    {
        public abstract void EnterState();
        public abstract void ExitState();
    }

    public class Playing : IGameState
    {
        public void EnterState()
        {
          
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Bulgama : IGameState
    {
        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }
    }
}


