using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//Handles the fade-in effect at the start of a scene
public class FadeIn : MonoBehaviour
{
    //UI elements and messages
    public GameObject fadePanel;
    public GameObject character;
    public GameObject canvas;
    public TMP_Text msg;
    Image image;

    private string[] messages = new string[]
    {
        "Welcome to the Sauna!",
        "Relax and enjoy your time!",
        "Items can be obtained through a walk!",
        "Search your friend's collecting book!",
        "Roll the stone!",
        "Some stones like hot rooms.."
    };

    void Awake()
    {
        StartCoroutine(FadeInStart());
    }

    //Coroutine to handle the fade-in effect
    IEnumerator FadeInStart()
    {
        //Wait briefly before starting the fade-in
        yield return new WaitForSeconds(0.3f);

        //Get the Image component from the fade panel
        image = fadePanel.GetComponent<Image>();
        float fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.015f);
            image.color = new Color(0,0,0,fadeCount);
        }
        
        //Check if the current scene is not "Loading"
        if(SceneManager.GetActiveScene().name != "Loading")
            fadePanel.SetActive(false);
        else
        {
            //Display a random message and start the loading coroutine
            canvas.SetActive(true);
            int randomIndex = Random.Range(0, messages.Length);
            msg.text = messages[randomIndex];
            msg.gameObject.SetActive(true);
            StartCoroutine(Loading());
        }
    }

    //Coroutine to handle loading the next scene
    IEnumerator Loading()
    {
        //Show the character and wait for a few seconds before changing the map
        character.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}
