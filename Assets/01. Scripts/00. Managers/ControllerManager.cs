using System;
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

        if (map.Equals(MAP_TYPE.MainTitle))
        {
            _button.signUp.onClick.RemoveAllListeners();
            _button.logIn.onClick.RemoveAllListeners();
            _button.logOut.onClick.RemoveAllListeners();
            _button.gameStart.onClick.RemoveAllListeners();
            _button.quit.onClick.RemoveAllListeners();

            _button.signUp.onClick.AddListener(GameManager.Instance._logIn.SignUp);
            _button.logIn.onClick.AddListener(GameManager.Instance._logIn.LogIn);
            _button.logOut.onClick.AddListener(GameManager.Instance._logIn.LogOut);
            _button.gameStart.onClick.AddListener(GameManager.Instance._logIn.UpdateButtonListener);
            //_button.gameStart.onClick.AddListener(() => GameManager.Instance.ChangeMap(MAP_TYPE.Sauna));
            _button.quit.onClick.AddListener(GameManager.Instance.GameQuit);
        }
        else if (map.Equals(MAP_TYPE.Sauna))
        {
            _ui.main.SetActive(true);
            _ui.SearchFriends.SetActive(false);
            _ui.Setting.SetActive(false);

            _button.moveToSauna.onClick.RemoveAllListeners();
            _button.moveToBulgama.onClick.RemoveAllListeners();
            _button.moveToMainTitle.onClick.RemoveAllListeners();
            _button.enableCollectingBook.onClick.RemoveAllListeners();
            _button.enableSetting.onClick.RemoveAllListeners();

            _button.moveToSauna.onClick.AddListener(() => _camera.MoveMainCamera(new Vector3(0F, 0F, -10F)));
            _button.moveToBulgama.onClick.AddListener(() => GameManager.Instance.ChangeMap(MAP_TYPE.Bulgama));
            _button.moveToMainTitle.onClick.AddListener(() => GameManager.Instance.ChangeMap(MAP_TYPE.MainTitle));

            _button.enableCollectingBook.onClick.AddListener(() => GameManager.Instance._book.LookMyBook());

            _button.enableSearchFriends.onClick.AddListener(GameManager.Instance._search.StartShowUserList);
            _button.enableSearchFriends.onClick.AddListener(() => _ui.SwitchUI(_ui.SearchFriends));

            _button.enableSetting.onClick.AddListener(() => _ui.SwitchUI(_ui.Setting));

            _button.exit.onClick.AddListener( () => GameManager.Instance.GameQuit());
        }
        else if (map.Equals(MAP_TYPE.Tub))
        {
            _button.PickBrush.onClick.RemoveAllListeners();

            _button.PickBrush.onClick.AddListener(() => _item.PickBrush());
        }
        else if (map.Equals(MAP_TYPE.Tub))
        {

        }
    }
}
