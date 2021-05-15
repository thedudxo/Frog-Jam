using UnityEngine;

namespace Characters.Instances.Deaths
{
    public interface ISuicideInput
    {
        bool GetSuicideInput();
    }

    public class PressKeyDeathCondition : IDeathCondition
    {
        IRespawnMethod respawnMethod;
        ISuicideInput suicideInput;
        public bool Enabled { get; set; } = true;

        public PressKeyDeathCondition(ISuicideInput suicideInput, IRespawnMethod respawnMethod)
        {
            this.respawnMethod = respawnMethod;
            this.suicideInput = suicideInput;
        }

        public DeathInformation Check()
        {
            if (!Enabled) return null;
            if (suicideInput.GetSuicideInput())
            {
                DeathInformation death = new DeathInformation(respawnMethod, "Killed them self");
                return death;
            }

            else return null;
        }
    }
}
