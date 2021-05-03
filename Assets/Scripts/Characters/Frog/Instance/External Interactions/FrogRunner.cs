using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;
using Characters;

namespace Frogs
{
    public class FrogRunner : MonoBehaviour, 
        INotifyOnLeftPlatform, 
        INotifyOnDeath, 
        INotifyOnEndLevel, 
        INotifyOnRestart
    {
        [SerializeField] public Frog frog;

        PursuitController pursuitHandler;
        public Runner runner;

        public bool IsCurrentlyChaseable => frog.state == FrogState.State.Level;

        private void Update()
        {
            if (runner != null)
            {
                runner.position = transform.position.x;
            }
        }

        private void Start()
        {
            frog.events.SubscribeOnLeftPlatform(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
            frog.events.SubscribeOnRestart(this);

            pursuitHandler = frog.collection.pursuitHandler;
        }

        public void OnEndLevel() => EndChase();
        public void OnRestart() => EndChase();
        public void OnLeftPlatform() => StartChase();
        public void OnDeath()
        {
            if (runner == null) return;
            float setbackPos = runner.position - frog.SetbackDistance;
            bool wouldResetBehindChaser = runner.pursuerBehind?.position > setbackPos;

            pursuitHandler.Tick();
        }

        void EndChase()
        {
            if (runner == null) return;
            pursuitHandler.pursuit.Remove(runner);
            runner = null;
            Debug.Log("end chase");
        }

        void StartChase()
        {
            runner = pursuitHandler.AddRunner();
        }


    }
}