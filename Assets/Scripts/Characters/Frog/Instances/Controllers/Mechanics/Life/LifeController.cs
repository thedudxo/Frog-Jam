using System.Collections.Generic;
using UnityEngine;
using static Frogs.Life.DeathConditions;
using static Frogs.Life.DeathConditions.DeathType;

namespace Frogs.Life
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] Frog frog;

        public RespawnTimer respawnTimer = new RespawnTimer();
        DeathConditions deathConditions;
        [SerializeField] LifeStateControlls stateControlls;
            
        void Start()
        {
            deathConditions = new DeathConditions(frog, frog.currentCollisions);
        }

        bool alive = true;
        DeathType lastDeathType = none;


        public void Update()
        {
            switch (alive)
            {
                case true:
                    lastDeathType = deathConditions.Check();
                    if (lastDeathType != none)
                    {
                        stateControlls.Die();
                        alive = false;
                    }
                    break;

                case false:
                    if (respawnTimer.ShouldRespawnNow())
                    {
                        stateControlls.Respawn(lastDeathType);
                        alive = true;
                    }
                    break;
            }
        }

        public void Restart()
        {
            stateControlls.Restart();
        }
    }
}
