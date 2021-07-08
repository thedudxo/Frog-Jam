namespace Pursuits
{
    class DefaultPursuitRules : IPursuitRules
    {
        PursuitMemberCollection memberList;
        int memberCount;
        int currentIndex;
        Pursuer previousPursuer = null;

        public DefaultPursuitRules(PursuitMemberCollection memberList)
        {
            this.memberList = memberList;
        }

        public void Check()
        {
            memberCount = memberList.members.Count;


            for (int index = 0; index <= memberCount - 1; index++)
            {
                currentIndex = index;

                PerformRulesBasedOnMemberType();
            }
        }

        private void PerformRulesBasedOnMemberType()
        {
            PursuitMember member = memberList.members[currentIndex];

            if (member is Pursuer)
                PursuerRules(member as Pursuer);

            else
                RunnerRules(member as Runner);
        }

        void RunnerRules(Runner runner)
        {
            runner.pursuerBehind = previousPursuer;
        }

        void PursuerRules(Pursuer pursuer)
        {
            if (pursuerIsLast || nextMemberIsPursuer)
                memberList.Remove(pursuer);

            else
                previousPursuer = pursuer;
        }

        bool nextMemberIsPursuer => memberList.members[currentIndex + 1] is Pursuer;
        bool pursuerIsLast => currentIndex == memberCount - 1;
    }
}
