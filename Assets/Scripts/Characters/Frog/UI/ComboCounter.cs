using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{

    [SerializeField] Text comboText;
    [SerializeField] string comboSuffix = "x";
    [SerializeField] float maxComboTime;

    float currentComboTime;
    int combo = 0;

    void Start()
    {
        //GM.comboCounter = this;
        comboText.text = "";
    }

    void Update()
    {
        currentComboTime += Time.deltaTime;

        if (currentComboTime >= maxComboTime)
        {
            comboText.text = "";
            combo = 0;
        }
    }

    public void CheckCombo()
    {
        combo++;
        comboText.text = combo + comboSuffix;
        currentComboTime = 0;
    }
}
