using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    // Static variable for the singleton instance
    public static AudioManager instance;
    [SerializeField] Slider volumeSlider;


    // Initialize the instance and destroy duplicate instances
    private void Awake()
    {
        // If there is no instance of AudioManager, set this as the instance and make it persistent across scenes
        if(instance == null)
        {
            instance = this;
           // If an instance already exists, destroy this game object to enforce the singleton pattern
            DontDestroyOnLoad(gameObject);
        }

        // Destroy the duplicate instance
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Set the volume on start
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

    // Called when the slider value is changed
    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    
    }

    // Save the volume value
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
