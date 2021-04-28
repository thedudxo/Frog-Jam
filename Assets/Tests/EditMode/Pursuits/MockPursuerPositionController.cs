using Pursuits;

namespace Tests.Pursuits
{
    class MockPursuerPositionController : IpostitonController
    {
        internal float speed = 1;

        public PursuitMember member { get; set; }

        public void UpdatePosition()
        {
            member.position += speed;
        }
    }
}
