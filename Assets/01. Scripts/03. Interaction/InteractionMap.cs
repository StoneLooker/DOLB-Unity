using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMap : MonoBehaviour
{
    public GameObject zone;
    public GameObject stone;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(3f);
        zone.SetActive(true);
        stone.SetActive(true);
        gameObject.SetActive(false);
    }
}
