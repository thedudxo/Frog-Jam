using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

namespace FrogScripts.UI
{
    public class SplitUI : MonoBehaviour
    {
        //instantiate these with a prefab
        Text bestTimeText;
        Text title;

        string name;

        float bestTime;
        bool triggeredThisLife = false;

        public void Setup(Split split)
        {
            title.text = split.Name;
            split.AddSplitUI(this);
        }

        bool BeatBestTime => frog.TimeInCurrentSplit < bestTime;
        bool FirstTimeHere => bestTime == 0;

        public void ReachedSplit()
        {

            triggeredThisLife = true;

            if (FirstTimeHere)
            {  
                TrackFirstTimeAnalyitic();
                UpdateUI();
            }

            else if (BeatBestTime || bestTime == 0)
            {
                UpdateUI();
            }
        }

        void UpdateUI()
        {
            bestTime = GM.splitManager.currentTime;
            bestTimeText.text = decimal.Round(bestTime, 2) + " sec";
        }

        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", GM.splitManager.currentTime}
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }
    }
}
