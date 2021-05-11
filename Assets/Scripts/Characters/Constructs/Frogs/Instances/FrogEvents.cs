using System;
using System.Collections.Generic;

namespace Frogs.Instances
{
    public interface IEventSubscriber {}
    public interface INotifyOnDeath : IEventSubscriber { void OnDeath(); }
    public interface INotifyBeforeDeath : IEventSubscriber { void BeforeDeath(); }
    public interface INotifyOnRestart : IEventSubscriber { void OnRestart(); }
    public interface INotifyBeforeRestart : IEventSubscriber { void BeforeRestart(); }
    public interface INotifyOnSetback : IEventSubscriber { void OnSetback(); }
    public interface INotifyOnAnyRespawn : IEventSubscriber { void OnAnyRespawn(); }
    public interface INotifyOnEndLevel : IEventSubscriber { void OnEndLevel(); }
    public interface INotifyOnLeftPlatform : IEventSubscriber { void OnLeftPlatform(); }

    public class FrogEvents
    {
        public List<INotifyBeforeDeath> toNotifyBeforeDeath = new List<INotifyBeforeDeath>();
        public List<INotifyOnDeath> toNotifyOnDeath = new List<INotifyOnDeath>();

        public List<INotifyOnSetback> toNotifyOnSetback = new List<INotifyOnSetback>();

        public List<INotifyOnRestart> toNotifyOnRestart = new List<INotifyOnRestart>();

        public List<INotifyOnRestart> UnsubscribeToNotifyOnRestart = new List<INotifyOnRestart>();

        public List<INotifyBeforeRestart> toNotifyBeforeRestart = new List<INotifyBeforeRestart>();

        public List<INotifyOnAnyRespawn> toNotifyOnAnyRespawn = new List<INotifyOnAnyRespawn>();

        public List<INotifyOnEndLevel> toNotifyOnEndLevel = new List<INotifyOnEndLevel>();

        public List<INotifyOnLeftPlatform> toNotifyOnLeftPlatform = new List<INotifyOnLeftPlatform>();

        public void SubscribeBeforeDeath(INotifyBeforeDeath subscriber)
        {
            toNotifyBeforeDeath.Add(subscriber);
        }
        public void UnsubscribeBeforeDeath(INotifyBeforeDeath subscriber)
        {
            toNotifyBeforeDeath.Remove(subscriber);
        }
        public void SendBeforeDeath()
        {
            foreach (INotifyBeforeDeath notify in toNotifyBeforeDeath) 
                notify.BeforeDeath();
        }

        public void SubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Add(subscriber);
        }
        public void UnscubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Remove(subscriber);
        }
        internal void SendOnDeath()
        {
            foreach (INotifyOnDeath notify in toNotifyOnDeath)
                notify.OnDeath();
        }

        public void SubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Add(subscriber);
        }
        public void UnsubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Remove(subscriber);
        }
        public void SendOnSetback()
        {
            foreach (INotifyOnSetback notify in toNotifyOnSetback)
                notify.OnSetback();
        }

        public void SubscribeOnRestart(INotifyOnRestart subscriber)
        {
            toNotifyOnRestart.Add(subscriber);
        }
        public void UnsubscribeOnRestart(INotifyOnRestart subscriber)
        {
            UnsubscribeToNotifyOnRestart.Add(subscriber);
        }

        public void SubscribeBeforeRestart(INotifyBeforeRestart subscriber)
        {
            toNotifyBeforeRestart.Add(subscriber);
        }
        public void UnsubscribeBeforeRestart(INotifyBeforeRestart subscriber)
        {
            toNotifyBeforeRestart.Remove(subscriber);
        }
        public void SendBeforeRestart()
        {
            foreach (INotifyBeforeRestart notify in toNotifyBeforeRestart)
                notify.BeforeRestart();
        }

        public void SendRestart()
        {
            foreach (INotifyOnRestart notify in toNotifyOnRestart)
            {
                notify.OnRestart();
            }

            foreach (INotifyOnRestart unsubscribe in UnsubscribeToNotifyOnRestart)
            {
                toNotifyOnRestart.Remove(unsubscribe);
            }
        }

        public void SubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Add(subscriber);
        }
        public void UnsubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Remove(subscriber);
        }
        public void SendOnAnyRespawn()
        {
            foreach (INotifyOnAnyRespawn notify in toNotifyOnAnyRespawn)
                notify.OnAnyRespawn();
        }

        public void SubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Add(subscriber);
        }
        public void UnsubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Remove(subscriber);
        }
        public void SendOnEndLevel()
        {
            foreach (INotifyOnEndLevel subscriber in toNotifyOnEndLevel)
                subscriber.OnEndLevel();
        }

        public void SubscribeOnLeftPlatform(INotifyOnLeftPlatform subscriber)
        {
            toNotifyOnLeftPlatform.Add(subscriber);
        }
        public void UnsubscribeOnLeftPlatform(INotifyOnLeftPlatform subscriber)
        {
            toNotifyOnLeftPlatform.Remove(subscriber);
        }
    }
}
