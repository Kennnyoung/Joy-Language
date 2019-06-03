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
    public GameObject LevelDetail;
    public GameObject currentCanvas;

    string correct;
    int correctTimes;
    int totalTime = 3;
    int curTimes = 1;
    UnitSelection allLevel; 

    // Start is called before the first frame update
    void Start(){
        newLevelIn();

        choice1.onClick.AddListener(delegate { userPick(choice1); });
        choice2.onClick.AddListener(delegate { userPick(choice2); });
        choice3.onClick.AddListener(delegate { userPick(choice3); });
        choice4.onClick.AddListener(delegate { userPick(choice4); });
    }

    void userPick(Button clicked) {
        //print(clicked.GetComponentInChildren<Text>().text);
        string userAns = clicked.GetComponentInChildren<Text>().text;
        if (userAns == correct) {
            checkAns.text = "Correct";
            correctTimes++;
        }
        else {
            checkAns.text = "False";
        }

        if(curTimes == totalTime) {
            checkAns.text = "Finish!";

            //mark the level as we complete
            allLevel.saveComplete(correctTimes);

            Invoke("goBack2Unit", 0.1f);
        }
        else {
            curTimes++;
            //disable for a whihle
            disableAllButton();
            Invoke("reloadData", 0.1f);
        }
    }

    void printAll(VocabularySheet test) {
        print(test.EasySheet.Count);
        print(test.MediumSheet.Count);
        print(test.HardSheet.Count);

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

        //for (int i = 0; i < 4; i++) print(list[i]);
        return list;
    }

    // to reset the checkAns text box
    //void resetCheckAns() {
    //    reloadData();
    //}

    void goBack2Unit() {
        currentCanvas.SetActive(false);
        LevelDetail.SetActive(true);
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

    // this function called when new level is in
    public void newLevelIn() {
        // get the level detail
        UnitSelection temp = LevelDetail.GetComponent<UnitSelection>();
        allLevel = temp;
        print(allLevel.currentLevel);

        // reset all count
        correctTimes = 0;
        curTimes = 1;

        // now refresh the all data
        reloadData();
    }

    // this function called everytime user pick ans
    void reloadData() {
        // get list
        VocabularySheet test = new VocabularySheet("./Assets/Scripts/Vocabulary/word_test.json");
        List<Vocabulary> list = test.GetVList('M', 4);
        //printAll(test);

        List<int> rlist = randomizedArr();
        Random rand = new Random();
        int correctIndex = rand.Next(3);
        // i will get the correct CN here
        correct = list[correctIndex].Spelling;
        // set the EN
        englishShow.text = list[correctIndex].Meaning;
        checkAns.text = "";

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
