using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] public CameraController _camera;
    [SerializeField] public ButtonController _button;
    [SerializeField] public UIController _ui;   

    void Start()
    {
        GameManager.Instance._controller = this;
        SetController(GameManager.Instance.sceneName);
    }

    void Update()
    {

    }

    public void SetController(string sceneName)
    {
        if (sceneName.Equals("Sauna"))
        {
            _button.moveToSauna.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(0F, 0F, -10F)));
            _button.moveToBulgama.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(-20F, 0F, -10F)));
            _button.enableCollectingBook.onClick.AddListener(() => _ui.SwitchUI(_ui.collectingBook));
        }
    }
}
