using System.Collections;
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
    public float feedBackDuration;
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
        bool test = (gameObject.tag == "Correct Answer") ? true : false;
        gameObject.tag = "Current Answer";
        gameManager.SendMessage("TestChoice", test);
        PlayAnimation();
    }

    void PlayAnimation()
    {
        GameObject star = GameObject.Find("Star");
        star.SendMessage("MakeChoice", starTarget);
    }

    IEnumerator ChoiceAnimation()
    {
        Vector3 aim = blankPosition.GetPos();
        iTween.MoveTo(gameObject, iTween.Hash("position", aim, "time", moveDuration, "easetype", "easeOutExpo"));
        gameManager.SendMessage("OptionVanish");
        yield return new WaitForSeconds(moveDuration);
        gameManager.SendMessage("AnswerFeedback", feedBackDuration);
        yield return new WaitForSeconds(feedBackDuration);
        gameManager.SendMessage("SwitchQuestion");
        Destroy(gameObject);
    }
}
