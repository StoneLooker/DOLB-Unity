using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float objectSpeed = 8.0f;

    void Update()
    {
        objectSpeed += 0.1f * Time.deltaTime;

        if (transform.position.y < 5)
            transform.position -= new Vector3(objectSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x < -10)
            gameObject.SetActive(false);
    }
}
