using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject character;
    Image image;

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
            StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        character.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}
