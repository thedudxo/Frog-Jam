using UnityEngine.UI;
using UnityEngine;

namespace FrogScripts
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Text timeTaken, pbTimeTaken, splitsSum, restartPrompt;
        [SerializeField] GameObject endScreen;

        public static bool bugOccured = false;

        const string timeFormat = "F3";

        public void Enable(float time, float pbTime, float splitsSum)
        {
<<<<<<< Updated upstream
            if (splitsSum > 10000000)
            {
                bugOccured = true;
                Debug.LogError("That annoying split bug");
            }
                
=======
            if (splitsSum > 1000000)
                Debug.LogError("That annoying split bug");
>>>>>>> Stashed changes

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