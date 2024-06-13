using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("musicVolume"); 
            volumeSlider.value = savedVolume;  
            AudioListener.volume = savedVolume; 
        }

        else
        {
             volumeSlider.value = 1f; 
            AudioListener.volume = 1f; 
            Save(); 
        }

    }

    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
