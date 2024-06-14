using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle scrolling background in the minigame
public class ScrollBackground : MonoBehaviour
{
    private float backGroundSpeed = 2.0f;
    private float moveCheck;

    void Start() {
        moveCheck = transform.position.x;
    }

    void Update() {

        //Check if the minigame is currently active
        if(GameManager.Instance._minigame.minigameControl)
        {
            //Move the background to the left
            moveCheck -= backGroundSpeed * Time.deltaTime;
            transform.position = new Vector3(moveCheck, transform.position.y, transform.position.z);
            
            //Reset the position when the background moves off-screen to create a looping effect
            if (moveCheck < -8.65f)
                moveCheck = 11f;
        }
    }
}