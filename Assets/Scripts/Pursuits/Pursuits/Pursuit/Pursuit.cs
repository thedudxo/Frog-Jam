using System.Collections.Generic;

namespace Pursuits
{
    public class Pursuit
    {

        public List<PursuitMember> members = new List<PursuitMember>();


        public Pursuer incomingPursuer = null;
        public float entryPoint = 0;

        public Add add;
        public Remove remove;

        public List<string> LastTickLog { get; private set; } = new List<string>();
        int tickCount = 0;

        public Pursuit(IPositionControllerAssigner pursuerPosAssigner, IPositionControllerAssigner runnerPosAssigner)
        {
            add = new Add(this, pursuerPosAssigner, runnerPosAssigner);
            remove = new Remove(this);
        }

        public void Tick(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                tickCount++;

                MoveMembers();
                CheckIncomingPursuerHasArrived();
                members.Sort();

                FindAdjacentPursuers();
                RemovePursuerIfLast();

                LogTick();
            }

            void MoveMembers()
            {
                foreach(PursuitMember m in members)
                {
                    m.positionController.UpdatePosition();
                }
            }

            void FindAdjacentPursuers()
            {
                for (int i = 0; i <= members.Count - 2; i++)
                {
                    PursuitMember member = members[i];

                    bool adjacentPursuers = member is Pursuer && members[i + 1] is Pursuer;
                    if (adjacentPursuers)
                    {
                        remove.Pursuer(member as Pursuer);
                    }
                }
            }

            void RemovePursuerIfLast()
            {
                PursuitMember last = members[members.Count - 1];
                if (last is Pursuer)
                {
                    remove.Pursuer((Pursuer) last);
                }
            }

            void CheckIncomingPursuerHasArrived()
            {
                if (incomingPursuer?.IsPast(entryPoint) == true)
                {
                    members.Insert(0, incomingPursuer);
                    incomingPursuer = null;
                }
            }

            void LogTick()
            {
                LastTickLog.Clear();

                LastTickLog.Add($"<color=yellow>________ Tick {tickCount} ________</color>");
                LastTickLog.Add($"Incoming: {incomingPursuer?.ToString()}");

                foreach(PursuitMember m in members)
                {
                    LastTickLog.Add(m.ToString());
                }
            }
        }
    }
}
