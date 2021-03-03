﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{
    public interface INotifyOnDeath {void OnDeath();}
    public interface INotifyOnRestart {void OnRestart(); }
    public interface INotifyBeforeRestart {void BeforeRestart(); }
    public interface INotifyOnSetback {void OnSetback(); }
    public interface INotifyOnAnyRespawn {void OnAnyRespawn();}
    public interface INotifyOnEndLevel { void OnEndLevel(); }
    public interface INotifyOnLeftPlatform { void OnLeftPlatform(); }

    public class FrogEvents
    {
        public List<INotifyOnDeath>        toNotifyOnDeath              = new List<INotifyOnDeath>();
        public List<INotifyOnSetback>      toNotifyOnSetback            = new List<INotifyOnSetback>();
        public List<INotifyOnRestart>      toNotifyOnRestart            = new List<INotifyOnRestart>();
        public List<INotifyOnRestart>      UnsubscribeToNotifyOnRestart = new List<INotifyOnRestart>();
        public List<INotifyBeforeRestart>  toNotifyBeforeRestart        = new List<INotifyBeforeRestart>();
        public List<INotifyOnAnyRespawn>   toNotifyOnAnyRespawn         = new List<INotifyOnAnyRespawn>();
        public List<INotifyOnEndLevel>     toNotifyOnEndLevel           = new List<INotifyOnEndLevel>();
        public List<INotifyOnLeftPlatform> toNotifyOnLeftPlatform       = new List<INotifyOnLeftPlatform>();

        public void SubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Add(subscriber);
        }
        public void UnscubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Remove(subscriber);
        }

        public void SubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Add(subscriber);
        }
        public void UnsubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Remove(subscriber);
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

        public void SubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Add(subscriber);
        }
        public void UnsubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Remove(subscriber);
        }

        public void SubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Add(subscriber);
        }
        public void UnsubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Remove(subscriber);
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