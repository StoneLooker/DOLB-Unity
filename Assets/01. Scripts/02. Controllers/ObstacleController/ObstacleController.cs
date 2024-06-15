using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //Speed at which the obstacle moves
    public float objectSpeed = 5.0f;

    public void Update()
    {
        //Increment the speed of the obstacle over time
        objectSpeed += 0.1f * Time.deltaTime;

        //Check if the obstacle is below a certain heigh
        if (transform.position.y < 5)
            transform.position -= new Vector3(objectSpeed * Time.deltaTime, 0, 0); //Move the obstacle to the left
        //Check if the obstacle has moved off the left side of the screen
        if (transform.position.x < -10)
            gameObject.SetActive(false); //Deactivate the obstacle
    }
}