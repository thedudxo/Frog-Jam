using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelScripts
{
    public class SplitManager : MonoBehaviour
    {
        [HideInInspector] public List<Split> splits;
        [SerializeField] public FrogManager frogManager;

        public void AddSplit(Split split)
        {
            if (splits.Contains(split)) 
            {
                Debug.LogWarning("Tried adding split that was allready in the list", split);
                return;
            }

            splits.Add(split);
        }
    }
}
