using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FrogScripts
{
    public class FrogManagerEvents
    {
        List<INotifyAnyFrogLeftPlatform> leftPlatformSubs = new List<INotifyAnyFrogLeftPlatform>();
        public void SubscribeAnyFrogLeftPlatform(INotifyAnyFrogLeftPlatform sub) { leftPlatformSubs.Add(sub); }
        public void UnsubscribeAnyFrogLeftPlatform(INotifyAnyFrogLeftPlatform sub) { leftPlatformSubs.Remove(sub); }
        public void TriggerAnyFrogLeftPlatform() {
            foreach (INotifyAnyFrogLeftPlatform sub in leftPlatformSubs) sub.AnyFrogLeftPlatform(); }
    }

    public interface INotifyAnyFrogLeftPlatform {void AnyFrogLeftPlatform(); }
}
