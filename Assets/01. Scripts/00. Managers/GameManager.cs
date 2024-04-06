using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private InputManager _input;
    private StoneManager _stone;

    public static InputManager Input { get { return Instance._input;  } }
    public static StoneManager Stone { get { return Instance._stone; } }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _stone.OnStart();   
    }

    void Update()
    {
        _input.OnUpdate();
    }
}
