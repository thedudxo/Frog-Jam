using LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{
    public class SplitEffectsManager : MonoBehaviour, INotifyOnSetback, INotifyOnRestart
    {
        [Header("Dependincies")]
        [SerializeField] public Frog frog;
        [SerializeField] FrogTime frogTime;
        [SerializeField] ParticleSystem newPBParticles;

        const int ParticleEmitAmmount = 20;
        Transform ParticleTransform;

        SplitManager splitManager;

        List<SplitEffect> splitEffects = new List<SplitEffect>();
        [HideInInspector]public float CurrentSplitTime { get; set; } = 0;
        [HideInInspector] public float TotalSplitTime {
            get 
            {
                float time = 0;
                foreach(SplitEffect effect in splitEffects)
                    time += effect.BestTime;
                return time;
            }
            private set { }
        }

        private void Start()
        {
            frog.SubscribeOnRestart(this);
            frog.SubscribeOnSetback(this);

            splitManager = frog.currentLevel.splitManager;

            SetupSplitEffects();

            ParticleTransform = newPBParticles.transform;
        }

        public void SetupSplitEffects()
        {
            foreach(Split split in splitManager.splits)
            {
                SplitEffect effect = Instantiate(split.playerCopyCanvas).GetComponent<SplitEffect>();

                GameObject obj = effect.gameObject;
                obj.SetActive(true);
                obj.transform.position = split.playerCopyCanvas.transform.position;
                obj.layer = LayerMask.NameToLayer(frog.UILayer);

                effect.Setup(split, this);

                splitEffects.Add(effect);
            }
        }

        private void Update()
        {
            if(!frog.OnStartingPlatform)
                CurrentSplitTime += Time.deltaTime;
        }

        public void OnSetback()
        {
            int i = -1;
            foreach (SplitEffect effect in splitEffects)
            {
                ++i;

                bool notFirstInList = i > 0;

                if (notFirstInList)
                {
                    SplitEffect previousSplit = splitEffects[i - 1];

                    if (previousSplit.CharacterIsPast)
                        effect.triggeredThisLife = true;
                    else
                        effect.triggeredThisLife = false;
                }
            }
        }

        public void OnRestart()
        {
            CurrentSplitTime = 0;
            splitEffects[0].triggeredThisLife = false;
        }

        public void EmitPBParticles()
        {
            ParticleTransform.position = frog.transform.position;
            newPBParticles.Emit(ParticleEmitAmmount);
        }
    }
}