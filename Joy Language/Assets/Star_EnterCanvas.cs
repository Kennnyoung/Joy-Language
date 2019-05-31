using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_EnterCanvas : MonoBehaviour
{
    public Transform shark;
    public Transform enterPosition;
    Vector3 idlePosition;
    // Start is called before the first frame update
    void Awake()
    {
        idlePosition = transform.position;
        shark.SendMessage("SetAim", idlePosition);
    }

    void Enter(float tempDuration)
    {
        transform.position = enterPosition.position;
        iTween.MoveTo(gameObject, iTween.Hash("position", idlePosition, "time", tempDuration, "easetype", "easeOutQuad"));
    }
}
