using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    public Button settingButton;
    public Button bookButton;
    public Button mapMoveButton_Main;
    public Button mapMoveButton_Select;

    // Start is called before the first frame update
    void Start()
    {
        mapMoveButton_Main.onClick.AddListener(() => GameManager.Camera.MoveMainCamera(new Vector3(0, 0, -10)));
        mapMoveButton_Select.onClick.AddListener( () => GameManager.Camera.MoveMainCamera(new Vector3(-20,0,-10)) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    