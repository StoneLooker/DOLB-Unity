using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject main;
    [SerializeField] public GameObject collectingBook;

    public void OnStart()
    {
        main.SetActive(true);
        collectingBook.SetActive(false);
    }    

    public void EnableUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void DisableUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void SwitchUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
    }
}
