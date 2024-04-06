using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Vector3 mousePosDevice;
    public Vector3 mousePosUnity { get { return Camera.main.ScreenToWorldPoint(mousePosDevice); } }

    public Action keyAction = null;

    // Update is called once per frame
    public void OnUpdate()
    {
        mousePosDevice = Input.mousePosition;
        mousePosDevice.z = 10.0F;

        if (Input.anyKey == false) return;

        keyAction?.Invoke();
    }
}
