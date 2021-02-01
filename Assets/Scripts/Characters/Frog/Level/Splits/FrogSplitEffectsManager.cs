using LevelScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class FrogSplitEffectsManager : MonoBehaviour, INotifyOnSetback, INotifyOnRestart
    {
        [Header("Components")]
        [SerializeField] public Frog frog;
        [SerializeField] FrogTime frogTime;
        [SerializeField] ParticleSystem newPBParticles;
        const int ParticleEmitAmmount = 20;
        Transform newPBParticlesTransform;

        SplitManager splitManager;

        List<FrogSplitEffects> splitEffects = new List<FrogSplitEffects>();
        bool inCurrentSplit = false;
        public FrogSplitEffects nextSplit;
        [HideInInspector]public float currentSplitTime { get; private set; } = 0;


        /*
         * when reaching the end of the split:
         *  increment nextSplit by 1
         *  currentSplitTime = 0
         *  
         * when frog setback
         *  check what split were in now by:
         *      split.IsPastSplit(float Xposition)
         *   
         *  
         * when Frog Restart
         *  nextSplit = 0 
         */

        private void Update()
        {
            currentSplitTime += Time.deltaTime;
        }

        private void Start()
        {
            frog.SubscribeOnRestart(this);
            frog.SubscribeOnSetback(this);

            splitManager = frog.currentLevel.splitManager;

            SetupSplitEffects();
            nextSplit = splitEffects[0];

            newPBParticlesTransform = newPBParticles.transform;
        }

        public void SetupSplitEffects()
        {
            foreach(Split split in splitManager.splits)
            {
                FrogSplitEffects splitTracker = Instantiate(split.playerCopyCanvas).GetComponent<FrogSplitEffects>();
                GameObject obj = splitTracker.gameObject;
                obj.SetActive(true);
                obj.transform.position = split.playerCopyCanvas.transform.position;
                obj.layer = LayerMask.NameToLayer(frog.UILayer);

                splitTracker.Setup(split, this);

                splitEffects.Add(splitTracker);
            }
        }

        public void OnSetback()
        {
            foreach(FrogSplitEffects effect in splitEffects)
            {
                bool frogPassedSplit = effect.Split.IsPastSplit(frog.transform.position.x);

                if (frogPassedSplit)
                {

                }
            }
        }

        public void ReachedNextSplit()
        {
            Debug.Log("reached me");

            int currentSplitIndex = splitEffects.IndexOf(nextSplit);
            nextSplit = splitEffects[currentSplitIndex + 1];
            currentSplitTime = 0;
        }

        public bool PreviousSplitActive(FrogSplitEffects effect)
        {
            int currentPos = splitEffects.IndexOf(effect);
            if (currentPos == 0) return false;
            int previousPositon = currentPos - 1;
            return splitEffects[previousPositon].isActive;
        }

        public void EmitPBParticles()
        {
            newPBParticlesTransform.position = frog.transform.position;
            newPBParticles.Emit(ParticleEmitAmmount);
        }

        public void OnRestart()
        {
            currentSplitTime = 0;
            nextSplit = splitEffects[0];
        }
    }
}