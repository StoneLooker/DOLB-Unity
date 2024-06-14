using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Scene-Sauna")]
    [SerializeField] public GameObject main;
    [SerializeField] public GameObject SearchFriends;
    [SerializeField] public GameObject Setting;
    
    [SerializeField] public Slider LoveGageSlider;
    [SerializeField] public Slider EvolutionGageSlider;

    private void Awake()
    {
    }

    void Start()
    {
        if(LoveGageSlider != null)
        {
            if (GameManager.Stone.growingStone == null)
                SetSlider(LoveGageSlider, 0, 0);
            else
            {
                SetSlider(LoveGageSlider, GameManager.Stone.growingStone.maxLoveGage, GameManager.Stone.growingStone.loveGage);
            }
        }

        if (EvolutionGageSlider != null)
        {
            if (GameManager.Stone.growingStone == null)
                SetSlider(EvolutionGageSlider, 0, 0);
            else
            {
                SetSlider(EvolutionGageSlider, GameManager.Stone.growingStone.maxEvolutionGage, GameManager.Stone.growingStone.evolutionGage);
            }
        }
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

    public void CallSetSlider(SLIDER_TYPE sliderType, float maxValue, float initValue)
    {
        switch(sliderType)
        {
            case SLIDER_TYPE.LoveGage:
                this.SetSlider(this.LoveGageSlider, maxValue, initValue); break;
            case SLIDER_TYPE.Evolution:
                this.SetSlider(this.EvolutionGageSlider, maxValue, initValue); break;
        }
    }

    private void SetSlider(Slider slider, float maxValue, float initValue)
    {
        if(slider == null)
        {
            Debug.LogError("Empty slider: " + slider);
            return;
        }
        
        slider.maxValue = maxValue;
        slider.value = initValue;       
    }

    public void CallUpdateSlider(SLIDER_TYPE sliderType, float targetValue)
    {
        switch (sliderType)
        {
            case SLIDER_TYPE.LoveGage:
                this.UpdatSlider(this.LoveGageSlider, targetValue); break;
            case SLIDER_TYPE.Evolution:
                this.UpdatSlider(this.EvolutionGageSlider, targetValue); break;
        }
    }

    private void UpdatSlider(Slider slider, float targetValue)
    {
        if (slider == null)
        {
            Debug.LogError("Empty slider: " + slider);
            return;
        }

        slider.value = targetValue;
    }
}

public enum SLIDER_TYPE
{
    LoveGage, Evolution
}
