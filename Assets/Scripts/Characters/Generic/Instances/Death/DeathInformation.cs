using System;

namespace Characters.Instances.Deaths
{
    public class DeathInformation
    {
        public IRespawnMethod respawnMethod;
        public int Priority { get => respawnMethod.Priority; }

        public string deathMessage;

        public DeathInformation(IRespawnMethod respawnMethod, string deathMessage ="not specified")
        {
            this.respawnMethod = respawnMethod;
            this.deathMessage = deathMessage;
        }
    }
}