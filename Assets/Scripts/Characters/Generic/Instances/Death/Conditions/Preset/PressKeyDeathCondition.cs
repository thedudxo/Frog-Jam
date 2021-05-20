using Characters.Instances.Inputs;
using Characters.Instances.States;

namespace Characters.Instances.Deaths
{

    public class PressKeyDeathCondition : IDeathCondition
    {
        IRespawnMethod respawnMethod;
        ISuicideInput suicideInput;
        ICharacterState allowedState;
        
        public bool Enabled { get; set; } = true;

        public PressKeyDeathCondition(ISuicideInput suicideInput, IRespawnMethod respawnMethod, ICharacterState allowedState)
        {
            this.respawnMethod = respawnMethod;
            this.suicideInput = suicideInput;
            this.allowedState = allowedState;
        }

        public DeathInformation Check()
        {
            if (!Enabled) return null;

            InputEvent input = suicideInput.GetSuicideInput();

            //allowed state prevents suiciding immeadately after restarting from the end level screen
            if (input.Holding && input.StartState == allowedState)
            {
                DeathInformation death = new DeathInformation(respawnMethod, "Killed them self");
                return death;
            }

            else return null;
        }
    }
}
