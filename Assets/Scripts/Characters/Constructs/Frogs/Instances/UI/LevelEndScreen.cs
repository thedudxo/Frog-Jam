﻿using UnityEngine.UI;
using UnityEngine;

namespace Frogs.Instances
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Text timeTaken, pbTimeTaken, splitsSum, restartPrompt;
        [SerializeField] GameObject endScreen;
        [SerializeField] GameObject restartButton;

        const string timeFormat = "F3";

        private void Start()
        {
#if UNITY_ANDROID
            restartButton.SetActive(true);
            restartPrompt.enabled = false;
#else
            restartButton.SetActive(false);
            restartPrompt.enabled = true;
#endif
        }

        public void Enable(float time, float pbTime, float splitsSum)
        {

            endScreen.SetActive(true);
            timeTaken.text = time.ToString(timeFormat);
            pbTimeTaken.text = pbTime.ToString(timeFormat);
            this.splitsSum.text = splitsSum.ToString(timeFormat);

            //this is a really quick fix to make the key display as a user friendly name
            KeyCode suicideKey = frog.controllers.input.GetKeybind(Inputs.Action.Suicide);
            string restartText = suicideKey.ToString();
            if (suicideKey == KeyCode.RightShift) restartText = "shift";
            restartPrompt.text = $"Press {restartText} to restart";
        }

        public void Disable()
        {
            endScreen.SetActive(false);
        }
    }
}