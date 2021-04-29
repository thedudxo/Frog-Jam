using System;

namespace Pursuits
{
    public abstract class PursuitMember : IComparable<PursuitMember>
    {
        public float position;

        public int CompareTo(PursuitMember pm)
        {
            return position.CompareTo(pm.position);
        }
    }
}
