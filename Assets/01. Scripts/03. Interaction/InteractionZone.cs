using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractionZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] public MAP_TYPE map;
    public GameObject infoScreen;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenMap()
    {
        if(map == MAP_TYPE.MiniGame)
        {
            if(GameManager.Instance.MinigameEnter)
            {
                transform.parent.gameObject.SetActive(false);
                GameManager.Instance.ChangeMap(map);
            }else
            {
                UpdateInfoText("It will be available in 10 seconds.");
            }
        }else
        {
            transform.parent.gameObject.SetActive(false);
            GameManager.Instance.ChangeMap(map);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Stone"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
            other.GetComponent<StoneController>().isInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Stone"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            other.GetComponent<StoneController>().isInZone = false;
        }
    }

    private void UpdateInfoText(string str)
    {
        infoScreen.SetActive(true);
        infoScreen.transform.GetChild(0).GetComponent<TMP_Text>().text = str;
    }
}