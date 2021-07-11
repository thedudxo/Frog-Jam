namespace Pursuits
{
    public class DefaultPursuitRules : IPursuitRules
    {
        readonly IPursuitRulesMemberCollection members;
        int currentIndex = 0;
        Pursuer previousPursuer = null;

        public DefaultPursuitRules(IPursuitRulesMemberCollection memberList)
        {
            this.members = memberList;
        }

        public void Check()
        {
            foreach(PursuitMember member in members)
            {
                PerformRulesBasedOnMemberType(member);

                currentIndex++;
            }

            currentIndex = 0;
        }

        private void PerformRulesBasedOnMemberType(PursuitMember member)
        {
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
