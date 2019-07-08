using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] icons;
    [SerializeField] Transform enterPosition;
    [SerializeField] float moveInDuration;
    [SerializeField] float hoverDuration;
    [SerializeField] float fadeOutDuration;
    SpriteRenderer srComponent;

    Vector3 targetPosition;
    void Awake()
    {
        srComponent = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        targetPosition = transform.position;
        transform.position = enterPosition.position;
    }
    void DisplayCurrentCombo(int tempCombo)
    {
        srComponent.sprite = icons[tempCombo - 1];
        StartCoroutine(ComboAnimation());
    }

    IEnumerator ComboAnimation()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPosition, "time", moveInDuration, "easetype", "easeOutBack"));
        yield return new WaitForSeconds(moveInDuration + hoverDuration);
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", fadeOutDuration, "easetype", "easeInOutSine"));
        yield return new WaitForSeconds(fadeOutDuration);
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 0f, "easetype", "easeInOutSine"));
        transform.position = enterPosition.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
