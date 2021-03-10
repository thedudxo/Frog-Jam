using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine.UI;

namespace FrogScripts
{
    public class SplitEffect : MonoBehaviour, INotifyOnEndLevel
    {
        [SerializeField] Text bestTimeText;
        [SerializeField] Split split;
        SplitEffectsManager SplitFXMngr;

        [HideInInspector] public float BestTime { get; private set; } = float.MaxValue;
        [HideInInspector] public bool triggeredThisLife = false;

        [HideInInspector] public Transform CharacterTransform { get; set; }
        [HideInInspector] public int CharacterInstanceID { get; set; }
        float splitXPos;
        public bool CharacterIsPast => CharacterTransform.position.x > splitXPos;

        string splitName;

        public virtual void Setup(SplitEffectsManager SplitFXMngr)
        {
            split.AddSplitEffect(this);
            this.SplitFXMngr = SplitFXMngr;
            Frog frog = SplitFXMngr.frog;

            CharacterTransform = frog.transform;
            CharacterInstanceID = frog.gameObject.GetInstanceID();

            splitXPos = split.transform.position.x;
            splitName = split.SplitName;

            if(split is SplitEnd)
                frog.events.SubscribeOnEndLevel(this);
                //bit messy but its only 3 lines instead of an entire new class to deal with
        }

        public void OnEndLevel() => ReachedSplit();

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

            Analytics.CustomEvent("First Time at " + splitName, info);
        }
    }
}
