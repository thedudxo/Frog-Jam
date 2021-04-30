using System.Collections.Generic;

namespace Pursuits
{
    public class Pursuit
    {

        public List<PursuitMember> members = new List<PursuitMember>();

        public List<string> LastTickLog { get; private set; } = new List<string>();
        int tickCount = 0;

        public memberType Add<memberType>() where memberType : PursuitMember, new()
        {
            PursuitMember member = new memberType();
            members.Insert(0, member);
            return member as memberType;
        }

        public void Remove(PursuitMember member)
        {
            members.Remove(member);
        }

        public void Tick(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                tickCount++;

                members.Sort();

                FindAdjacentPursuers();
                RemovePursuerIfLast();

                LogTick();
            }

            void FindAdjacentPursuers()
            {
                for (int i = 0; i <= members.Count - 2; i++)
                {
                    PursuitMember member = members[i];

                    bool adjacentPursuers = member is Pursuer && members[i + 1] is Pursuer;
                    if (adjacentPursuers)
                    {
                        Remove(member);
                    }
                }
            }

            void RemovePursuerIfLast()
            {
                PursuitMember last = members[members.Count - 1];
                if (last is Pursuer)
                {
                    Remove(last);
                }
            }

            void LogTick()
            {
                LastTickLog.Clear();

                LastTickLog.Add($"<color=yellow>________ Tick {tickCount} ________</color>");

                foreach(PursuitMember m in members)
                {
                    LastTickLog.Add(m.ToString());
                }
            }
        }
    }
}
