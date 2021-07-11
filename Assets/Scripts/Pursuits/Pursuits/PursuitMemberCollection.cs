using System.Collections;
using System.Collections.Generic;
using System;

namespace Pursuits
{
    class PursuitMemberCollection : IEnumerable<PursuitMember>, IPursuitRulesMemberCollection
    {
        readonly List<PursuitMember> members = new List<PursuitMember>();
        readonly Stack<PursuitMember> membersToRemove = new Stack<PursuitMember>();

        public PursuitMember this[int index]
        {
            get 
            {
                if (IndexOutOfBounds(index)) throw new ArgumentOutOfRangeException();
                return members[index];
            }
            set
            {
                if (IndexOutOfBounds(index)) throw new ArgumentOutOfRangeException();
                members[index] = value;
            }
        }

        bool IndexOutOfBounds(int index) => index < 0 || index > members.Count - 1;

        public int Count => members.Count;


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

        public IEnumerator<PursuitMember> GetEnumerator()
        {
            return ((IEnumerable<PursuitMember>)members).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)members).GetEnumerator();
        }
    }
}
