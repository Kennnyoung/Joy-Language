using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    Animator starAnimator;

    void Awake()
    {
        starAnimator = gameObject.GetComponent<Animator>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveBack(float tempDuration,Vector3 tempPosition)
    {
        starAnimator.SetBool("isMakingChoice", false);
        iTween.MoveTo(gameObject, iTween.Hash("position", tempPosition, "time", tempDuration, "easetype", "easeInOutQuart"));
    }
}
