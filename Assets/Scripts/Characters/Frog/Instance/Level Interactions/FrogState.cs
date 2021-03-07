using System.Collections;
using UnityEngine;

namespace FrogScripts
{
    public class FrogState : INotifyOnRestart, INotifyOnDeath, INotifyOnEndLevel, INotifyOnAnyRespawn
    {
        public enum State {Level, StartPlatform, Hidden, Dead}
        Frog frog;
        Transform transform;
        Rigidbody2D rb;

        public FrogState(Frog frog)
        {
            this.frog = frog;
            this.transform = frog.transform;
            this.rb = frog.rb;

            frog.events.SubscribeOnRestart(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
            frog.events.SubscribeOnAnyRespawn(this);
        }

        public void CheckLocation()
        {
            switch (frog.state)
            {
                case State.StartPlatform:
                    CheckStillOnPlatform();
                    break;
            }

            void CheckStillOnPlatform()
            {
                bool OnStartingPlatform = transform.position.x < frog.currentLevel.startLength;
                if (OnStartingPlatform == false)
                {
                    frog.state = State.Level;
                    frog.manager.events.TriggerAnyFrogLeftPlatform();
                    foreach (INotifyOnLeftPlatform subscriber in frog.events.toNotifyOnLeftPlatform)
                    {
                        subscriber.OnLeftPlatform();
                    }
                }
            }
        }

        public void OnRestart()
        {
            if(frog.state == State.Hidden)
                frog.vfxManager.ShowFrogVisuals(true);

            frog.state = State.StartPlatform;
        }

        public void OnAnyRespawn()
        {
            rb.simulated = true;
            rb.isKinematic = false;

            if (frog.state == State.Dead)
            frog.state = State.Level;
        }

        public void OnDeath() 
        {
            frog.state = State.Dead;
            rb.simulated = false;
        }

        public void OnEndLevel()
        {
            frog.state = State.Hidden;
            rb.simulated = false;
            frog.vfxManager.ShowFrogVisuals(false);
        }

    }
}