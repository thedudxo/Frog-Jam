using System.Collections.Generic;

namespace Pursuits
{
    class PursuitTickRunner
    {
        PursuitMemberCollection members;
        IPursuitRules pursuitRules;
        int tickCount = 0;
        public List<string> LastTickLog { get; private set; } = new List<string>();
        public PursuitTickRunner(PursuitMemberCollection memberList, IPursuitRules pursuitRules)
        {
            this.members = memberList;
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

            members.Sort();

            pursuitRules.Check();
            members.RemoveQueuedMembers();
            members.AssignMemberIndexes();

            LogTick();
        }

        void LogTick()
        {
            LastTickLog.Clear();

            LastTickLog.Add($"<color=yellow>________ Tick {tickCount} ________</color>");

            foreach (PursuitMember m in members)
            {
                LastTickLog.Add(m.ToString());
            }
        }
    }
}
