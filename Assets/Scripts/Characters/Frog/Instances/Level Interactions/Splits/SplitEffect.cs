using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using Levels;
using UnityEngine.UI;

namespace Frogs
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

        [HideInInspector] public string splitName;

        public virtual void Setup(SplitEffectsManager SplitFXMngr)
        {
            split.AddSplitEffect(this);
            this.SplitFXMngr = SplitFXMngr;
            Frog frog = SplitFXMngr.frog;

            CharacterTransform = frog.transform;
            CharacterInstanceID = frog.gameObject.GetInstanceID();

            splitXPos = split.transform.position.x;
            splitName = split.SplitName;


            if (split is SplitEnd)
            {
                frog.events.SubscribeOnEndLevel(this);
                //Debug.Log("subscribed end split");
            }
                //bit messy but its only 3 lines instead of an entire new class to deal with
                //these are instansiated dynamicly so creating a new class requires changing the way that happens
        }

        public void OnEndLevel()
        {
            Debug.Log("<color=green>ON END LEVEL</color>");
            ReachedSplit();
        }
        public void ReachedSplit()
        {
            float newTime = SplitFXMngr.CurrentSplitTime;
            SplitFXMngr.CurrentSplitTime = 0;

            if (triggeredThisLife ) return;
            triggeredThisLife = true;

            ReachedSplitAnalyitics();

            if (newTime < BestTime)
                NewBestTime(newTime);
        }

        void NewBestTime(float newTime)
        {
            BestTime = newTime;
            bestTimeText.text = BestTime.ToString("f2") + " sec";
            SplitFXMngr.EmitPBParticles();
        }


        bool FirstTimeHere => BestTime == float.MaxValue;
        void ReachedSplitAnalyitics()
        {
            if (GM.sendAnyalitics == false) return;
            if (!FirstTimeHere) return;

            Dictionary<string, object> info = new Dictionary<string, object>
                { {"Time", SplitFXMngr.CurrentSplitTime }
                };

            Analytics.CustomEvent("First Time at " + splitName, info);
        }
    }
}
