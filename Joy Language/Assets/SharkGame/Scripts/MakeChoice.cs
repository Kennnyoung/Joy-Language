﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeChoice : MonoBehaviour
{
    GetBlankPosition blankPosition;
    GameObject gameManager;
    Button thisButton;

    public Transform starTarget;
    public float moveDuration;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        thisButton = gameObject.GetComponent<Button>();
        blankPosition = GameObject.Find("Question").GetComponent<GetBlankPosition>();
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

    void MoveToBlank()
    {
            Vector3 aim = blankPosition.GetPos();
            iTween.MoveTo(gameObject, iTween.Hash("position", aim, "time", moveDuration, "easetype", "easeOutExpo"));   
    }
}