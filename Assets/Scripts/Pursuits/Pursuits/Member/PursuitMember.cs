using System;

namespace Pursuits
{
    public abstract class PursuitMember : IComparable<PursuitMember>
    {
        public float position;
        public IpostitonController positionController;

        internal PursuitMember()
        {
        }

        public int CompareTo(PursuitMember pm)
        {
            return position.CompareTo(pm.position);
        }
    }
}
