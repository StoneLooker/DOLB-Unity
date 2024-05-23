using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    private float backGroundSpeed = 2.0f;
    private float moveCheck;

    void Start() {
        moveCheck = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        moveCheck -= backGroundSpeed * Time.deltaTime;
        transform.position = new Vector3(moveCheck, transform.position.y, transform.position.z);
        if (moveCheck < -8.69f)
            moveCheck = 11f;
    }
}