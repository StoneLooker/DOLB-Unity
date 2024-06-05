using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject fadePanel;
    Image image;

    public void LoadingSceneLoad()
    {
        fadePanel.SetActive(true);
        image = fadePanel.GetComponent<Image>();
        StartCoroutine(StartFadeCoroutine());
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

        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}
