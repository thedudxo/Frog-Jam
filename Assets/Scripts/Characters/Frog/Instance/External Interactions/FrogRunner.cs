using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;

namespace FrogScripts
{
    public class FrogRunner : MonoBehaviour, 
        INotifyOnLeftPlatform, 
        INotifyOnDeath, 
        INotifyOnEndLevel, 
        INotifyOnRestart
    {
        [SerializeField] public Frog frog;

        Pursuit pursuit;
        public Runner runner;

        public bool IsCurrentlyChaseable => frog.state == FrogState.State.Level;

        private void Update()
        {
            runner.position = transform.position.x;
        }

        private void Start()
        {
            frog.events.SubscribeOnLeftPlatform(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
            frog.events.SubscribeOnRestart(this);

            pursuit = frog.pursuitHandler.pursuit;
            StartChase();
        }

        public void OnDeath()
        {
            float setbackPos = runner.position - frog.SetbackDistance;
            bool wouldResetBehindChaser = runner.pursuerBehind.position > setbackPos;

            if (wouldResetBehindChaser)
                EndChase();
        }

        void EndChase()
        {
            pursuit.Remove(runner);
        }

        void StartChase()
        {
            runner = frog.pursuitHandler.AddRunner();
        }

        public void OnEndLevel() => EndChase();
        public void OnRestart() => EndChase();
        public void OnLeftPlatform() => StartChase();
    }
}