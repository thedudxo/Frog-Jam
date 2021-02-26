using System.Collections;
using UnityEngine;

namespace FrogScripts
{
    public class FrogLocationTracker : INotifyOnRestart, INotifyOnDeath, INotifyOnEndLevel
    {
        public enum Location { Level, StartPlatform, Hidden }
        Frog frog;
        Transform transform;

        public FrogLocationTracker(Frog frog)
        {
            this.frog = frog;
            transform = frog.transform;
            frog.SubscribeOnRestart(this);
            frog.SubscribeOnDeath(this);
            frog.SubscribeOnEndLevel(this);
        }

        public void CheckLocation()
        {
            switch (frog.location)
            {
                case Location.StartPlatform:
                    CheckStillOnPlatform();
                    break;
            }

            void CheckStillOnPlatform()
            {
                bool OnStartingPlatform = transform.position.x < frog.currentLevel.startLength;
                Debug.Log(OnStartingPlatform);
                if (OnStartingPlatform == false)
                {
                    frog.location = Location.Level;
                    frog.frogManager.events.TriggerAnyFrogLeftPlatform();
                }
            }
        }

        public void OnRestart()
        {
            frog.location = Location.StartPlatform;
        }

        void SetHidden()
        {
            frog.location = Location.Hidden;
        }

        public void OnDeath(){ SetHidden(); }
        public void OnEndLevel() { SetHidden(); }

    }
}