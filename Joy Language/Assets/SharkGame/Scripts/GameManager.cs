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

    // generate question
    public GameObject vocManager;
    private VocabularyManager vManger;

    public Text question;

    GameObject Button;
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
        vManger = vocManager.GetComponent<VocabularyManager>();
        StartCoroutine(StartLevel());
        GenerateQuestion();
    }

    // Get New question.
    void GetNewQuest()
    {
        (List<string>, List<string>) quest = vManger.PopQuestionCtE('E');

        question.text = string.Join(";", quest.Item1) + "\n_____";
        answers[0] = quest.Item2[0];
        answers[1] = quest.Item2[1];
        answers[2] = quest.Item2[2];
    }

    void GenerateQuestion()
    {
        GetNewQuest();
        GenerateOptions();
    }

    IEnumerator StartLevel()
    {
        star.SendMessage("Enter", enterAnimationDuration);
        shark.SendMessage("Enter", enterAnimationDuration);
        yield return new WaitForSeconds(enterAnimationDuration);
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
        vManger.Pass(tempTest);

        yield return null;
        for (int i=0; i < buttons.Count; i++)
        {
            Transform button = buttons[i];
            if (button.tag != "Current Answer")
            {

            }
        }
    }
}
