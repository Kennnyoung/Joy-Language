using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fragments : MonoBehaviour
{
    public int amount;
    [Range(0, 10)] public int currentAmount;
    public Image image;
    public Image background;
    public Button btn;
    public bool isCompleted;
    public int CardID;

    void Start()
    {
        currentAmount = 0;
        Display();
    }

    void Display()
    {
        isCompleted = currentAmount == amount ? true : false;

        if (!isCompleted)
        {
            image.fillAmount = (float)currentAmount / amount;
        }
        else
        {
            image.fillAmount = 1f;
            background.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.StoryChips != null)
        {
            if (GameManager.StoryChips.ContainsKey(CardID))
            {
                currentAmount = GameManager.StoryChips[CardID].Count;
            }
        }
        image.fillAmount = (float)currentAmount / amount;
    }
}
