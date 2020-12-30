using System.Collections.Generic;
using UnityEngine;
using static Frog.Life.DeathConditions;

namespace Frog.Life
{
    public class LifeController
    {
        RespawnTimer respawnTimer;
        DeathConditions deathConditions;

        LifeStateControlls state;
            public void Restart() { state.Restart(); }

        public LifeController(Frog frog)
        {
            respawnTimer = new RespawnTimer();
            deathConditions = new DeathConditions(frog.transform, frog.currentCollisions);
            state = new LifeStateControlls(frog);
        }

        bool alive = true;
        const GM.GameState finishedLevel = GM.GameState.finishedLevel;
        DeathType deathType = DeathType.none;

        public void Update()
        {
            if (GM.gameState == finishedLevel) { return; }

            switch (alive)
            {
                case true:
                    deathType = deathConditions.Check();
                    if (deathType != DeathType.none)
                    {
                        state.Die();
                        alive = false;
                    }
                    break;

                case false:
                    if (respawnTimer.ShouldRespawnNow())
                    {
                        state.Respawn(deathType);
                        alive = true;
                    }
                    break;
            }
        }


    }

    
}
