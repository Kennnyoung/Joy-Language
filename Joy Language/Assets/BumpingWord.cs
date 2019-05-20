using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpingWord : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager GM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GM.DisableCollider();
        yield return new WaitForSeconds(0.5f);
        GameObject choice = collision.gameObject;
        if (choice.tag == "Correct Answer")
        {
            GM.AnswerCorrect();
        }
        else
        {
            GM.AnswerWrong();
        }
    }
}
