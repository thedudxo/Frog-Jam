using UnityEngine.UI;
using UnityEngine;

namespace FrogScripts
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Text timeTaken, pbTimeTaken, splitsSum;
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
        }

        public void Disable()
        {
            endScreen.SetActive(false);
        }
    }
}