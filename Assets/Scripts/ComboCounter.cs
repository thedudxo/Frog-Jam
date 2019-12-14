using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{

    [SerializeField] Text comboText;
    [SerializeField] string comboPrefix = "x";
    [SerializeField] float maxComboTime;

    float currentComboTime;
    int combo = 0;

    // Start is called before the first frame update
    void Start()
    {
        GM.comboCounter = this;
        comboText.text = "";
    }

    // Update is called once per frame
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
        comboText.text = combo + comboPrefix;
        currentComboTime = 0;
    }
}
