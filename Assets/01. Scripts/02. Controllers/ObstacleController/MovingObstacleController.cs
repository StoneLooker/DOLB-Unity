using System.Collections;
using UnityEngine;

//An obstacle that moves up and down
public class MovingObstacleController : ObstacleController
{
    //Starting position of the obstacle
    private Vector3 startPosition;

    //Called when the object is enabled
    private void OnEnable()
    {
        startPosition = transform.position;
        StartCoroutine(MoveUpAndDown());
    }

    private IEnumerator MoveUpAndDown()
    {
        float speed = 1.5f;

        //Loop as long as the game object is active in the hierarchy
        while (gameObject.activeInHierarchy)
        {
            //Calculate the new Y position using PingPong for oscillating motion
            float newY = startPosition.y + Mathf.PingPong(Time.time * speed, 1f);
            //Update the position of the obstacle
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            //Wait for the next frame
            yield return null;
        }
    }

    //Called when the object is disabled
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}