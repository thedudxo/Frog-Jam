using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine.UI;

namespace FrogScripts
{

    public class SplitEffect : MonoBehaviour
    {
        [SerializeField] Text bestTimeText;
        SplitEffectsManager SplitFXMngr;

        [HideInInspector] public float BestTime { get; private set; } = float.MaxValue;
        [HideInInspector] public bool triggeredThisLife = false;

        [HideInInspector] public Transform CharacterTransform { get; set; }
        [HideInInspector] public int CharacterInstanceID { get; set; }
        float splitXPos;
        public bool CharacterIsPast => CharacterTransform.position.x > splitXPos;


        public virtual void Setup(Split split, SplitEffectsManager SplitFXMngr)
        {
            split.AddSplitEffect(this);
            this.SplitFXMngr = SplitFXMngr;

            CharacterTransform = SplitFXMngr.frog.transform;
            CharacterInstanceID = SplitFXMngr.frog.gameObject.GetInstanceID();

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
