using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine.UI;

namespace FrogScripts
{
    public class FrogSplitEffects : MonoBehaviour, INotifyOnAnyRespawn, ISplitEffect
    {
        //instantiate these with a prefab
        [SerializeField] Text bestTimeText;
        FrogSplitEffectsManager splitManager;
        public Frog Frog { get; set; }
        public int TriggerInstanceID { get; set; }

        public Split Split { get; private set; }

        float bestTime = float.MaxValue;
        bool triggeredThisLife = false;

        public void Setup(Split split, FrogSplitEffectsManager frogSplitManager)
        {
            split.AddSplitEffect(this);
            this.splitManager = frogSplitManager;
            Frog = frogSplitManager.frog;
            Frog.SubscribeOnAnyRespawn(this);
            TriggerInstanceID = Frog.gameObject.GetInstanceID();
            Debug.Log("Split effect ID: " + TriggerInstanceID, this);
        }

        bool BeatBestTime => splitManager.currentSplitTime < bestTime;
        bool FirstTimeHere => bestTime == 0;

        public void ReachedSplit()
        {
            if (triggeredThisLife) return;
            triggeredThisLife = true;

            if (FirstTimeHere) TrackFirstTimeAnalyitic();

            if (BeatBestTime)
            {
                bestTime = splitManager.currentSplitTime;
                bestTimeText.text = bestTime.ToString("f2") + " sec";


                splitManager.EmitPBParticles();
            }
        }

        public void OnAnyRespawn()
        {
            triggeredThisLife = false;
        }

        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", splitManager.currentSplitTime }
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }


    }
}
