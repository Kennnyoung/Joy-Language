using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Chasing : MonoBehaviour
{
    [SerializeField] float verticalOffset;
    [SerializeField] float chasingSpeed;
    Vector3 aim;

    [SerializeField] float knockBackDelay;
    [SerializeField] float knockBackDistance;
    bool knockBack;
    Coroutine timer;
    void SetAim(Vector3 tempAim)
    {
        aim = tempAim;
    }

    private void OnParticleCollision(GameObject other)
    {
        transform.position += Vector3.down * knockBackDistance;
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        knockBack = true;
        timer = StartCoroutine(BeatBack());
    }
    // Update is called once per frame
    void Update()
    {
        if(!knockBack)
        transform.position = Vector2.Lerp(transform.position, new Vector2(aim.x, aim.y - verticalOffset), chasingSpeed * Time.deltaTime);
    }

    IEnumerator BeatBack()
    {
        yield return new WaitForSeconds(knockBackDelay);
        knockBack = false;
    }
}
