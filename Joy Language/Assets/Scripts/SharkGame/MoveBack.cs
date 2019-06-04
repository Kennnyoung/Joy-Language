using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    Animator starAnimator;
    Vector3 idlePosition;

    float moveDuration;
    private void Awake()
    {
        starAnimator = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        idlePosition = transform.parent.position;
    }

    void SetBackDuration(float tempDuration)
    {
        moveDuration = tempDuration;
    }
    public IEnumerator BackToIdle()
    {
        starAnimator.SetBool("isMakingChoice", false);
        iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", idlePosition, "time", moveDuration, "easetype", "easeInOutQuad"));
        yield return new WaitForSeconds(moveDuration);
        starAnimator.SetBool("isMoving", false);
    }
}
