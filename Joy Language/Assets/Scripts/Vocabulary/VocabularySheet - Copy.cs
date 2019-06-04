using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Random = System.Random;
using UnityEngine;



public class VocabularySheett {
    public List<Vocabulary> EasySheet { get; }
    public List<Vocabulary> MediumSheet { get; }
    public List<Vocabulary> HardSheet { get; }
    public Dictionary<string, int> ExpMap { get; }

    // Read the vocabulary from a json file.
    public VocabularySheett(string fileLocation) {
        EasySheet = new List<Vocabulary>();
        MediumSheet = new List<Vocabulary>();
        HardSheet = new List<Vocabulary>();
        ExpMap = new Dictionary<string, int>();

        // read the target file as chinese char
        //string upStr = File.ReadAllText(fileLocation, Encoding.GetEncoding("gb2312"));
        string upStr = File.ReadAllText(fileLocation);

        // Read Json file of the vocabulary.
        List<Vocabulary> vLst = JsonConvert.DeserializeObject<List<Vocabulary>>(upStr);
        foreach (Vocabulary v in vLst) {
            Dictionary<string, List<Vocabulary>> vSheet;
            if (v.Spelling.Length <= 5) {
                EasySheet.Add(v);
            }
            else if (v.Spelling.Length <= 10 && v.Spelling.Length > 5) {
                MediumSheet.Add(v);
            }
            else {
                HardSheet.Add(v);
            }
        }
        sortAllSheet();
    }

    public void sortAllSheet() {
        // sort sheet by proficient
        EasySheet.Sort(sortByProfi);
        MediumSheet.Sort(sortByProfi);
        HardSheet.Sort(sortByProfi);
    }

    static int sortByProfi(Vocabulary v1, Vocabulary v2) {
        return -1 * v1.Proficient.CompareTo(v2.Proficient);
    }

    public void printAll() {
        Debug.Log(EasySheet.Count);
        Debug.Log(MediumSheet.Count);
        Debug.Log(HardSheet.Count);

        Debug.Log("------Easy------");
        foreach (var v in EasySheet) {
            v.PrintV();
        }

        Debug.Log("------Medium------");
        foreach (var v in MediumSheet) {
            v.PrintV();
        }

        Debug.Log("------Hard------");
        foreach (var v in HardSheet) {
            v.PrintV();
        }
    }

    // Generate the a vocabulary list to study based on the difficulty.
    public List<Vocabulary> GetVList(char difficulty, int length) {

        // TODO: For now, just get 5 word randomly.
        List<Vocabulary> vSheet;
        if (difficulty == 'E') {
            vSheet = EasySheet;
        }
        else if (difficulty == 'M') {
            vSheet = MediumSheet;
        }
        else {
            vSheet = HardSheet;
        }

        List<Vocabulary> result = new List<Vocabulary>();
        int sheetSize = vSheet.Count;
        // if we are not enough
        // TODO in the canvas
        if (sheetSize <= length) {
            result = vSheet;
        }
        // randomize pick
        else {
            Random rand = new Random();
            // item in result list
            int curLength = 0;
            // index of the vsheet
            int index = 0;
            while (curLength < length) {
                int proba = rand.Next(100);
                //Debug.Log(index);
                if (proba < vSheet[index].Proficient) {
                    curLength++;
                    result.Add(vSheet[index]);
                }

                index++;
                if (index >= 100) index -= 100;
            }
        }

        return result;
    }

    // because the required list is only 4 slots I decide use stupid method
    private bool checkPicked(List<int> vList, int input) {
        for (int i = 0; i < vList.Count; i++) {
            if (vList[i] == input) {
                return false;
            }
        }
        return true;
    }

    // Search for definition a given vocabulary.
    public List<Vocabulary> FindVocabulary(string v) {
        //if (EasySheet.ContainsKey(v))
        //{
        //    return EasySheet[v];
        //} else if (MediumSheet.ContainsKey(v))
        //{
        //    return MediumSheet[v];
        //} else if (HardSheet.ContainsKey(v)) {

        //    return HardSheet[v];
        //}

        return null;
    }

    public void writeBack() {
        //string upStr = File.ReadAllText("./Assets/Scripts/Vocabulary/word_test.json");

        // Read Json file of the vocabulary.
        //List<Vocabulary> vLst = JsonConvert.DeserializeObject<List<Vocabulary>>(upStr);

        //var rand = new Random();

        //for(int i = 0; i <vLst.Count; i++) {
        //    vLst[i].Proficient = rand.Next(100);
        //}

        // sort again before write back
        sortAllSheet();

        // add all together
        List<Vocabulary> temp = new List<Vocabulary>();
        temp.AddRange(EasySheet);
        temp.AddRange(MediumSheet);
        temp.AddRange(HardSheet);

        string str = JsonConvert.SerializeObject(temp);
        File.WriteAllText("./Assets/Scripts/Vocabulary/word_test.json", str);
    }

    public void UpdateExp(string spelling, int score) {
        ExpMap[spelling] += score;
    }
}