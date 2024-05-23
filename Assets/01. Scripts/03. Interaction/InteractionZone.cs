using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public string mapName;
    public GameObject stone; //나중에 Stone 싱글톤으로 만들면 수정할 예정

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenMap()
    {
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.ChangeScene(mapName);
        stone.SetActive(false);
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
}