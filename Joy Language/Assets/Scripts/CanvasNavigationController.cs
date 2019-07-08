using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasNavigationController : MonoBehaviour
{
    public GameObject CurrentCanvas;
    public GameObject TargetCanvas;

    public void CanvasNavigation()
    {
        CurrentCanvas.SetActive(false);
        TargetCanvas.SetActive(true);
    }

    public void SceneNavigation()
    {
        SceneManager.LoadScene("Shark Game");
    }
}
