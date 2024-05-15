using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void MoveMainCamera(Vector3 location)
    {
        mainCamera.transform.localPosition = location;
    }

    void Update()
    {
        
    }
}
