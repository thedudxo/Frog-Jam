using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelScripts
{
    public class SplitManager : MonoBehaviour
    {
        [HideInInspector] public List<Split> splitsList;
        [SerializeField] public FrogManager frogManager;

        public void AddSplit(Split split)
        {
            if (splitsList.Contains(split)) 
            {
                Debug.LogWarning($"Tried adding split '{split.name}', but it was allready in the list", split);
                return;
            }

            splitsList.Add(split);
            Debug.Log($"<color=green>MANAGER:</color> added {split.name} to the manager", this);
        }
    }
}
