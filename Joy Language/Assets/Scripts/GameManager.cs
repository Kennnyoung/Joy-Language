using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Question;
    public TextMeshProUGUI ChoiceOne;
    public TextMeshProUGUI ChoiceTwo;
    public TextMeshProUGUI ChoiceThree;

    // Start is called before the first frame update
    void Start()
    {
        Question.text = "This bed sheet has multiple _____";
        ChoiceOne.text = "layers";
        ChoiceTwo.text = "steps";
        ChoiceThree.text = "levels";
        ChoiceOne.tag = "Correct Answer";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AnswerCorrect()
    {
        Reset();
    }

    public void AnswerWrong()
    {
        Reset();
    }


    public void DisableCollider()
    {
        ChoiceOne.GetComponent<Collider2D>().enabled = (ChoiceOne.tag == "Correct Answer") ? true : false;
        ChoiceTwo.GetComponent<Collider2D>().enabled = (ChoiceTwo.tag == "Correct Answer") ? true : false;
        ChoiceThree.GetComponent<Collider2D>().enabled = (ChoiceThree.tag == "Correct Answer") ? true : false;
    }

    public void Reset()
    {
        ChoiceOne.enabled = false;
        ChoiceTwo.enabled = false;
        ChoiceThree.enabled = false;
    }
}
