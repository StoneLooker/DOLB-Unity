using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public void MoveMainCamera(Vector3 location)
    {
        mainCamera.transform.localPosition = location;
    }

    void Update()
    {
        
    }
}
