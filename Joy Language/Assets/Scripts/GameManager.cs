using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Question;

    // Start is called before the first frame update
    void Start()
    {
        Question.text = "This bed sheet has multiple _____";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
