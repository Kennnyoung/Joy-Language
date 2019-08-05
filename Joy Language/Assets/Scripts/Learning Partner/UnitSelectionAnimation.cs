using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionAnimation : MonoBehaviour
{
    List<GameObject> levelButtons;
    [SerializeField] float interval;
    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(EnableAnimation());
    }

    IEnumerator EnableAnimation()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            yield return new WaitForSeconds(interval);
            iTween.ScaleTo(levelButtons[i], new Vector3(1, 1, 1), duration);
            yield return new WaitForSeconds(duration);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            iTween.ScaleTo(levelButtons[i], new Vector3(0,0,0),.1f);
        }
    }

    void GetLevelButtons(List<Button> tempButtons)
    {
        for(int i = 0; i < tempButtons.Count; i++)
        {
            levelButtons.Add(tempButtons[i].gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
