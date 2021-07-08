using System.Collections.Generic;

namespace Pursuits
{
    class PursuitMemberCollection
    {
        public List<PursuitMember> members = new List<PursuitMember>();
        Stack<PursuitMember> membersToRemove = new Stack<PursuitMember>();

        public memberType Add<memberType>() where memberType : PursuitMember, new()
        {
            PursuitMember member = new memberType();
            members.Insert(0, member);
            return member as memberType;
        }

        public void Remove(PursuitMember member)
        {
            if (member == null) throw new System.ArgumentNullException("member");

            membersToRemove.Push(member);
        }

        public void Sort()
        {
            members.Sort();
        }

        public void RemoveQueuedMembers()
        {
            foreach (PursuitMember m in membersToRemove)
            {
                m.ToNotifyOnMemberRemoved?.OnMemberRemoved();
                members.Remove(m);
            }

            membersToRemove.Clear();
        }

        public void AssignMemberIndexes()
        {
            for (int i = 0; i <= members.Count - 1; i++)
            {
                members[i].index = i;
            }
        }
    }
}
