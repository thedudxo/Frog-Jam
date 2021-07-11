using Pursuits;
using System.Collections.Generic;

namespace Tests.Pursuits
{
    class MemberListMock : IPursuitRulesMemberCollection
    {
        public List<PursuitMember> list = new List<PursuitMember>();

        public PursuitMember this[int index] 
        { 
            get => list[index]; 
            set => list[index] = value; 
        }

        public int Count => list.Count;

        public memberType Add<memberType>() where memberType : PursuitMember, new()
        {
            memberType member = new memberType();
            list.Add(member);
            return (member);
        }

        public IEnumerator<PursuitMember> GetEnumerator() => list.GetEnumerator();

        public void Remove(PursuitMember member) => list.Remove(member);
    }
}
