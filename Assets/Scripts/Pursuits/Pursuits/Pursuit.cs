using System.Collections.Generic;

namespace Pursuits
{
    public class Pursuit
    {

        public List<PursuitMember> members = new List<PursuitMember>();
        Stack<PursuitMember> membersToRemove = new Stack<PursuitMember>();

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
            if (member == null) throw new System.ArgumentNullException("member");
            membersToRemove.Push(member);
        }

        public void Tick(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                tickCount++;

                members.Sort(); 

                CheckRules();
                RemoveQueuedMembers();
                AssignIndexes();

                LogTick();
            }

            void CheckRules()
            {
                int memberCount = members.Count;

                Pursuer lastPursuer = null;

                for (int index = 0; index <= memberCount - 1; index++)
                {
                    PursuitMember member = members[index];

                    if (member is Pursuer)
                    {
                        bool notLastMember = index != memberCount - 1;

                        if (notLastMember)
                        {
                            bool nextMemberIsPursuer = members[index + 1] is Pursuer;

                            if (nextMemberIsPursuer)
                            {
                                Remove(member);
                            }

                            else
                            {
                                lastPursuer = member as Pursuer;
                            }
                        }
                        else
                        {
                            Remove(member);
                        }
                    }

                    else
                    {
                        (member as Runner).pursuerBehind = lastPursuer;
                    }
                }
            }

            void RemoveQueuedMembers()
            {
                foreach (PursuitMember m in membersToRemove)
                {
                    m.ToNotifyOnMemberRemoved?.OnMemberRemoved();
                    members.Remove(m);
                }

                membersToRemove.Clear();
            }

            void AssignIndexes()
            {
                for (int i = 0; i <= members.Count - 1; i++)
                {
                    members[i].index = i;
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
