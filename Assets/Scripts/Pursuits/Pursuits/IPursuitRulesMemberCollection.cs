using System.Collections.Generic;

namespace Pursuits
{
    public interface IPursuitRulesMemberCollection
    {
        PursuitMember this[int index] { get; set; }

        int Count { get; }

        memberType Add<memberType>() where memberType : PursuitMember, new();
        void Remove(PursuitMember member);
        IEnumerator<PursuitMember> GetEnumerator();
    }
}