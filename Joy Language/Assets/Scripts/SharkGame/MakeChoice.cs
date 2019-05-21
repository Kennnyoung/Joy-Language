using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeChoice : MonoBehaviour
{
    GameObject gameManager;
    Button thisButton;

    public Transform starTarget;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        thisButton = gameObject.GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(TestChoice);
    }

    void TestChoice()
    {
        gameObject.GetComponent<Button>().enabled = false;
        bool test = (gameObject.tag == "Correct Answer") ? true : false;
        gameManager.SendMessage("TestChoice", test);
        gameObject.tag = "Current Answer";
        PlayAnimation();
    }

    void PlayAnimation()
    {
        GameObject star = GameObject.Find("Star");
        star.SendMessage("MakeChoice", starTarget);
    }
}
