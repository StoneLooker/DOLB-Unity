using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//Handles interactions within a specific zone in the game
public class InteractionZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

     // Type of map associated with this interaction zone
    [SerializeField] public MAP_TYPE map;

    // Reference to the info screen UI element
    public GameObject infoScreen;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Method to open the map associated with this interaction zone
    public void OpenMap()
    {
        if(map == MAP_TYPE.MiniGame)
        {
            //Check if the player can enter the minigame
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
    
    //Method to update the text on the info screen
    private void UpdateInfoText(string str)
    {
        infoScreen.SetActive(true);
        infoScreen.transform.GetChild(0).GetComponent<TMP_Text>().text = str;
    }
}