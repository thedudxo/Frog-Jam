using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine.UI;

namespace FrogScripts
{
    public class SplitEffect : MonoBehaviour, ISplitEffect
    {
        [SerializeField] Text bestTimeText;
        SplitEffectsManager SplitFXMngr;

        [HideInInspector] public int TriggerInstanceID { get; set; }
        [HideInInspector] public float BestTime { get; private set; } = float.MaxValue;
        [HideInInspector] public bool triggeredThisLife = false;

        Transform frogTransform;
        float splitXPos;
        public bool FrogIsPast => frogTransform.position.x > splitXPos;


        public void Setup(Split split, SplitEffectsManager frogSplitManager)
        {
            split.AddSplitEffect(this);
            this.SplitFXMngr = frogSplitManager;

            frogTransform = SplitFXMngr.frog.transform;
            TriggerInstanceID = SplitFXMngr.frog.gameObject.GetInstanceID();

            splitXPos = split.transform.position.x;
        }

        bool FirstTimeHere => BestTime == 0;

        public void ReachedSplit()
        {
            float newTime = SplitFXMngr.CurrentSplitTime;
            SplitFXMngr.CurrentSplitTime = 0;

            if (triggeredThisLife ) return;
            triggeredThisLife = true;

            if (FirstTimeHere)
                    TrackFirstTimeAnalyitic();

            if (newTime < BestTime)
                NewBestTime(newTime);
        }

        void NewBestTime(float newTime)
        {
            BestTime = newTime;
            bestTimeText.text = BestTime.ToString("f2") + " sec";
            SplitFXMngr.EmitPBParticles();
        }

        void TrackFirstTimeAnalyitic()
        {
            if (!GM.sendAnyalitics) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", SplitFXMngr.CurrentSplitTime }
                };

            Analytics.CustomEvent("First Time at " + name, info);
        }

    }
}
