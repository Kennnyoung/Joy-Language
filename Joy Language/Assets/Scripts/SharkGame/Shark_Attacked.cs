using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Attacked : MonoBehaviour
{
    [SerializeField] GameObject eye;
    [SerializeField] float delay;

    Coroutine timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (timer != null) {
            StopCoroutine(timer);
        }

        eye.SetActive(true);
        timer = StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(delay);
        eye.SetActive(false);
    }
}
