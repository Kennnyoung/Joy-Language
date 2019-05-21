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

        yield return new WaitForSeconds(moveDuration);
        starAnimator.SetBool("isMakingChoice", true);
    }
}
