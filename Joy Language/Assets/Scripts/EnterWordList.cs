using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWordList : MonoBehaviour
{
    public int DiffcultyLevel;
    public GameObject LevelChoice;
    public GameObject UnitChoice;

    public void EnterLevel()
    {
        LoadData();
    }

    private void LoadData()
    {
        //Load data here, after it finishes call back to show selected level

        LevelChoice.SetActive(false);
        UnitChoice.SetActive(true);

    }
}
