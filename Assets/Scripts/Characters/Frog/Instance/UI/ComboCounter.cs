using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts {
    public class ComboCounter : MonoBehaviour, INotifyOnDeath
    {
        [SerializeField] Frog frog;
        [SerializeField] Text comboText;
        [SerializeField] string comboSuffix = "x";
        float maxComboTime = 3;

        float currentComboTime;
        int combo = 0;

        void Start()
        {
            frog.SubscribeOnDeath(this);
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

        public void OnDeath()
        {
            CheckCombo();
        }
    }
}
