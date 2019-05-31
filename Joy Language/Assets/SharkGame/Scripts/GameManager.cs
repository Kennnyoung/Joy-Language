using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // animation
    public float enterAnimationDuration;
    public Transform star;
    public Transform shark;
    public GameObject optionVanishParticle;

    // generate question
    public Text question;

    GameObject button;
    public Transform canvas;
    public Transform choice;
    public Transform spawnPosition;

    public string[] answers;
    public Transform[] choicePositions;

    List<Transform> buttons;
    List<Transform> targets;

    // answerCheck
    bool answerCheck;
    void Awake()
    {
        targets = new List<Transform>();
        buttons = new List<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        question.text = "This bed sheet has multiple ______ ";
        StartCoroutine(StartLevel());
    } 

    IEnumerator StartLevel()
    {
        star.SendMessage("Enter", enterAnimationDuration);
        shark.SendMessage("Enter", enterAnimationDuration);
        yield return new WaitForSeconds(enterAnimationDuration);
        GenerateOptions();
    }
    void GenerateOptions()
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
            buttons.Add(button);
            button.name = "choice" + (i + 1);
            //the first element in the array is the correct answer
            if(i == 0)
            {
                button.tag = "Correct Answer";
            }
            //set choice text
            button.Find("Text").GetComponent<TextMeshProUGUI>().text = answers[i];
            
            //random arrange buttons
            int n = Random.Range(0, targets.Count);
            button.SendMessage("SetAim", targets[n].position);
            targets.RemoveAt(n);
        }
    }

    IEnumerator TestChoice(bool tempTest)
    {
        answerCheck = tempTest;

        yield return null;
        for (int i=0; i < buttons.Count; i++)
        {
            Transform button = buttons[i];
            if (button.tag != "Current Answer")
            {
                Instantiate(optionVanishParticle, button.position,Quaternion.identity);
                GameObject.Destroy(button.gameObject);
            }
        }
    }
}
