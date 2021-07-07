using System.Collections.Generic;

namespace Pursuits
{
    class PursuitTickRunner
    {
        PursuitMemberCollection memberList;
        IPursuitRules pursuitRules;
        int tickCount = 0;
        public List<string> LastTickLog { get; private set; } = new List<string>();
        public PursuitTickRunner(PursuitMemberCollection memberList, IPursuitRules pursuitRules)
        {
            this.memberList = memberList;
            this.pursuitRules = pursuitRules;
        }

        public void Tick(int count = 1)
        {
            RunGivenTicks(count);
        }

        void RunGivenTicks(int count)
        {
            for (int i = 0; i < count; i++)
                RunSingleTick();
        }

        void RunSingleTick()
        {
            tickCount++;

            memberList.Sort();

            pursuitRules.Check();
            memberList.RemoveQueuedMembers();
            memberList.AssignMemberIndexes();

            LogTick();
        }

        void LogTick()
        {
            LastTickLog.Clear();

            LastTickLog.Add($"<color=yellow>________ Tick {tickCount} ________</color>");

            foreach (PursuitMember m in memberList.members)
            {
                LastTickLog.Add(m.ToString());
            }
        }
    }
}
