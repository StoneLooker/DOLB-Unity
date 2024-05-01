using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private InputManager _input;
    private StoneManager _stone;
    [SerializeField] private CameraManager _camera;
    [SerializeField] private ButtonManager _button;

    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }    
    public static CameraManager Camera { get { return Instance._camera; } }    
    public static ButtonManager Button { get { return Instance._button; } }

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
    }

    public class BoolCondition
    {
        
    }

    public BoolCondition boolCondition {  get; private set; }


    private void Start()
    {
        _stone.OnStart();
    }

    void Update()
    {
        _input.OnUpdate();
    }
}
