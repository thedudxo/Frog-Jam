using UnityEngine;

namespace Characters.Instances.Deaths
{
    class PressKeyDeathCondition : IDeathCondition
    {
        KeyCode key;
        IRespawnMethod respawnMethod;

        public PressKeyDeathCondition(KeyCode key, IRespawnMethod respawnMethod)
        {
            this.key = key;
            this.respawnMethod = respawnMethod;
        }

        public DeathInformation Check()
        {
            if (Input.GetKeyDown(key))
            {
                return new DeathInformation(respawnMethod, "Killed them self");
            }

            else return null;
        }
    }
}
