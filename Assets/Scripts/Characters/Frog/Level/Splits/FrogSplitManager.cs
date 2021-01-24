using LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{
    public class FrogSplitManager : MonoBehaviour
    {
        [SerializeField] GameObject splitUIPrefab;

        Frog frog;
        SplitManager splitManager;

        List<FrogSplitTracker> splitUIs = new List<FrogSplitTracker>();

        private void Start()
        {
            splitManager = frog.currentLevel.splitManager;
        }

        public void SetupSplitUIs()
        {
            foreach(Split split in splitManager.splits)
            {
                FrogSplitTracker splitUI = Instantiate(splitUIPrefab).GetComponent<FrogSplitTracker>();

                splitUI.Setup(split);

                splitUIs.Add(splitUI);
            }
        }
    }
}