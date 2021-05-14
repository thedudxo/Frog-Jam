using UnityEngine;

namespace Characters.Instances.Deaths
{
    public class BelowYDeathCondition : IDeathCondition
    {
        GameObject gameObject;
        float y;
        IRespawnMethod respawnMethod;
        public bool Enabled { get; set; } = true;

        public BelowYDeathCondition(GameObject gameObject, float y, IRespawnMethod respawnMethod)
        {
            this.gameObject = gameObject;
            this.y = y;
            this.respawnMethod = respawnMethod;
        }

        public DeathInformation Check()
        {
            if (!Enabled) return null;
            if (gameObject.transform.position.y < y)
            {
                return new DeathInformation(respawnMethod, "Fell in the water");
            }

            else return null;
        }
    }
}
