using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelScripts
{
    public class SplitManager : MonoBehaviour
    {
        //manages all the split locations in a level
        //instance per level

        [SerializeField] Text timer;
        [SerializeField] public Split[] splits;


        void Update()
        {
            timer.text = currentTime.ToString("f2");
        }
    }
}
