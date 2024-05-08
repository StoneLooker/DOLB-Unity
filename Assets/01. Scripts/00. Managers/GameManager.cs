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

    public Game g { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        g = new();
        _input = new InputManager();
        _stone = new StoneManager();

    #endregion
        _stone.OnAwake();
        g.OnAwake();
    }

    void Start()
    {
        _ui.OnStart();

        g.stateEvent.Bulgama.AddListener(() => _camera.MoveMainCamera(g.variable.bulgamaLocation));
        g.stateEvent.Sauna.AddListener(() => _camera.MoveMainCamera(g.variable.saunaLocation));
        g.stateEvent.CollectingBook.AddListener(() => _ui.SwitchUI(_ui.collectingBook));

        _button.enableCollectingBook.onClick.AddListener(() => g.ChangeState(Game.GameStateType.CollectingBook));
        _button.moveToSauna.onClick.AddListener(() => g.ChangeState(Game.GameStateType.Sauna));
        _button.moveToBulgama.onClick.AddListener(() => g.ChangeState(Game.GameStateType.Bulgama));
    }

    void Update()
    {
        _input.OnUpdate();
    }    
}

public class Game
{
    public Dictionary<GameStateType, IGameState> stateInfo;
    public IGameState nowState {  get; private set; }
    public Variable variable { get; private set; }
    public StateEvent stateEvent {  get; private set; }
    public BoolCondition boolCondition { get; private set; }
    
    public void OnAwake()
    {
        stateInfo = new();
        stateInfo.Clear();
        stateInfo.Add(GameStateType.Sauna, new Sauna());
        stateInfo.Add(GameStateType.Bulgama, new Bulgama());
        stateInfo.Add(GameStateType.CollectingBook, new CollectingBook());

        nowState = stateInfo[GameStateType.Sauna];

        variable = new Variable();
        stateEvent = new StateEvent();
        boolCondition = new BoolCondition();
    }

    public void ChangeState(GameStateType _state)
    {
        this.nowState.ExitState();
        this.nowState = stateInfo[_state];
        this.nowState.EnterState();
    }

    #region container
    public class Variable
    {
        public Vector3 saunaLocation = new Vector3(0, 0, -10);
        public Vector3 bulgamaLocation = new Vector3(-20, 0, -10);
    }

    public class StateEvent
    {
        public UnityEvent Sauna = new();
        public UnityEvent Stopped = new();
        public UnityEvent Bulgama = new();
        public UnityEvent CollectingBook = new();
    }

    public class BoolCondition
    {

    }

    public enum GameStateType
    {
        Sauna, Stopped, TimeSpeed, Setting, Bulgama, CollectingBook
    }

    public interface IGameState
    { 
        public abstract void EnterState(); // Do setting in event
        public abstract void ExitState(); // Do reset in same event
    }

    public class Sauna : IGameState
    {
        public void EnterState()
        {
            GameManager.Instance.g.stateEvent.Sauna?.Invoke();
        }

        public void ExitState()
        {
            GameManager.Instance.g.stateEvent.Sauna?.Invoke();
        }
    }

    public class Bulgama : IGameState
    {
        public void EnterState()
        {
            GameManager.Instance.g.stateEvent.Bulgama?.Invoke();
        }

        public void ExitState()
        {
            GameManager.Instance.g.stateEvent.Bulgama?.Invoke();
        }
    }

    public class CollectingBook : IGameState
    {
        public void EnterState()
        {
            GameManager.Instance.g.stateEvent.CollectingBook?.Invoke();
        }

        public void ExitState()
        {
            GameManager.Instance.g.stateEvent.CollectingBook?.Invoke();
        }
    }
    #endregion
}


