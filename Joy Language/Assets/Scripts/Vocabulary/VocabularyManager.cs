using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabularyManager : MonoBehaviour
{
    public VocabularySheet vSheet;

    public int combo;

    private string curVoc;

    // Start is called before the first frame update
    void Awake()
    {
        vSheet = new VocabularySheet("./Assets/Scripts/Vocabulary/dentistry.json");
        combo = 0;
    }

    // Generate choice for the user.
    public (List<string>, List<string>) PopQuestionCtE(char difficulty)
    {
        List<Vocabulary> vLst = vSheet.GetVList(difficulty, 5);

        List<string> choices = new List<string>();
        foreach (Vocabulary v in vLst)
        {
            if (!choices.Contains(v.Spelling))
            {
                choices.Add(v.Spelling);
            }
        }

        List<string> question = new List<string>();
        foreach (Vocabulary v in vSheet.FindVocabulary(choices[0]))
        {
            question.Add(v.Meaning);
        }

        curVoc = vLst[0].Spelling;

        return (question, choices);
    }

    public (string, List<List<string>>) PopQuestionEtC(char difficulty)
    {
        List<Vocabulary> vLst = vSheet.GetVList(difficulty, 5);

        string question = vLst[0].Spelling;

        List<List<string>> choices = new List<List<string>>();
        foreach (Vocabulary v in vLst)
        {
            List<string> choice = new List<string>();
            foreach (Vocabulary voc in vSheet.FindVocabulary(v.Spelling))
            {
                choice.Add(voc.Meaning);
            }

            if (!choices.Contains(choice))
            {
                choices.Add(choice);
            }
        }

        curVoc = vLst[0].Spelling;

        return (question, choices);
    }

    public void Pass(bool isPass)
    {
        if (isPass)
        {
            combo++;
            vSheet.UpdateExp(curVoc, 1);
        } else
        {
            //combo = 0;
            vSheet.UpdateExp(curVoc, -1);
        }
    }

    // For Test.
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            (List<string>, List<string>) test = PopQuestionCtE('E');
            foreach (string meaning in test.Item1)
            {
                Debug.Log(meaning);
            }
            foreach (string choice in test.Item2)
            {
                Debug.Log(choice);
            }
        }
    }
}
