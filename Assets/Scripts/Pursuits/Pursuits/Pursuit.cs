using System.Collections.Generic;

namespace Pursuits
{
    public class Pursuit
    {
        PursuitMemberCollection memberList = new PursuitMemberCollection();
        PursuitTickRunner ticks;
        public List<string> LastTickLog => ticks.LastTickLog;

        public Pursuit(IPursuitRules rules = null)
        {
            if (rules == null)
                rules = new DefaultPursuitRules(memberList);

            ticks = new PursuitTickRunner(memberList, rules);
        }

        public memberType Add<memberType>() where memberType : PursuitMember, new()
            => memberList.Add<memberType>();

        public void Remove(PursuitMember member)
            => memberList.Remove(member);

        public void Tick(int count = 1)
            => ticks.Tick(count);
    }
}
