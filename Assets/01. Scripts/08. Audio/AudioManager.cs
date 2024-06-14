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
        // If there is no instance of AudioManager, set this as the instance and make it persistent across scenes
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this game object to enforce the singleton pattern
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Check if the PlayerPrefs contains a key for "musicVolume"
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            // Set the default volume value to 1
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }

        else
        {
            Load();
        }

    }

    // Method to change the volume based on the slider value
    public void changeVolume()
    {
        // Set the AudioListener volume to the slider's value
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {

    }

    // Method to save the current volume value to PlayerPrefs
    private void Save()
    {
        // Save the slider's value as the "musicVolume" in PlayerPrefs
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
