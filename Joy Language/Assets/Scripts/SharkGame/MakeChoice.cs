using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeChoice : MonoBehaviour
{
    public Transform text;
    GameObject gameManager;
    Button thisButton;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestChoice()
    {
        gameObject.GetComponent<Button>().enabled = false;
        //text.transform.parent = GameObject.Find("Star").transform;
        bool test = (gameObject.tag == "Correct Answer") ? true : false;
        gameObject.tag = "Current Answer";
        gameManager.SendMessage("TestChoice", test);
    }
}
