using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneController : MonoBehaviour
{
    public bool isInZone;
    private bool isDrop;
    public bool speedDrop;
    private Vector3 originPosition;
    private Vector3 lastPosition;
    private Vector3 dragStartPosition;
    private float startTime;
    private Rigidbody2D rb;
    private float timeSinceLastPositionUpdate = 0f;

    void Start()
    {
        originPosition = transform.position;
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        //GameManager.Input.keyAction -= Draged;
        //GameManager.Input.keyAction += Draged;
    }

    void Update()
    {
        timeSinceLastPositionUpdate += Time.deltaTime;

        if (timeSinceLastPositionUpdate >= 1f)
        {
            lastPosition = transform.position;
            timeSinceLastPositionUpdate = 0f;
        }   
    }

    // void Draged()
    // {
    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         // 오브젝트가 선택되었는지 확인
    //         if (hit.transform.gameObject == transform.gameObject)
    //         {
    //             Debug.Log("In");
    //             float distance = GameManager.Input.mousePosUnity.z;
    //             Vector3 mousePosition = new Vector3(GameManager.Input.mousePosDevice.x, GameManager.Input.mousePosDevice.y, distance);
    //             Vector3 objPosition = GameManager.Input.mousePosUnity;
    //             transform.position = objPosition;
    //         }
    //     }
    // }

    void OnMouseDown()
    {
        dragStartPosition = transform.position; // 드래그 시작 위치 저장
        startTime = Time.time; // 드래그 시작 시간 저장
        rb.isKinematic = true;
        lastPosition = transform.position;
        isDrop = false;
    }

    void OnMouseDrag()
    {
        if(!speedDrop)
        {
            float distance = GameManager.Input.mousePosUnity.z;
            Vector3 mousePosition = new Vector3(GameManager.Input.mousePosDevice.x, GameManager.Input.mousePosDevice.y, distance);
            Vector3 objPosition = GameManager.Input.mousePosUnity;

            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime * 10000;
            //rb.velocity.magnitude
            if (speed < 0.7f){
                transform.position = objPosition;
                lastPosition = transform.position;
            }
            else
            {
                lastPosition = transform.position;
                speedDrop = true;
                StartCoroutine(Delay());
            }

            lastPosition = objPosition;
            Debug.Log(speed);
        }
    }

    void OnMouseUp()
    {
        rb.isKinematic = false; // 드래그 종료 후 물리적 영향을 다시 받도록 설정
        float duration = Time.time - startTime; // 드래그한 총 시간 계산
        Vector3 endPosition = transform.position; // 드래그 종료 위치
        Vector3 velocity = (endPosition - dragStartPosition) / duration; // 속도 계산

        rb.velocity = velocity; // 계산된 속도를 Rigidbody의 속도로 설정
        StartCoroutine(StopMovementAfterTime(2.0f));

        isDrop = true;
    }

    IEnumerator StopMovementAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero; // 속도를 0으로 설정하여 움직임을 멈춤
    }

    private void OnTriggerStay2D(Collider2D other) 
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

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        speedDrop = false;
    }
}