using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChipCollection : MonoBehaviour
{
    [SerializeField] int NumberOfItems;

    private static Dictionary<int, List<int>> Chips = new Dictionary<int, List<int>>(); //A Dictionary that contains current droppable items and their chips <item, chips>
    private static int MinItem = 0;
    private static int MinLength = 100000;
    private static int MaxItem;

    public (int, int) GetChipIndex()
    {
        if (MinItem == 0)
        {
            MaxItem = NumberOfItems - 1;
        }
        //if chips dictionary is empty which is the intial state, we add NumberOfItems into it
        if (Chips.Count == 0)
        {
            int CurrentItem = GameManager.NumberOfUnitRecited / 10;
            for (int i= 0; i < NumberOfItems; i++)
            { 
                Chips[CurrentItem + i] = new List<int> { 0, 1, 2 };
            }
        }

        //if chips dictionary count is less than NumberOfItems which means one Item has dropped all its chips and has been removed
        //then we add next item in queue into the dictionary
        if (Chips.Count < NumberOfItems)
        {
            Chips[MaxItem + 1] = new List<int> { 0, 1, 2 };
            MinItem++;
            MaxItem++;
            MinLength = 100000;
        }

        List<int> Items = new List<int>(Chips.Keys);

       //get the top priority item (MinItem) and the lowest priority item (MaxItem) to get a range
       //MinLength is used to decide when should we drop chips from the top priority item
        for (int i = 0; i < Items.Count; i++){
            if (Chips[Items[i]].Count < MinLength)
            {
                MinLength = Chips[Items[i]].Count;
            }
        }

        //if top priority item has bigger or equal number of chips than the min chip length among all items
        //we drop chips from the top priority item, since we want it to drop all the chips first before all other items
        if (Chips[MinItem].Count > MinLength * 1.5 || MinLength == 1)
        {
            int ChipIndex = Random.Range(0, Chips[MinItem].Count - 1);
            int Chip = Chips[MinItem][ChipIndex];
            Chips[MinItem].Remove(Chip);
            if (Chips[MinItem].Count == 0)
            {
                Chips.Remove(MinItem);
            }
            print("Min Chip");
            return (MinItem, Chip);
        }
        //otherwise we random a drop 
        else
        {
            int ItemIndex = Random.Range(MinItem, MaxItem);
            int ChipIndex = Random.Range(0, Chips[ItemIndex].Count - 1);
            int Chip = Chips[ItemIndex][ChipIndex];
            Chips[ItemIndex].Remove(Chip);
            print("Random chip");
            return (ItemIndex, Chip);
        }
    }
}
