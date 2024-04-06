using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Input.keyAction -= Draged;
        GameManager.Input.keyAction += Draged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Draged()
    {

    }
}