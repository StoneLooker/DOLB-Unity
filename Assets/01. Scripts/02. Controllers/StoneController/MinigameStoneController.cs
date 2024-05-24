using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStoneController : MonoBehaviour
{
    private float rotateSpeed = 300f;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRender;
    private bool isGrounded = true;

    public int life;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        GameManager.Input.keyAction -= JumpStone;
        GameManager.Input.keyAction += JumpStone;

        spriteRender = GetComponent<SpriteRenderer>();

        GameManager.Stone.WhenPlayerDecideGrowingNewStoneInBulgama(GameManager.Stone.stoneInfo["LimeStone"]);
        if(GameManager.Stone.growingStone.GetScientificName() == "LimeStone")
            life = 3;
        else if(GameManager.Stone.growingStone.GetScientificName() == "Granite")
            life = 5;
    }

    void Update()
    {
        transform.Rotate(0,0, -Time.deltaTime * rotateSpeed);
    }

    void JumpStone()
    {
        if(isGrounded)
        {
            rigid.AddForce(Vector3.up * 300.0f);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle")){
            if(life > 0)
                life--;
            if(life == 0)
                GameManager.Instance._minigame.isGameOver = true;
        }else if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}