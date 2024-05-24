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
        SetController(GameManager.Instance.nowMap);
    }

    void Update()
    {

    }

    public void SetController(MAP_TYPE map)
    {
        if (_button.BackToSauna != null)
        {
            _button.BackToSauna.onClick.RemoveAllListeners();
            _button.BackToSauna.onClick.AddListener(() => GameManager.Instance.ChangeMap(MAP_TYPE.Sauna));
        }
        if (map.Equals(MAP_TYPE.Sauna))
        {
            _ui.main.SetActive(true);
            _ui.collectingBook.SetActive(false);

            _button.moveToSauna.onClick.RemoveAllListeners();
            _button.moveToBulgama.onClick.RemoveAllListeners();
            _button.enableCollectingBook.onClick.RemoveAllListeners();

            _button.moveToSauna.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(0F, 0F, -10F)));
            _button.moveToBulgama.onClick.AddListener(() => GameManager.Instance.ChangeMap(MAP_TYPE.Bulgama));
            _button.enableCollectingBook.onClick.AddListener(() => _ui.SwitchUI(_ui.collectingBook));
        }
        if (map.Equals(MAP_TYPE.Tub))
        {
            _button.PickBrush.onClick.RemoveAllListeners();

            _button.PickBrush.onClick.AddListener(() => _item.PickBrush());
        }
    }
}
