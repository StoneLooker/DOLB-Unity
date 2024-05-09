using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneController : MonoBehaviour
{
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
        //GameManager.Input.keyAction -= Draged;
        //GameManager.Input.keyAction += Draged;
    }

    public void Update()
    {
        if(isShoot){
            transform.Rotate(0, 0, Time.deltaTime * 200);
            rb.velocity *= Mathf.Pow(0.15f, Time.deltaTime);
            if(rb.velocity.magnitude < 0.01f)
                isShoot = false;
        }
    }

    public void OnMouseDown()
    {
        dragStartPosition = transform.position; // 드래그 시작 위치 저장
        startTime = Time.time; // 드래그 시작 시간 저장
        rb.isKinematic = true;
        isDrop = false;
    }

    public void OnMouseDrag()
    {
        if(!speedDrop)
        {
            // float distance = GameManager.Input.mousePosUnity.z;
            // Vector3 mousePosition = new Vector3(GameManager.Input.mousePosDevice.x, GameManager.Input.mousePosDevice.y, distance);
            Vector3 objPosition = GameManager.Input.mousePosUnity;

            float speed = (objPosition - lastPosition).magnitude / Time.deltaTime;
            //rb.velocity.magnitude
            if (rb.velocity.magnitude < 0.5f){
                transform.position = objPosition;
            }
            else
            {
                speedDrop = true;
                StartCoroutine(Delay());
            }
            lastPosition = transform.position;
            Debug.Log(rb.velocity.magnitude);
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
        if(other.CompareTag("InteractionZone"))
        {
            if(isInZone && isDrop)
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
}