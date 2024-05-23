using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public Animator panelAnimator;
    public GameObject item;

    public void OpenPanel()
    {
        panelAnimator.SetTrigger("Open");
    }

    public void ClosePanel()
    {
        panelAnimator.SetTrigger("Close");
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
