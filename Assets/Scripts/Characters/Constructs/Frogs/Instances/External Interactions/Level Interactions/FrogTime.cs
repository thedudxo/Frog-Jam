using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frogs.Instances
{
    public class FrogTime : MonoBehaviour, INotifyOnRestart
    {
        [SerializeField] Frog frog;
        [SerializeField] Text timer;
        [HideInInspector] public float CurrentLevelTime { get; private set; } = 0;

        private void Start()
        {
            frog.events.SubscribeOnRestart(this);
        }

        void Update()
        {
            if (frog.state.OnStartPlatform == false)
            {
                CurrentLevelTime += Time.deltaTime;
            }

            timer.text = CurrentLevelTime.ToString("f1");
        }

        public void OnRestart()
        {
            CurrentLevelTime = 0;
        }
    }
}
