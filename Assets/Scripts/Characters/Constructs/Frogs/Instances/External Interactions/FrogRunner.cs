using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;
using Characters;

namespace Frogs.Instances
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
        public void OnRestart() { EndChase(); }
        public void OnLeftPlatform() => StartChase();
        public void OnDeath()
        {
            if (runner == null) return;

            if(frog.state.inDanger)
                //EndChase();

            pursuitHandler.Tick();
        }

        void EndChase()
        {
            if (runner == null) return;
            pursuitHandler.pursuit.Remove(runner);
            runner = null;
        }

        void StartChase()
        {
            runner = pursuitHandler.AddRunner();
        }
    }
}