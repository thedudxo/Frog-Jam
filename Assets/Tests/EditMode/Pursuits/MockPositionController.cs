using Pursuits;

namespace Tests.Pursuits
{
    class MockPositionController : IpostitonController
    {
        internal float speed = 1;

        internal MockPositionController(float speed = 1)
        {
            this.speed = speed;
        }

        public PursuitMember member { get; set; }

        public void UpdatePosition()
        {
            member.position += speed;
        }
    }
}
