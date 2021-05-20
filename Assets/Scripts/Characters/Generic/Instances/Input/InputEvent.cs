using Characters.Instances.States;

namespace Characters.Instances.Inputs
{
    public struct InputEvent
    {
        public ICharacterState StartState { get; private set; }
        public bool Holding { get; private set; }

        public void SetHoldingTrue(ICharacterState state)
        {
            Holding = true;
            StartState = state;
        }

        public void SetHoldingFalse()
        {
            Holding = false;
        }
    }
}
