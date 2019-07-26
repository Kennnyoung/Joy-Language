using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitCompleted : MonoBehaviour
{
    [SerializeField] GameObject questionCanvas;
    [SerializeField] GameObject finishPanal;
    [SerializeField] Transform enterPosition;
    [SerializeField] float moveInDuration;
    [SerializeField] Transform worldSettlementText;
    Vector3 targetPosition;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        targetPosition = finishPanal.transform.position;
        finishPanal.transform.position = enterPosition.position;
        worldSettlementText.GetComponent<Text>().text = gm.NumberOfWordPerUnit + " Words";
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
