using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChipCollection : MonoBehaviour
{
    public static int NumberOfChipsCollected;
    public static bool DuplicateDrop;

    private static List<int> AllItems;


    [SerializeField] int ChipLimit;

    private void Update()
    {
        if (NumberOfChipsCollected == ChipLimit)
        {
            DropItem();
        }
    }

    public int DropItem()
    {
        int ItemIndex = Random.Range(0, AllItems.Count-1);
        int Item = AllItems[ItemIndex];
        if (GameManager.GameChips.Contains(Item))
        {
            NumberOfChipsCollected = (int)Mathf.Ceil(0.5f * NumberOfChipsCollected);
            DuplicateDrop = true;
        }
        else
        {
            DuplicateDrop = false;
            GameManager.GameChips.Add(Item);
        }

        return Item;
        
    }
}
