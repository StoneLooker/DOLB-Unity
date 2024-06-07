using System.Collections;
using UnityEngine;

//an obstacle that moves up and down
public class MovingObstacleController : ObstacleController
{
    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
        StartCoroutine(MoveUpAndDown());
    }

    private IEnumerator MoveUpAndDown()
    {
        float speed = 3f;
        while (gameObject.activeInHierarchy)
        {
            float newY = startPosition.y + Mathf.PingPong(Time.time * speed, 1.4f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}