using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStoneController : MonoBehaviour
{
    private float rotateSpeed = 300f;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRender;
    private bool isGrounded = true;

    public int life = 3;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        rigid.mass = 1.5f;
        rigid.gravityScale = 2.0f;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0,0, -Time.deltaTime * rotateSpeed);
    }

    void OnMouseDown()
    {
        if(isGrounded)
        {
            rigid.AddForce(Vector3.up * 600.0f);
            isGrounded = false;
        }
    }

    private void OntriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
            life--;
        else if(collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OntriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}