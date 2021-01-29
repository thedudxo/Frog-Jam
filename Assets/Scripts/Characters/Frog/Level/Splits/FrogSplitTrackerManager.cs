using LevelScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class FrogSplitTrackerManager : MonoBehaviour, INotifyOnSetback
    {
        [Header("Components")]
        [SerializeField] public Frog frog;
        [SerializeField] FrogTime frogTime;

        SplitManager splitManager;

        List<FrogSplitTracker> splitReferences = new List<FrogSplitTracker>();
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
            SetupSplitUIs();
        }

        public void SetupSplitUIs()
        {
            foreach(Split split in splitManager.splits)
            {
                FrogSplitTracker splitTracker = Instantiate(split.playerCopyCanvas).GetComponent<FrogSplitTracker>();
                splitTracker.gameObject.SetActive(true);
                splitTracker.gameObject.transform.position = split.playerCopyCanvas.transform.position;

                splitTracker.Setup(split, this);

                splitReferences.Add(splitTracker);
            }
        }

        public void OnSetback()
        {
            foreach(FrogSplitTracker tracker in splitReferences)
            {
                bool frogPassedSplit = tracker.Split.IsPastSplit(frog.transform.position.x);

                if (frogPassedSplit)
                {

                }
            }
        }
    }
}