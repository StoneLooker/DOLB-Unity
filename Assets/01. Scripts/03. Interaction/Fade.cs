using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject logInPanel;
    public GameObject infoPanel;
    Image image;

    public void SceneLoad()
    {
        fadePanel.SetActive(true);
        image = fadePanel.GetComponent<Image>();
        StartCoroutine(StartFadeCoroutine());
    }

    public void LogInPanelOpen()
    {
        logInPanel.SetActive(true);
    }

    public void closeInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    IEnumerator StartFadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.015f);
            image.color = new Color(0,0,0,fadeCount);
        }
        
        if(SceneManager.GetActiveScene().name == "Title")
            SceneManager.LoadScene("Loading");
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
