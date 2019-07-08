using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public GameObject btnObj;
    public GameObject menu;
    public Sprite expan;
    public Sprite back;
    Button btn;
    bool isshow = false;
    // Use this for initialization
    void Start()
    {
        menu.SetActive(isshow);
        btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            isshow = !isshow;
            menu.SetActive(isshow);
            if (isshow)
            {
                btn.GetComponent<Image>().sprite = expan;
            }
            else
            {
                btn.GetComponent<Image>().sprite = back;
            }
        });
    }
}
