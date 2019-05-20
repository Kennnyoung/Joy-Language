using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabularyManager : MonoBehaviour
{
    public VocabularySheet vSheet;

    // Start is called before the first frame update
    void Start()
    {
        vSheet = new VocabularySheet("Assets/Scripts/Vocabulary/word_test.json");
    }

    // Generate choice for the user.
    public (List<string>, List<string>) popQuestion(char difficulty)
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

    // For Test.
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            (List<string>, List<string>) test = popQuestion('E');
            Debug.Log(test.Item1);
            Debug.Log(test.Item2);
        }
    }
}
