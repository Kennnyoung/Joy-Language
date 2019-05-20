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


    GameObject button;
    public Transform canvas;
    public Transform choice;
    public Transform spawnPosition;

    public string[] answers;
    public Transform[] choicePositions;

    List<Transform> targets;
    void Awake()
    {
        targets = new List<Transform>();
        GenerateChoices();
    }
    // Start is called before the first frame update
    void Start()
    {
        Question.text = "This bed sheet has multiple _____";
        //ChoiceOne.text = "layers";
        //ChoiceTwo.text = "steps";
        //ChoiceThree.text = "levels";
        //ChoiceOne.tag = "Correct Answer";
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}


    //public void AnswerCorrect()
    //{
    //    Reset();
    //}

    //public void AnswerWrong()
    //{
    //    Reset();
    //}


    //public void DisableCollider()
    //{
    //    ChoiceOne.GetComponent<Collider2D>().enabled = (ChoiceOne.tag == "Correct Answer") ? true : false;
    //    ChoiceTwo.GetComponent<Collider2D>().enabled = (ChoiceTwo.tag == "Correct Answer") ? true : false;
    //    ChoiceThree.GetComponent<Collider2D>().enabled = (ChoiceThree.tag == "Correct Answer") ? true : false;
    //}

    //public void Reset()
    //{
    //    ChoiceOne.enabled = false;
    //    ChoiceTwo.enabled = false;
    //    ChoiceThree.enabled = false;
    //}
    
    void GenerateChoices()
    {
        // get target positions
        for (int j = 0; j < choicePositions.Length; j++)
        {
            targets.Add(choicePositions[j]);
        }
        for (int i = 0; i < 3; i++)
        {
            //generate a button
            Transform button = Instantiate(choice, spawnPosition.position, Quaternion.identity, canvas);
            button.name = "choice" + (i + 1);
            //the first element in the array is the correct answer
            if(i == 0)
            {
                button.tag = "Correct Answer";
            }
            //set choice text
            button.Find("Text").GetComponent<Text>().text = answers[i];
            
            //random arrange buttons
            int n = Random.Range(0, targets.Count);
            button.SendMessage("SetAim", targets[n].position);
            targets.RemoveAt(n);
        }
    }
}
