using UnityEngine;

namespace Characters.Instances.Deaths
{
    interface ISuicideInput
    {
        bool GetSuicideInput();
    }

    class PressKeyDeathCondition : IDeathCondition
    {
        IRespawnMethod respawnMethod;
        ISuicideInput suicideInput;

        public PressKeyDeathCondition(ISuicideInput suicideInput, IRespawnMethod respawnMethod)
        {
            this.respawnMethod = respawnMethod;
            this.suicideInput = suicideInput;
        }

        public DeathInformation Check()
        {
            if (suicideInput.GetSuicideInput())
            {
                DeathInformation death = new DeathInformation(respawnMethod, "Killed them self");
                return death;
            }

            else return null;
        }
    }
}
