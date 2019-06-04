using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    [SerializeField] ParticleSystem attackParticle;
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

    void SetFeedbackDuration(float tempDuration)
    {
        moveDuration = tempDuration;
    }

    IEnumerator BackToIdle()
    {
        starAnimator.SetBool("isMakingChoice", false);
        iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", idlePosition, "time", moveDuration, "easetype", "easeInOutQuad"));
        yield return new WaitForSeconds(moveDuration);
        starAnimator.SetBool("isMoving", false);
    }

    IEnumerator Attack()
    {
        starAnimator.SetBool("isMakingChoice", false);
        iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", idlePosition, "time", moveDuration/2, "easetype", "easeInOutQuad"));
        yield return new WaitForSeconds(moveDuration/2);
        starAnimator.SetBool("isMoving", false);
        attackParticle.Play();
    }
}
