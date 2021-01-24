using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using LevelScripts;

namespace FrogScripts
{
    public class FrogSplitTracker : MonoBehaviour, INotifyOnAnyRespawn
    {
        //instantiate these with a prefab
        Text bestTimeText;
        Text title;
        FrogSplitManager frogSplitManager;

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
            if (triggeredThisLife) return;
            triggeredThisLife = true;

            bestTime = frogSplitManager.currentTime;
            bestTimeText.text = bestTime.ToString("f2") + " sec";

            if (FirstTimeHere) TrackFirstTimeAnalyitic();
        }


        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", frogSplitManager.currentTime}
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }

        public void OnAnyRespawn()
        {
            triggeredThisLife = false;
        }
    }
}
