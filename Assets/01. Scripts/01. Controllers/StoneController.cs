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
    private Rigidbody2D rb;

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

            //float speed = (transform.position - lastPosition).magnitude / Time.deltaTime * 10000;

            if (rb.velocity.magnitude < 0.7f){
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
            Debug.Log(rb.velocity.magnitude);
        }
    }

    void OnMouseUp()
    {
        isDrop = true;
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