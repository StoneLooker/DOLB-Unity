using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneController : MonoBehaviour
{
    public Stone stone;
    public bool isInZone;
    private bool isDrop;
    private bool isShoot;
    public bool speedDrop;
    private Vector3 originPosition;
    private Vector3 lastPosition;
    private Vector3 dragStartPosition;
    private float startTime;
    private Rigidbody2D rb;


    public void Start()
    {
        originPosition = transform.position;
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetStone(Stone stone)
    {
        this.stone = stone;
    }

    public void Update()
    {
        if (isShoot)
        {
            transform.Rotate(0, 0, Time.deltaTime * 200);
            rb.velocity *= Mathf.Pow(0.15f, Time.deltaTime);
            if (rb.velocity.magnitude < 0.01f)
                isShoot = false;
        }
    }

    public void OnMouseDown()
    {
        dragStartPosition = transform.position;
        startTime = Time.time;
        rb.isKinematic = true;
        isDrop = false;
    }

    public void OnMouseDrag()
    {
        if (!speedDrop)
        {
            Vector3 objPosition = GameManager.Input.mousePosUnity;

            float speed = (objPosition - lastPosition).magnitude / Time.deltaTime;
            if (rb.velocity.magnitude < 0.5f)
            {
                transform.position = objPosition;
            }
            else
            {
                speedDrop = true;
                StartCoroutine(Delay());
            }
            lastPosition = transform.position;
            //Debug.Log(rb.velocity.magnitude);
        }
    }

    public void OnMouseUp()
    {
        rb.isKinematic = false;
        float duration = Time.time - startTime;
        Vector3 endPosition = transform.position;
        Vector3 velocity = (endPosition - dragStartPosition) / duration;
        rb.velocity = velocity;

        isDrop = true;
        isShoot = true;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("InteractionZone"))
        {
            if (isInZone && isDrop)
            {
                other.GetComponent<InteractionZone>().OpenMap();
                isInZone = false;
                isDrop = false;
                speedDrop = false;
                transform.position = originPosition;
            }
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        speedDrop = false;
    }
    public void OnCollisionStay(Collision collision)
    {
    }
    public void Set(string nN)
    {
    }
}