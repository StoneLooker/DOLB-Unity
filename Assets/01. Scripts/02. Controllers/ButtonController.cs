using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button BackToSauna;
    [Header("Scene-MainTitle")]

    [Header("Scene-Sauna")]
    public Button enableSetting;
    public Button enableCollectingBook;
    public Button moveToSauna;
    public Button moveToBulgama;
    [Header("Scene-Tub")]
    public Button PickBrush;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
