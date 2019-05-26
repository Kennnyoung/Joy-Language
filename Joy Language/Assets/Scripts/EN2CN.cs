using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine.SceneManagement;

public class EN2CN : MonoBehaviour
{
    [SerializeField] Button choice1;
    [SerializeField] Button choice2;
    [SerializeField] Button choice3;
    [SerializeField] Button choice4;
    [SerializeField] Text englishShow;
    [SerializeField] Text checkAns;

    string correct;

    // Start is called before the first frame update
    void Start(){
        reloadData();

        choice1.onClick.AddListener(delegate { userPick(choice1); });
        choice2.onClick.AddListener(delegate { userPick(choice2); });
        choice3.onClick.AddListener(delegate { userPick(choice3); });
        choice4.onClick.AddListener(delegate { userPick(choice4); });
    }

    void userPick(Button clicked) {
        //print(clicked.GetComponentInChildren<Text>().text);
        string userAns = clicked.GetComponentInChildren<Text>().text;
        if (userAns == correct) {
            print("Correct!");
            checkAns.text = "Correct";

            //disable for a whihle
            disableAllButton();

            Invoke("resetCheckAns", 0.1f);
        }
        else {
            print("False!");
            checkAns.text = "False";
        }
        //Invoke("reloadScene", 1);
    }

    void printAll(VocabularySheet test) {
        print("------Easy------");
        foreach (var v in test.EasySheet) {
            print(v.Key);
            print(v.Value);
        }

        print("------Medium------");
        foreach (var v in test.MediumSheet) {
            print(v.Key);
            print(v.Value);
        }

        print("------Hard------");
        foreach (var v in test.HardSheet) {
            print(v.Key);
            print(v.Value);
        }
    }

    private List<int> randomizedArr() {
        // init array
        List<int> list = new List<int>();
        for (int i = 0; i < 4; i++)  list.Add(i);

        // start randing
        Random rand = new Random();
        for (int i = 0; i < 4; i++) {
            int idx = rand.Next(3);
            // switch
            int temp = list[i];
            list[i] = list[idx];
            list[idx] = temp;
        }

        for (int i = 0; i < 4; i++) print(list[i]);
        return list;
    }

    // to reset the checkAns text box
    void resetCheckAns() {
        checkAns.text = "";
        reloadScene();
    }

    // only for testing
    void reloadScene() {
        SceneManager.LoadScene(1);
    }

    void disableAllButton() {
        // disable all the button for now
        choice1.interactable = false;
        choice2.interactable = false;
        choice3.interactable = false;
        choice4.interactable = false;
    }

    void enableAllButton() {
        // disable all the button for now
        choice1.interactable = true;
        choice2.interactable = true;
        choice3.interactable = true;
        choice4.interactable = true;
    }

    void reloadData() {
        VocabularySheet test = new VocabularySheet("./Assets/Scripts/Vocabulary/word_test.json");
        List<Vocabulary> list = test.GetVList('M', 4);

        List<int> rlist = randomizedArr();
        Random rand = new Random();
        int correctIndex = rand.Next(3);
        // i will get the correct CN here
        correct = list[correctIndex].Spelling;
        // set the EN
        englishShow.text = list[correctIndex].Meaning;

        // set the choice
        choice1.GetComponentInChildren<Text>().text = list[rlist[0]].Spelling;
        choice2.GetComponentInChildren<Text>().text = list[rlist[1]].Spelling;
        choice3.GetComponentInChildren<Text>().text = list[rlist[2]].Spelling;
        choice4.GetComponentInChildren<Text>().text = list[rlist[3]].Spelling;

        // enable all after refresh
        enableAllButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
