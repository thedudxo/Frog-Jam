using UnityEngine.UI;
using UnityEngine;

namespace FrogScripts
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Text timeTaken, pbTimeTaken, splitsSum, restartPrompt;
        [SerializeField] GameObject endScreen;

        const string timeFormat = "F3";

        public void Enable(float time, float pbTime, float splitsSum)
        {
            if (splitsSum > 1000000)
                Debug.Log("bug");

            endScreen.SetActive(true);
            timeTaken.text = time.ToString(timeFormat);
            pbTimeTaken.text = pbTime.ToString(timeFormat);
            this.splitsSum.text = splitsSum.ToString(timeFormat);

            //this is a really quick fix to make the keys display as a user freindly name
            string restartText = frog.controlls.suicideKey.ToString();
            if (frog.controlls.suicideKey == KeyCode.RightShift) restartText = "shift";
            restartPrompt.text = $"Press {restartText} to restart";
        }

        public void Disable()
        {
            endScreen.SetActive(false);
        }
    }
}