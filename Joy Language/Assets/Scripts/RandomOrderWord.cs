using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

// this is for user picked button and store its position
class BottonPos {
    public Button button;
    public int pos;
    
    public BottonPos(Button b, int p) {
        button = b;
        pos = p;
    }
}

public class RandomOrderWord : MonoBehaviour
{
    [SerializeField] Text CN;
    [SerializeField] GameObject unorderButton;
    [SerializeField] GameObject pickedList;
    [SerializeField] Button sampleButton;
    [SerializeField] Text ansCheck;

    // maping the string onto buttons
    List<char> userPickeds = new List<char>();
    List<BottonPos> pickedButtons = new List<BottonPos>();
    int currentPos = 0;

    string correctAns;

    // Start is called before the first frame update
    void Start(){
        VocabularySheet allWords = new VocabularySheet("./Assets/Scripts/Vocabulary/word_test.json");
        Vocabulary picked = allWords.GetVList('M', 1)[0];
        correctAns = picked.Spelling;
        picked.printV();

        // set up the question
        CN.text = picked.Meaning;
        createChoice(picked.Spelling);
        initPickedList(picked.Spelling);
    }

    void createChoice(string spell) {
        List<int> l = randomizedArr(spell.Length);
        for (int i = 0; i < spell.Length; i++) {
            Button test = Instantiate(sampleButton) as Button;
            test.transform.SetParent(unorderButton.transform);
            test.transform.localPosition = Vector3.zero;

            // add the text
            test.GetComponentInChildren<Text>().text = "" + spell[l[i]];
            // add onclick
            test.onClick.AddListener(delegate { userPick(test); });
        }
    }

    void userPick(Button wordPicked) {
        // if current pos is -1 then user is wrong need delete
        if (currentPos == -1) return;

        string currentLetter = wordPicked.GetComponentInChildren<Text>().text;
        userPickeds[currentPos] = currentLetter.ToCharArray()[0];
        //update picked list letters
        pickedButtons[currentPos].button.GetComponentInChildren<Text>().text = "" + currentLetter.ToCharArray()[0];
        // move the current pos to next _ empty one
        currentPos = userPickeds.IndexOf('_');


        //string temp = "";
        //for (int i = 0; i < userPickeds.Count; i++) temp += "" + userPickeds[i];
        //print(temp);
        //print(userPickeds.ToArray().ToString());

        //print(currentLetter.ToCharArray()[0] + " " + currentPos + " " + userPickeds.IndexOf('_'));

        // if no empty space means user pick all letters check ans
        if (currentPos == -1) {
            string tempy = "";
            for (int i = 0; i < userPickeds.Count; i++) tempy += "" + userPickeds[i];
            if (tempy == correctAns) {
                ansCheck.text = "Correct!";
                Invoke("reloadData", 0.1f);
            }
            else ansCheck.text = "FALSE";
        }
    }

    private List<int> randomizedArr(int length) {
        // init array
        List<int> list = new List<int>();
        for (int i = 0; i < length; i++) list.Add(i);

        // start randing
        Random rand = new Random();
        for (int i = 0; i < length; i++) {
            int idx = rand.Next(length);
            // switch
            int temp = list[i];
            list[i] = list[idx];
            list[idx] = temp;
        }
        return list;
    }

    void initPickedList(string spell) {
        for (int i = 0; i < spell.Length; i++) {
            Button test = Instantiate(sampleButton) as Button;
            test.transform.SetParent(pickedList.transform);
            test.transform.localPosition = Vector3.zero;
            BottonPos newItem = new BottonPos(test, i);

            // add to array for tracking
            pickedButtons.Add(newItem);

            // add the text
            test.GetComponentInChildren<Text>().text = "_";
            // add onclick
            test.onClick.AddListener(delegate { userdDepick(newItem); });

            // init the picked liest to lenght * _
            userPickeds.Add('_');
        }
    }

    void userdDepick(BottonPos wordDepicked) {
        wordDepicked.button.GetComponentInChildren<Text>().text = "_";
        userPickeds[wordDepicked.pos] = '_';
        // recalculate the position
        currentPos = userPickeds.IndexOf('_');
    }

    void reloadData() {
        // destory all the children
        for(int i = 0; i < unorderButton.transform.childCount; i++) {
            GameObject child = unorderButton.transform.GetChild(i).gameObject;
            Destroy(child);
        }

       
        for (int i = 0; i < pickedList.transform.childCount; i++) {
            var child = pickedList.transform.GetChild(i).gameObject;
            Destroy(child);
        }

        ansCheck.text = "";
        userPickeds = new List<char>();
        pickedButtons = new List<BottonPos>();
        currentPos = 0;

        // then reload everything
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
