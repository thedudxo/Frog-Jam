using Levels;
using System.Collections.Generic;
using UnityEngine;
using static ObjectInstanceBuilder;

namespace Frogs.Instances
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

        List<SplitEffect> splitEffects;
        [HideInInspector]public float CurrentSplitTime { get; set; } = 0;
        [HideInInspector] public float TotalSplitTime {
            get 
            {
                float time = 0;
                foreach (SplitEffect effect in splitEffects)
                {
                    time += effect.BestTime;
                    if (time > 10000000)
                    {
                        Debug.LogError("That annoying split bug");
                    }
                }
                return time;
            }
            private set { }
        }

        private void Start()
        {
            SetupManager();
            SetupSplitEffects();  
            
            void SetupManager()
            {
                frog.events.SubscribeOnRestart(this);
                frog.events.SubscribeOnSetback(this);

                splitManager = frog.currentLevel.splitManager;

                ParticleTransform = newPBParticles.transform;
            }

            void SetupSplitEffects()
            {
                List<GameObject> SplitUITemplates = new List<GameObject>();

                foreach (Split split in splitManager.splits)
                    SplitUITemplates.Add(split.canvasPrototype.gameObject);

                splitEffects = CreateInstances<SplitEffect>(SplitUITemplates, frog.SetObjectUILayer);

                foreach (SplitEffect effect in splitEffects)
                {
                    effect.Setup(this);
                    //Debug.Log($"<color=green>INSTANCE:</color> setup effect {effect.splitName}", this);
                }
            }
        }

        private void Update()
        {
            if (frog.state.OnStartPlatform == false)
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

                    if (previousSplit.CharacterIsPast == false)
                        effect.triggeredThisLife = false;
                }
            }
        }
        public void OnRestart()
        {
            CurrentSplitTime = 0;
            foreach(SplitEffect effect in splitEffects)
                effect.triggeredThisLife = false;
        }

        public void EmitPBParticles()
        {
            ParticleTransform.position = frog.transform.position;
            newPBParticles.Emit(ParticleEmitAmmount);
        }
    }
}