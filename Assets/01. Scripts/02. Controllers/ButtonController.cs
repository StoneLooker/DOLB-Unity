using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button BackToSauna;
    [Header("Scene-MainTitle")]
    public Button signUp;
    public Button logIn;
    public Button logOut;
    public Button gameStart;
    public Button quit;

    [Header("Scene-Sauna")]
    public Button enableSetting;
    public Button enableCollectingBook;
    public Button enableSearchFriends;
    public Button enableProfileList;
    public Button moveToSauna;
    public Button moveToBulgama;
    public Button exit;

    [Header("Scene-Tub")]
    public Button PickBrush;
    public Button PickTowel;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
