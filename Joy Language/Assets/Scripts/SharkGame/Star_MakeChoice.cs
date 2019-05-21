using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_MakeChoice : MonoBehaviour
{
    public Animator starAnimator;
    public float moveDuration;

    Vector3 idlePosition;
    // Start is called before the first frame update
    void Start()
    {
        idlePosition = transform.position;
    }

    IEnumerator MakeChoice(Transform tempTarget)
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", tempTarget.position, "time", moveDuration, "easetype", "easeInOutQuart"));
        iTween.RotateBy(gameObject, iTween.Hash("z", 1, "time", moveDuration, "easeType", "easeInOutBack"));
        starAnimator.SetBool("isMoving", true);
        yield return new WaitForSeconds(moveDuration);
        starAnimator.SetBool("isMakingChoice", true);
        yield return null;
        starAnimator.SetBool("isMoving", false);
    }
}
