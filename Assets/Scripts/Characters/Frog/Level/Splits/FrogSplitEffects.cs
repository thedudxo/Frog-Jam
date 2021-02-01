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
        FrogSplitEffectsManager splitEffectsManager;
        public Frog Frog { get; set; }
        public int TriggerInstanceID { get; set; }
        public Split Split { get; private set; }

        float bestTime = float.MaxValue;
        bool triggeredThisLife = false;



        public bool isActive = true;

        public void Setup(Split split, FrogSplitEffectsManager frogSplitManager)
        {
            split.AddSplitEffect(this);
            this.Split = split;
            this.splitEffectsManager = frogSplitManager;
            Frog = frogSplitManager.frog;
            Frog.SubscribeOnAnyRespawn(this);
            TriggerInstanceID = Frog.gameObject.GetInstanceID();
        }

        bool BeatBestTime => splitEffectsManager.currentSplitTime < bestTime;
        bool FirstTimeHere => bestTime == 0;

        public void ReachedSplit()
        {
            if (triggeredThisLife || !isActive) return;
            triggeredThisLife = true;

            if (FirstTimeHere) TrackFirstTimeAnalyitic();

            if (BeatBestTime)
            {
                bestTime = splitEffectsManager.currentSplitTime;
                bestTimeText.text = bestTime.ToString("f2") + " sec";


                splitEffectsManager.EmitPBParticles();
            }

            splitEffectsManager.ReachedNextSplit();
        }

        public void OnAnyRespawn()
        {
            triggeredThisLife = false;
            if (splitEffectsManager.PreviousSplitActive(this))
            {

            }
        }

        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", splitEffectsManager.currentSplitTime }
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }


    }
}
