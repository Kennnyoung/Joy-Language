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
    public Transform optionVanishParticle;

    UIFader questionFader;
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

    [SerializeField] List<Transform> buttons;
    List<Transform> targets;

    // answerCheck
    bool answerCheck;
    void Awake()
    {
        questionFader = GetComponent<UIFader>();

        targets = new List<Transform>();
        buttons = new List<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        vManger = vocManager.GetComponent<VocabularyManager>();
        StartCoroutine(StartLevel());
        // generate first question
        GetNewQuest();
        questionFader.FadeIn();
        GenerateOptions();
    }

    // Get New question.
    void GetNewQuest()
    {
        (List<string>, List<string>) quest = vManger.PopQuestionCtE('E');

        question.text = string.Join(";", quest.Item1);
        answers[0] = quest.Item2[0];
        answers[1] = quest.Item2[1];
        answers[2] = quest.Item2[2];

        int maxLen = 0;
        foreach (string ans in answers)
        {
            if (ans.Length > maxLen)
            {
                maxLen = ans.Length;
            }
        }
        question.text += "\n\n" + new string('_', maxLen);
    }

    IEnumerator SwitchQuestion()
    {
        buttons.Clear();
        questionFader.FadeOut();
        yield return new WaitForSeconds(0.6f);
        GetNewQuest();
        questionFader.FadeIn();
        yield return new WaitForSeconds(0.5f);
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

    void TestChoice(bool tempTest)
    {
        answerCheck = tempTest;
        vManger.Pass(answerCheck);

        for (int i=0; i < buttons.Count; i++)
        {
            Transform button = buttons[i];
            button.GetComponent<Button>().enabled = false;
            if (button.tag != "Current Answer")
                button.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OptionVanish()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Transform button = buttons[i];
            if (button.tag != "Current Answer")
            {
                Instantiate(optionVanishParticle, button.position, Quaternion.identity);
                Destroy(button.gameObject);
            }
        }
    }

    void AnswerFeedback(float tempDuration)
    {
        switch (answerCheck)
        {
            case true:
                print("Right");
                break;
            case false:
                print("Wrong");
                break;
        }
    }
}
