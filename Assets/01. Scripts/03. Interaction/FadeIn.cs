using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FadeIn : MonoBehaviour
{
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
        "Roll the stone!"
    };

    void Awake()
    {
        StartCoroutine(FadeInStart());
    }

    IEnumerator FadeInStart()
    {
        yield return new WaitForSeconds(0.3f);

        image = fadePanel.GetComponent<Image>();
        float fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.015f);
            image.color = new Color(0,0,0,fadeCount);
        }
        
        if(SceneManager.GetActiveScene().name != "Loading")
            fadePanel.SetActive(false);
        else
        {
            canvas.SetActive(true);
            int randomIndex = Random.Range(0, messages.Length);
            msg.text = messages[randomIndex];
            msg.gameObject.SetActive(true);
            StartCoroutine(Loading());
        }
    }

    IEnumerator Loading()
    {
        character.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}
