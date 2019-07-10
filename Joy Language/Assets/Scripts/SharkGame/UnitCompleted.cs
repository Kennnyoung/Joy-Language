using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitCompleted : MonoBehaviour
{
    [SerializeField] GameObject questionCanvas;
    [SerializeField] GameObject finishPanal;
    [SerializeField] Transform enterPosition;
    [SerializeField] float moveInDuration;

    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = finishPanal.transform.position;
        finishPanal.transform.position = enterPosition.position;
    }

    IEnumerator FinishUnit()
    {
        yield return new WaitForSeconds(1f);
        questionCanvas.SetActive(false);
        yield return null;
        iTween.MoveTo(finishPanal, iTween.Hash("position", targetPosition, "time", moveInDuration, "easetype", "easeOutBack"));
        yield return new WaitForSeconds(moveInDuration); 
    }
}
