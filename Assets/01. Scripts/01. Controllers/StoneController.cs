using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    private float dragStartTime;
    private bool isDragging;
    public float maxDragTime = 3.0f; //마우스 드래그 시간(최대)
    private Rigidbody rb;

    void Start()
    {
        GameManager.Input.keyAction -= Draged;
        GameManager.Input.keyAction += Draged;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            float duration = Time.time - dragStartTime;
            if (duration > maxDragTime)
                ReleaseObject();
        }
    }

    void Draged()
    {
        if (!isDragging)
        {
            isDragging = true;
            dragStartTime = Time.time;
            rb.useGravity = true;
        }

        if (isDragging)
        {
            float distance = GameManager.Input.mousePosUnity.z;
            Vector3 mousePosition = new Vector3(GameManager.Input.mousePosDevice.x, GameManager.Input.mousePosDevice.y, distance);
            Vector3 objPosition = GameManager.Input.mousePosUnity;
            transform.position = objPosition;
        }
    }

    void ReleaseObject()
    {
        isDragging = false;
        rb.useGravity = false;
    }
}