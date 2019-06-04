using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_MakeChoice : MonoBehaviour
{
    public Animator starAnimator;
    public float moveDuration;
    public float backDuration;
    Vector3 idlePosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).SendMessage("SetFeedbackDuration", backDuration);
    }

    IEnumerator MakeChoice(Transform tempTarget)
    {
        starAnimator.SetBool("isMoving", true);
        iTween.MoveTo(gameObject, iTween.Hash("position", tempTarget.position, "time", moveDuration, "easetype", "easeInOutQuart"));
        iTween.RotateBy(gameObject, iTween.Hash("z", 1, "time", moveDuration, "easeType", "easeInOutBack"));
        yield return new WaitForSeconds(moveDuration);
        starAnimator.SetBool("isMakingChoice", true);
    }
}
