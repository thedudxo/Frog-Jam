namespace Pursuits
{
    class DefaultPursuitRules : IPursuitRules
    {
        PursuitMemberCollection memberList;
        int memberCount;

        public DefaultPursuitRules(PursuitMemberCollection memberList)
        {
            this.memberList = memberList;
        }

        public void Check()
        {
            memberCount = memberList.members.Count;

            Pursuer previousPursuer = null;

            for (int index = 0; index <= memberCount - 1; index++)
            {
                PursuitMember member = memberList.members[index];
                 
                if (member is Pursuer)
                {
                    previousPursuer = PursuerRules(previousPursuer, index, member);
                }

                else
                {
                    (member as Runner).pursuerBehind = previousPursuer;
                }
            }
        }

        Pursuer PursuerRules(Pursuer previousPursuer, int index, PursuitMember member)
        {
            bool notLastMember = index != memberCount - 1;

            if (notLastMember)
            {
                bool nextMemberIsPursuer = memberList.members[index + 1] is Pursuer;

                if (nextMemberIsPursuer)
                {
                    memberList.Remove(member);
                }

                else
                {
                    previousPursuer = member as Pursuer;
                }
            }

            else
            {
                memberList.Remove(member);
            }

            return previousPursuer;
        }
    }
}
