using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }

        else
        {
            Load();
        }

    }

    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    
    }

    private void Load()
    {

    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
