using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_EnterCanvas : MonoBehaviour
{
    public Transform enterPosition;
    public float delay;
    Vector3 idlePosition;
    // Start is called before the first frame update
    void Awake()
    {
        idlePosition = transform.position;
    }

    IEnumerator Enter(float tempDuration)
    {
        transform.position = enterPosition.position;
        yield return new WaitForSeconds(delay);
        float duration = tempDuration - delay;
        iTween.MoveTo(gameObject, iTween.Hash("position", idlePosition, "time", duration, "easetype", "easeOutQuad"));
    }
}
