using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public Animator panelAnimator;
    public GameObject item;
    private bool check;

    public void ControlPanel()
    {
        if(!check)
        {
            panelAnimator.SetTrigger("Open");
            check = true;
        }
        else
        {
            panelAnimator.SetTrigger("Close");
            check = false;
        }
    }


    public void ShowItem()
    {
        item.SetActive(true);
    }

    public void HideItem()
    {
        item.SetActive(false);
    }
}