using Frogs.Instances.Inputs;
using Characters.Instances.Inputs;

namespace Frogs.Instances.State
{
    class SuicideRespawnMethod : FrogRespawnMethod
    {
        readonly FrogSuicideInput input;
        public SuicideRespawnMethod(FrogStateContext context) : base(context) 
        {
            this.input = frog.controllers.input.suicide;
        }

        override public int Priority => 2;

        public override void Respawn()
        {
            InputEvent inputEvent = input.GetSuicideInput();
            if (inputEvent.Holding)
            {
                new RestartRespawnMethod(context).Respawn();
            }
            else
            {
                new SetbackRespawnMethod(context).Respawn();
            }
        }
    }
}
