using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNavigationController : MonoBehaviour
{
    public GameObject CurrentCanvas;
    public GameObject TargetCanvas;

    public void CanvasNavigation()
    {
        CurrentCanvas.SetActive(false);
        TargetCanvas.SetActive(true);
    }
}
