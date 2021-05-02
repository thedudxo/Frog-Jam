using System;

namespace Pursuits
{
    public interface INotifyOnMemberRemoved
    {
        void OnMemberRemoved();
    }

    public abstract class PursuitMember : IComparable<PursuitMember>
    {
        public float position;
        public float speed;
        public int index;

        public INotifyOnMemberRemoved ToNotifyOnMemberRemoved;

        public int CompareTo(PursuitMember pm)
        {
            return position.CompareTo(pm.position);
        }
    }
}
