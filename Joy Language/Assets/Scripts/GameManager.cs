using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Question;
    public TextMeshProUGUI ChoiceOne;
    public TextMeshProUGUI ChoiceTwo;
    public TextMeshProUGUI ChoiceThree;

    // Start is called before the first frame update
    void Start()
    {
        Question.text = "This bed sheet has multiple _____";
        ChoiceOne.text = "layers";
        ChoiceTwo.text = "steps";
        ChoiceThree.text = "levels";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
