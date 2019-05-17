using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EN2CN : MonoBehaviour
{
    [SerializeField] Button choice1;
    [SerializeField] Button choice2;
    [SerializeField] Button choice3;
    [SerializeField] Button choice4;
    [SerializeField] Text checkAns;

    string correct;

    // Start is called before the first frame update
    void Start()
    {
        //VocabularySheet test = new VocabularySheet("./Assets/Scripts/Vocalbulary/word.json");
        //print(test.GetVList('E'));
        choice1.onClick.AddListener(delegate { userPick(choice1); });
        choice2.onClick.AddListener(delegate { userPick(choice2); });
        choice3.onClick.AddListener(delegate { userPick(choice3); });
        choice4.onClick.AddListener(delegate { userPick(choice4); });

        // i will get the correct CN here
        correct = choice1.GetComponentInChildren<Text>().text;
    }

    void userPick(Button clicked) {
        print(clicked.GetComponentInChildren<Text>().text);
        string userAns = clicked.GetComponentInChildren<Text>().text;
        if (userAns == correct) {
            print("Correct!");
            checkAns.text = "Correct";
        }
        else {
            print("False!");
            checkAns.text = "False";
        }
        Invoke("resetCheckAns", 2);
    }

    // to reset the checkAns text box
    void resetCheckAns() {
        checkAns.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
