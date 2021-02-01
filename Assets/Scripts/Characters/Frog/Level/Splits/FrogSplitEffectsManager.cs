using LevelScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class FrogSplitEffectsManager : MonoBehaviour, INotifyOnSetback
    {
        [Header("Components")]
        [SerializeField] public Frog frog;
        [SerializeField] FrogTime frogTime;

        SplitManager splitManager;

        List<FrogSplitEffects> splitReferences = new List<FrogSplitEffects>();
        bool inCurrentSplit = false;
        int nextSplit = 0;
        [HideInInspector]public float currentSplitTime { get; private set; } = 0;


        /*
         * when reaching the end of the split:
         *  increment nextSplit by 1
         *  timeInCurrentSplit = 0
         *  
         * when frog setback
         *  check what split were in now by:
         *   
         *  
         * when Frog Restart
         *  nextSplit = 0 
         */

        private void Update()
        {
            currentSplitTime = frogTime.CurrentLevelTime;
        }

        private void Start()
        {
            splitManager = frog.currentLevel.splitManager;
            SetupSplitEffects();
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

                splitReferences.Add(splitTracker);
            }
        }


        public void OnSetback()
        {
            foreach(FrogSplitEffects tracker in splitReferences)
            {
                bool frogPassedSplit = tracker.Split.IsPastSplit(frog.transform.position.x);

                if (frogPassedSplit)
                {

                }
            }
        }
    }
}