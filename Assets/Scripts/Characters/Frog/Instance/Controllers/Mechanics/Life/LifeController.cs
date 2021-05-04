using System.Collections.Generic;
using UnityEngine;
using static Frogs.Life.DeathConditions;

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
        DeathType deathType = DeathType.none;


        public void Update()
        {
            switch (alive)
            {
                case true:
                    deathType = deathConditions.Check();
                    if (deathType != DeathType.none)
                    {
                        stateControlls.Die();
                        alive = false;
                    }
                    break;

                case false:
                    if (respawnTimer.ShouldRespawnNow())
                    {
                        stateControlls.Respawn(deathType);
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
