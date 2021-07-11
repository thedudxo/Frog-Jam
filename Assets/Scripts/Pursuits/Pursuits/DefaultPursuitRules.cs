﻿namespace Pursuits
{
    public class DefaultPursuitRules : IPursuitRules
    {
        readonly IPursuitRulesMemberCollection members;
        int currentIndex;
        Pursuer previousPursuer = null;

        public DefaultPursuitRules(IPursuitRulesMemberCollection memberList)
        {
            this.members = memberList;
        }

        public void Check()
        {

            for (int index = 0; index <= members.Count - 1; index++)
            {
                currentIndex = index;

                PerformRulesBasedOnMemberType();
            }
        }

        private void PerformRulesBasedOnMemberType()
        {
            PursuitMember member = members[currentIndex];

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
            if (PursuerIsLast || NextMemberIsPursuer)
                members.Remove(pursuer);

            else
                previousPursuer = pursuer;
        }

        bool NextMemberIsPursuer => members[currentIndex + 1] is Pursuer;
        bool PursuerIsLast => currentIndex == members.Count - 1;
    }
}
