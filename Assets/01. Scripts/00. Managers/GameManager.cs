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
    [SerializeField] private UIManager _ui;

    public static InputManager Input { get { return Instance._input; } }
    public static StoneManager Stone { get { return Instance._stone; } }    
    public static CameraManager Camera { get { return Instance._camera; } }    
    public static ButtonManager Button { get { return Instance._button; } }
    public static UIManager UI { get { return Instance._ui; } }

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
