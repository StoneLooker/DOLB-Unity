using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controller for handling the behavior of the stone in the minigame
public class MinigameStoneController : MonoBehaviour
{
    private float rotateSpeed = 300f;
    private float jumpForce = 5f;
    public Vector3 startPos;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRender;

    //Boolean to check if the stone is grounded
    private bool isGrounded = true;

    //Life count of the stone
    public int life;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();

        startPos = this.transform.position;

        //Set the life of the stone based on its type
        if(GameManager.Stone.growingStone.stoneStat.Equals(STONE_TYPE.LimeStone))
            life = 3;
        else if(GameManager.Stone.growingStone.stoneStat.Equals(STONE_TYPE.Granite))
            life = 5;
    }

    void Update()
    {
        //Check if the minigame is active
        if(GameManager.Instance._minigame.minigameControl)
        {
            //Rotate the stone
            transform.Rotate(0,0, -Time.deltaTime * rotateSpeed);

            //Check for mouse button input to make the stone jump
            if(Input.GetMouseButton(0))
                JumpStone();
        }
    }

    //Method to make the stone jump
    void JumpStone()
    {
        if(isGrounded)
        {
            //Reset the vertical velocity and apply an upward force
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Triggered when the stone collides with another object
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}