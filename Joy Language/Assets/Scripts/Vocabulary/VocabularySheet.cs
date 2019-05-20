using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Random = System.Random;

public class VocabularySheet
{
    public Dictionary<string, List<Vocabulary>> EasySheet { get; }
    public Dictionary<string, List<Vocabulary>> MediumSheet { get; }
    public Dictionary<string, List<Vocabulary>> HardSheet { get; }

    // Read the vocabulary from a json file.
    public VocabularySheet(string fileLocation)
    {
        EasySheet = new Dictionary<string, List<Vocabulary>>();
        MediumSheet = new Dictionary<string, List<Vocabulary>>();
        HardSheet = new Dictionary<string, List<Vocabulary>>();

        // Read Json file of the vocabulary.
        List<Vocabulary> vLst = JsonConvert.DeserializeObject<List<Vocabulary>>(fileLocation);
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
            }
        }
    }

    // Generate the a vocabulary list to study based on the difficulty.
    public List<Vocabulary> GetVList(char difficulty)
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
        int size = 5;
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
            Random rand = new Random();
            int idx = 0;
            while (size > 0)
            {
                if (rand.Next(sheetSize - idx) < size)
                {
                    result.AddRange(vLst[idx]);
                    size--;
                }

                idx++;
            }
        }

        return result;
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
}
