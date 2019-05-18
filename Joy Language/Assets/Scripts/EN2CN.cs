using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System.Text;

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
    void Start()
    {

        //string input = File.ReadAllText("./Assets/Scripts/Vocabulary/word_test.json", Encoding.GetEncoding("gb2312"));
        //List<Vocabulary> vLst = JsonConvert.DeserializeObject<List<Vocabulary>>(input);
        //foreach (Vocabulary v in vLst){
        //    v.printV();
        //}

        VocabularySheet test = new VocabularySheet("./Assets/Scripts/Vocabulary/word_test.json");
        



        //VocabularySheet test = new VocabularySheet("./Assets/Scripts/Vocalbulary/word.json");
        List<Vocabulary> list = test.GetVList('M');
        for(int i = 0; i < list.Count; i++) {
           list[i].printV();
        }

        choice1.onClick.AddListener(delegate { userPick(choice1); });
        choice2.onClick.AddListener(delegate { userPick(choice2); });
        choice3.onClick.AddListener(delegate { userPick(choice3); });
        choice4.onClick.AddListener(delegate { userPick(choice4); });

        // i will get the correct CN here
        correct = list[0].Meaning;
        // set the choice
        choice1.GetComponentInChildren<Text>().text = list[0].Meaning;
        choice2.GetComponentInChildren<Text>().text = list[1].Meaning;
        choice3.GetComponentInChildren<Text>().text = list[2].Meaning;
        choice4.GetComponentInChildren<Text>().text = list[3].Meaning;
        // set the EN
        englishShow.text = list[0].Spelling;
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

    // to reset the checkAns text box
    void resetCheckAns() {
        checkAns.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
