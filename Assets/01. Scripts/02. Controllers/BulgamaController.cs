using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulgamaController : MonoBehaviour
{
    private Boolean isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.g.stateEvent.Bulgama.AddListener(StateEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateEvent()
    {
        if (isActive)
        {

        }
    }
}
