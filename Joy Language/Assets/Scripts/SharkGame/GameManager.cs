using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // animation
    public float enterAnimationDuration;
    public Transform star;
    public Transform shark;
    public Transform starBody;
    public Transform optionVanishParticle;
    public Transform comboIcon;
    [SerializeField] int NumberOfWordPerUnit;
    public int NumOfQuest;
    public int Rank;
    public static int NumberOfUnitRecited = 0;
    private static int NumberOfWordRecited = 0;
    public static Dictionary<int, List<int>> StoryChips = new Dictionary<int, List<int>>();
    public static List<int> GameChips = new List<int>();

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
        NumOfQuest = 0;
        Rank = 3;

        vManger = vocManager.GetComponent<VocabularyManager>();
        StartCoroutine(StartLevel());
        // generate first question
        GetNewQuestCtE();
        questionFader.FadeIn();
        GenerateOptions();
    }

    // Get New question (CtE).
    void GetNewQuestCtE()
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

    void GetNewQuestEtC()
    {
        (string, List<List<string>>) quest = vManger.PopQuestionEtC('E');

        question.text = quest.Item1;
        answers[0] = string.Join(";", quest.Item2[0]);
        answers[1] = string.Join(";", quest.Item2[1]);
        answers[2] = string.Join(";", quest.Item2[2]);

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
        yield return new WaitForSeconds(0.7f);
        GetNewQuestCtE();
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
        NumOfQuest++;
        switch (answerCheck)
        {
            case true:
                print("Right");
                starBody.SendMessage("Attack");

                NumberOfWordRecited++;

                if (NumberOfWordRecited == NumberOfWordPerUnit)
                {
                    NumberOfUnitRecited++;
                    StoryChipCollection scc = gameObject.GetComponent<StoryChipCollection>();
                    (int, int) chip = scc.GetChipIndex();
                    print("chip:" + chip);
                    if (NumOfQuest <= NumberOfWordPerUnit + 2)
                    {
                        Rank = 3;
                    } else if (NumOfQuest <= NumberOfWordPerUnit + 5)
                    {
                        Rank = 2;
                    } else
                    {
                        Rank = 1;
                    }
                    print("Your Rank: " + Rank);

                    NumberOfWordRecited = 0;
                }

                break;
            case false:
                print("Wrong");
                starBody.SendMessage("BackToIdle");
                break;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
