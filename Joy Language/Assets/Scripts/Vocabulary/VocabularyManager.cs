using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabularyManager : MonoBehaviour
{
    public VocabularySheet vSheet;

    // Start is called before the first frame update
    void Start()
    {
        vSheet = new VocabularySheet("./Assets/Scripts/Vocabulary/word_test.json");
    }

    // Generate choice for the user.
    public (List<string>, List<string>) PopQuestionCtE(char difficulty)
    {
        List<Vocabulary> vLst = vSheet.GetVList(difficulty);

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

        return (question, choices);
    }

    public (string, List<List<string>>) PopQuestionEtC(char difficulty)
    {
        List<Vocabulary> vLst = vSheet.GetVList(difficulty);

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

        return (question, choices);
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
