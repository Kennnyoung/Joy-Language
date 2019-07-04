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

    void Start()
    {
        image.type = Image.Type.Filled;
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

    //void Update()
    //{
    //    image.fillAmount = (float)currentAmount / amount; 
    //}
}
