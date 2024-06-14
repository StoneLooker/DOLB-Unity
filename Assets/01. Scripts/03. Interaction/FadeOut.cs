using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Handles the fade-out effect when loading a new scene
public class FadeOut : MonoBehaviour
{
    //UI element for the fade panel
    public GameObject fadePanel;
    Image image;

    //Method to start loading a new scene with a fade-out effect
    public void LoadingSceneLoad()
    {
        fadePanel.SetActive(true);
        image = fadePanel.GetComponent<Image>();
        StartCoroutine(StartFadeCoroutine());
    }

    //Coroutine to handle the fade-out effect
    IEnumerator StartFadeCoroutine()
    {
        float fadeCount = 0;

        //Gradually increase the fade count to create the fade-out effect
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.015f);
            image.color = new Color(0,0,0,fadeCount);
        }

        //Load the "Loading" scene after the fade-out is complete
        SceneManager.LoadScene("Loading");
    }
}
