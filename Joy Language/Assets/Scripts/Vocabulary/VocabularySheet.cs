using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Random = System.Random;

public class VocabularySheet
{
    public Dictionary<string, List<Vocabulary>> EasySheet { get; }
    public Dictionary<string, List<Vocabulary>> MediumSheet { get; }
    public Dictionary<string, List<Vocabulary>> HardSheet { get; }
    public Dictionary<string, int> ExpMap { get; }

    // Read the vocabulary from a json file.
    public VocabularySheet(string fileLocation)
    {
        EasySheet = new Dictionary<string, List<Vocabulary>>();
        MediumSheet = new Dictionary<string, List<Vocabulary>>();
        HardSheet = new Dictionary<string, List<Vocabulary>>();
        ExpMap = new Dictionary<string, int>();

        // read the target file as chinese char
        string upStr = File.ReadAllText(fileLocation, Encoding.GetEncoding("gb2312"));

        // Read Json file of the vocabulary.
        List<Vocabulary> vLst = JsonConvert.DeserializeObject<List<Vocabulary>>(upStr);
        foreach (Vocabulary v in vLst)
        {
            Dictionary<string, List<Vocabulary>> vSheet;
            if (v.Spelling.Length <= 4)
            {
                vSheet = EasySheet;
            } else if (v.Spelling.Length <= 8 && v.Spelling.Length > 4)
            {
                vSheet = MediumSheet;
            } else
            {
                vSheet = HardSheet;
            }

            // Check if it is the first time add the vocabulary.
            if (vSheet.ContainsKey(v.Spelling))
            {
                vSheet[v.Spelling].Add(v);
            } else
            {
                vSheet.Add(v.Spelling, new List<Vocabulary>() { v });
                ExpMap.Add(v.Spelling, 0);
            }
        }
    }

    // Generate the a vocabulary list to study based on the difficulty.
    public List<Vocabulary> GetVList(char difficulty, int length)
    {
        // TODO: For now, just get 5 word randomly.
        Dictionary<string, List<Vocabulary>> vSheet;
        if (difficulty == 'E')
        {
            vSheet = EasySheet;
        } else if (difficulty == 'M')
        {
            vSheet = MediumSheet;
        } else
        {
            vSheet = HardSheet;
        }

        List<Vocabulary> result = new List<Vocabulary>();
        int size = length;
        int sheetSize = vSheet.Count;
        if (sheetSize <= size)
        {
            foreach (List<Vocabulary> vLst in vSheet.Values)
            {
                result.AddRange(vLst);
            }
        } else
        {
            List<List<Vocabulary>> vLst = vSheet.Values.ToList();
            // random pick
            Random rand = new Random();
            int idx = 0;
            // not duplicate
            List<int> picked = new List<int>();

            while (size > 0){
                // we need the CN is unique from the EN
                idx = rand.Next(sheetSize);
                while (!checkPicked(picked, idx)) {
                    idx = rand.Next(sheetSize);
                }
                // add to the pick if unique
                picked.Add(idx);


                if (vLst[idx].Count == 1) {
                    result.Add(vLst[idx][0]);
                }
                else {
                    int idx2 = rand.Next(vLst[idx].Count);
                    result.Add(vLst[idx][idx2]);
                }
                
                // refresh the index
                size--;
                idx++;
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
    public List<Vocabulary> FindVocabulary(string v)
    {
        if (EasySheet.ContainsKey(v))
        {
            return EasySheet[v];
        } else if (MediumSheet.ContainsKey(v))
        {
            return MediumSheet[v];
        } else if (HardSheet.ContainsKey(v)) {

            return HardSheet[v];
        }

        return null;
    }

    public void UpdateExp(string spelling, int score)
    {
        ExpMap[spelling] += score;
    }
}
