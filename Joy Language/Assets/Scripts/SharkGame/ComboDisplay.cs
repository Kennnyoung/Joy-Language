using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] icons;

    SpriteRenderer srComponent;

    private void Awake()
    {
        srComponent = GetComponent<SpriteRenderer>();
    }

    void DisplayCurrentCombo(int tempCombo)
    {
        srComponent.sprite = icons[tempCombo - 1];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
