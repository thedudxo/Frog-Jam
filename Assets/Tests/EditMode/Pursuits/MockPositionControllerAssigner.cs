using Pursuits;

namespace Tests.Pursuits
{
    class MockPositionControllerAssigner : IPositionControllerAssigner
    {
        public void AssignPositionControllerTo(PursuitMember pursuitMember)
        {
            pursuitMember.positionController = new MockPositionController();
        }
    }
}
