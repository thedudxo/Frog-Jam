using System.Collections;
using UnityEngine;

namespace Frogs.Instances
{
    public class FrogState : INotifyOnRestart, INotifyBeforeDeath, INotifyOnEndLevel, INotifyOnAnyRespawn
    {
        public enum State {Level, StartPlatform, Hidden, Dead}
        Frog frog;
        Transform transform;
        Rigidbody2D rb;

        public State state { get; private set; } = State.StartPlatform;

        public FrogState(Frog frog)
        {
            this.frog = frog;
            this.transform = frog.transform;
            this.rb = frog.rb;

            frog.events.SubscribeOnRestart(this);
            frog.events.SubscribeBeforeDeath(this);
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
                bool OnStartingPlatform = transform.position.x < frog.currentLevel.StartPlatformLength;
                if (OnStartingPlatform == false)
                {
                    state = State.Level;
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
                frog.controllers.vfx.ShowFrogVisuals(true);

            state = State.StartPlatform;
        }

        public void OnAnyRespawn()
        {
            rb.simulated = true;
            rb.isKinematic = false;

            if (frog.state == State.Dead)
            state = State.Level;
        }

        public void BeforeDeath() 
        {
            state = State.Dead;
            rb.simulated = false;
        }

        public void OnEndLevel()
        {
            state = State.Hidden;
            rb.simulated = false;
            frog.controllers.vfx.ShowFrogVisuals(false);
        }

    }
}