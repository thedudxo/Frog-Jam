using System.Collections.Generic;

namespace Pursuits
{
    public class Pursuit
    {

        public List<PursuitMember> members = new List<PursuitMember>();


        public Pursuer incomingPursuer = null;
        public float entryPoint = 0;

        public Add add;
        public IMemberMovement movement;

        public Pursuit(IMemberMovement movement)
        {
            add = new Add(this);

            this.movement = movement;
            movement.pursuit = this;
        }

        public void Tick()
        {
            movement.Move();
            CheckIncomingPursuer();
            members.Sort();

            FindAdjacentPursuers();


            void FindAdjacentPursuers()
            {
                for (int i = 0; i <= members.Count - 1; i++)
                {
                    PursuitMember member = members[i];

                    bool adjacentPursuers = member is Pursuer && members[i + 1] is Pursuer;
                    if (adjacentPursuers)
                    {
                        // this pursuer should be removed
                    }
                }
            }

            void CheckIncomingPursuer()
            {
                if (incomingPursuer?.IsPast(entryPoint) == true)
                {
                    members.Insert(0, incomingPursuer);
                    incomingPursuer = null;
                }
            }
        }
    }
}
