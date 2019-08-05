using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ListCount {
    public int easy;
    public int medium;
    public int hard;

    public ListCount(int e, int m, int h) {
        easy = e;
        medium = m;
        hard = h;
    }
}

public class LevelDetail {
    public int level;
    public int complete;

    public LevelDetail(int l, int c) {
        level = l;
        complete = c;
    }
}

public class UnitSelection : MonoBehaviour
{
    [SerializeField] Button unit;
    [SerializeField] GameObject panel;
    [SerializeField] Sprite fullStart;
    [SerializeField] Sprite emptyStart;
    [SerializeField] GameObject en2cn;

    public static List<LevelDetail> allLevel;
    [SerializeField] List<Button> levelButtons = new List<Button>();
    public int currentLevel = 1;

    // animation
    [SerializeField] float interval;
    [SerializeField] float duration;
    [SerializeField] float acceleration;
    [SerializeField] float minDuration;
    // audio
    AudioSource audio;
    [SerializeField] AudioClip bubble;
    void Awake(){
        audio = GetComponent<AudioSource>();
        string upStr = File.ReadAllText("./Assets/Scripts/Vocabulary/list_count.json", Encoding.GetEncoding("gb2312"));
        ListCount count = JsonConvert.DeserializeObject<ListCount>(upStr);
        //print(count.medium);

        upStr = File.ReadAllText("./Assets/Scripts/Vocabulary/level_detail.json", Encoding.GetEncoding("gb2312"));
        List<LevelDetail> levelDetails = JsonConvert.DeserializeObject<List<LevelDetail>>(upStr);
        allLevel = levelDetails;

        updateLevelScore(1, 1);

        for (int i = 0; i < levelDetails.Count; i++) {
            // add button
            Button temp = Instantiate(unit) as Button;
            temp.GetComponentInChildren<Text>().text = "" + levelDetails[i].level;
            temp.transform.SetParent(panel.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.onClick.AddListener(delegate { userOnclick(temp); });
            // keep track all button
            levelButtons.Add(temp);

            int levelCompelete = levelDetails[i].complete;
            for(int j = 1; j < 4; j++) {
                GameObject start = temp.transform.GetChild(j).gameObject;
                //print(start);
                if(j <= levelCompelete) start.GetComponent<Image>().sprite = fullStart;
                else start.GetComponent<Image>().sprite = emptyStart;
            }
        }
    }

    // store the current level user pick
    void userOnclick(Button eventObj) {
        currentLevel = int.Parse(eventObj.GetComponentInChildren<Text>().text);
        EN2CN temp = en2cn.GetComponent<EN2CN>();
        temp.newLevelIn();
    }

    public void saveComplete(int completeness) {
        allLevel[currentLevel-1].complete = completeness;
        string test = JsonConvert.SerializeObject(allLevel);
        rebuildLevel(completeness);
        File.WriteAllText("./Assets/Scripts/Vocabulary/level_detail.json", test);
    }

    // give the api to update level complete score base on level number
    static public void updateLevelScore(int completeness, int level){
        string upStr = File.ReadAllText("./Assets/Scripts/Vocabulary/level_detail.json", Encoding.GetEncoding("gb2312"));
        List<LevelDetail> levelDetails = JsonConvert.DeserializeObject<List<LevelDetail>>(upStr);

        // index 0 is level 1
        levelDetails[level-1].complete = completeness;
        string test = JsonConvert.SerializeObject(levelDetails);
        File.WriteAllText("./Assets/Scripts/Vocabulary/level_detail.json", test);
    }

    void rebuildLevel(int levelComplete) {
        Button temp = levelButtons[currentLevel-1];
        for (int j = 1; j < 4; j++) {
            GameObject start = temp.transform.GetChild(j).gameObject;
            //print(start);
            if (j <= levelComplete) start.GetComponent<Image>().sprite = fullStart;
            else start.GetComponent<Image>().sprite = emptyStart;
        }
    }


    // Animations
    private void OnEnable()
    {
        StartCoroutine(EnableAnimation());
    }

    IEnumerator EnableAnimation()
    {
        float tempDuration = duration;
        for (int i = 0; i < levelButtons.Count; i++)
        {
            yield return new WaitForSeconds(interval);
            
            iTween.ScaleTo(levelButtons[i].gameObject, iTween.Hash("scale", new Vector3(1,1,1), "time", tempDuration, "easetype", "easeInOutBounce"));
            audio.PlayOneShot(bubble, 0.8f);
            tempDuration -= acceleration;
            tempDuration = tempDuration > minDuration ? tempDuration : minDuration;
            yield return new WaitForSeconds(tempDuration);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            iTween.ScaleTo(levelButtons[i].gameObject, new Vector3(0, 0, 0), .01f);
        }
    }
}
