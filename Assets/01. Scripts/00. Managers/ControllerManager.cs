using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] public CameraController _camera;
    [SerializeField] public ButtonController _button;
    [SerializeField] public UIController _ui;
    [SerializeField] public ItemController _item;

    void Start()
    {
        GameManager.Instance._controller = null;
        GameManager.Instance._controller = this;
        SetController(GameManager.Instance.sceneName);
    }

    void Update()
    {

    }

    public void SetController(string sceneName)
    {
        _button.BackToSauna.onClick.RemoveAllListeners();
        _button.BackToSauna.onClick.AddListener(() => GameManager.Instance.ChangeScene("Sauna"));
        if (sceneName.Equals("Sauna"))
        {
            _ui.main.SetActive(true);
            _ui.collectingBook.SetActive(false);

            _button.moveToSauna.onClick.RemoveAllListeners();
            _button.moveToBulgama.onClick.RemoveAllListeners();
            _button.enableCollectingBook.onClick.RemoveAllListeners();

            _button.moveToSauna.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(0F, 0F, -10F)));
            _button.moveToBulgama.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(-20F, 0F, -10F)));
            _button.enableCollectingBook.onClick.AddListener(() => _ui.SwitchUI(_ui.collectingBook));
        }
        if (sceneName.Equals("Tub"))
        {
            _button.PickBrush.onClick.RemoveAllListeners();

            _button.PickBrush.onClick.AddListener(() => _item.PickBrush());
        }
    }
}
