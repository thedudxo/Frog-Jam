using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chaseables.MonoBehaviours;
using Chaseables;

namespace FrogScripts
{
    public class FrogChaseable : Chaseable, 
        INotifyOnLeftPlatform, 
        INotifyOnDeath, 
        INotifyOnEndLevel, 
        INotifyOnRestart
    {
        [SerializeField] public Frog frog;

        public override bool IsCurrentlyChaseable { get => frog.state == FrogState.State.Level; set { } }

        public override IChaserCollection ChaserCollection { get => frog.currentLevel.Chasers; }

        public override float GetXPos() => frog.transform.position.x;

        bool NotOnStartPlatform => frog.state != FrogState.State.StartPlatform;

        private void Start()
        {
            frog.events.SubscribeOnLeftPlatform(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
            frog.events.SubscribeOnRestart(this);
        }

        private void Update()
        {
            if (NotOnStartPlatform) //minor bug: can be on start platform and dying, causing this to trigger
            {
                CheckBehind();
            }
        }

        public void OnDeath()
        {
            if (ActiveChaser == null) return;

            float resetPos = GetXPos() - frog.SetbackDistance;

            bool wouldResetBehindChaser = resetPos < ActiveChaser.GetXPos();

            if (wouldResetBehindChaser)
                EndChase();
        }

        public void OnEndLevel() => EndChase();

        public void OnRestart() => EndChase();
        public void OnLeftPlatform() => AttachClosestChaser();
    }
}