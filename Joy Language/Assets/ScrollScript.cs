using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    // Start is called before the first frame update
    float ScrollSpeed = -5f;
    Vector2 StartPos;

    void Start()
    {
        StartPos = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * ScrollSpeed, 20);
        transform.position = StartPos + Vector2.up * newPos;
    }
}
