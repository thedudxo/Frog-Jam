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
        Frog frog;

        public Split Split { get; private set; }
        string name;

        float bestTime;
        bool triggeredThisLife = false;

        public void Setup(Split split, FrogSplitManager frogSplitManager)
        {
            title.text = split.Name;
            split.AddSplitUI(this);
            this.frogSplitManager = frogSplitManager;
            frog = frogSplitManager.frog;
        }

        bool BeatBestTime => frogSplitManager.currentSplitTime < bestTime;
        bool FirstTimeHere => bestTime == 0;

        public void ReachedSplit()
        {
            if (triggeredThisLife) return;
            triggeredThisLife = true;

            bestTime = frogSplitManager.currentSplitTime;
            bestTimeText.text = bestTime.ToString("f2") + " sec";

            if (FirstTimeHere) TrackFirstTimeAnalyitic();
        }


        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", frogSplitManager.currentSplitTime }
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }

        public void OnAnyRespawn()
        {
            triggeredThisLife = false;
        }


    }
}
