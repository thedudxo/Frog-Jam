using LevelScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class FrogSplitManager : MonoBehaviour, INotifyOnSetback
    {
        [SerializeField] GameObject splitUIPrefab;
        [SerializeField] public Frog frog;
        [SerializeField] Text timer;

        SplitManager splitManager;

        List<FrogSplitTracker> trackers = new List<FrogSplitTracker>();
        public float currentSplitTime { get; private set; } = 0;
        bool inCurrentSplit = false;
        int nextSplit = 0;
        bool timing = false;

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

        private void Start()
        {
            splitManager = frog.currentLevel.splitManager;
        }


        void Update()
        {
            if (timing)
            {
                timer.text = currentSplitTime.ToString("f2");
            }

        }

        public void SetupSplitUIs()
        {
            foreach(Split split in splitManager.splits)
            {
                FrogSplitTracker splitTracker = Instantiate(splitUIPrefab).GetComponent<FrogSplitTracker>();

                splitTracker.Setup(split, this);

                trackers.Add(splitTracker);
            }
        }

        public void OnSetback()
        {
            foreach(FrogSplitTracker tracker in trackers)
            {
                bool frogPassedSplit = tracker.Split.IsPastSplit(frog.transform.position.x);

                if (frogPassedSplit)
                {

                }
            }
        }
    }
}