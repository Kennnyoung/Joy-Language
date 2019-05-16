using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // every frame
       
        // if left-mouse-button is being held OR there is at least one touch
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            // get mouse position in screen space
            // (if touch, gets average of all touches)
            GetComponent<Animator>().enabled = false;
            Vector3 screenPos = Input.mousePosition;
            // set a distance from the camera
            //screenPos.z = 500.0f;
            // convert mouse position to world space
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            // get current position of this GameObject
            Vector3 newPos = transform.position;
            // set x position to mouse world-space x position
            newPos.x = worldPos.x;
            newPos.y = worldPos.y;

            //get orthographic camera height and width
            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;

            if (newPos.y < -130)
            {
                newPos.y = -130;
            }
            if (newPos.y > 80)
            {
                newPos.y = 80;
            }
            if (newPos.x < -width/2 + 0.5f)
            {
                newPos.x = -width / 2 + 0.5f;
            }
            if (newPos.x > width/2 - 0.5f)
            {
                newPos.x = width / 2 - 0.5f;
            }
            // apply new position
            transform.position = newPos;
        }
        else
        {
            GetComponent<Animator>().enabled = true; ;
        }
        
    }
}
