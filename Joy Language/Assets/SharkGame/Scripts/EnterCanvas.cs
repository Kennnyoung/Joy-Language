using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnterCanvas : MonoBehaviour
{
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAim(Vector3 tempAim)
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", tempAim, "time", duration, "easetype", "easeOutBack"));
    }
}
