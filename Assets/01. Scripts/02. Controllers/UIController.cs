using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Scene-Sauna")]
    [SerializeField] public GameObject main;
    [SerializeField] public GameObject SearchFriends;
    [SerializeField] public Slider HpSlider;

    private void Awake()
    {
    }

    void Start()
    {
    }

    public void EnableUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void DisableUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void SwitchUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
    }

    public void SetHPSlider(float maxValue, float initValue)
    {
        if(HpSlider == null)
        {
            Debug.LogError("Empty HP Slider");
            return;
        }
        
        this.HpSlider.maxValue = maxValue;
        this.HpSlider.value = initValue;
       
    }    

    public void UpdatHPSlider(float targetValue)
    {
        if (HpSlider == null)
        {
            Debug.LogError("Empty HP Slider");
            return;
        }

        this.HpSlider.value = targetValue;
    }
}
