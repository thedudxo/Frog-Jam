using Pursuits;

namespace Tests.Pursuits
{
    class MovementMock : IMemberMovement
    {
        internal float movementAmmount = 1;

        public Pursuit pursuit { get; set; }

        public void Move()
        {
            pursuit.incomingPursuer.position += movementAmmount;

            foreach(PursuitMember m in pursuit.members)
            {
                m.position += movementAmmount;
            }
        }
    }
}
