using System.Collections.Generic;
using Frogs.Instances;

namespace Frogs.Collections
{
    public interface INotifyOnAnyFrogDied
    {
        void OnAnyFrogDied();
    }

    public class FrogCollectionEvents: INotifyOnDeath
    {

        List<INotifyOnAnyFrogDied> onAnyFrogDied = new List<INotifyOnAnyFrogDied>();

        void NullCheck(object item)
        {
            if (item == null) throw new System.ArgumentNullException(item.ToString());
        }

        public void OnDeath() => NotifyOnAnyFrogDied();

        public void NotifyOnAnyFrogDied()
        {
            foreach(INotifyOnAnyFrogDied subscriber in onAnyFrogDied) 
            {
                subscriber.OnAnyFrogDied();
            }
        }

        public void SubscribeOnAnyFrogDied(INotifyOnAnyFrogDied subscriber)
        {
            NullCheck(subscriber);
            onAnyFrogDied.Add(subscriber);
        }

        public void UnsubscribeOnAnyFrogDied(INotifyOnAnyFrogDied subscriber)
        {
            NullCheck(subscriber);
            onAnyFrogDied.Remove(subscriber);
        }
    }
}
