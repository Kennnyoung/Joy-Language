using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Chasing : MonoBehaviour
{
    public float verticalOffset;
    Vector3 aim;
    
    void SetAim(Vector3 tempAim)
    {
        aim = tempAim;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(aim.x, aim.y - verticalOffset), 0.05f * Time.deltaTime);
    }
}
