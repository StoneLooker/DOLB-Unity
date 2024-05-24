using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    public MAP_TYPE map;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenMap()
    {
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.ChangeMap(map);
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