﻿using System.Collections.Generic;
using UnityEngine;
using static FrogScripts.Life.DeathConditions;

namespace FrogScripts.Life
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] Frog frog;

        RespawnTimer respawnTimer;
        DeathConditions deathConditions;
        [SerializeField] LifeStateControlls stateControlls;
            
        void Start()
        {
            respawnTimer = new RespawnTimer();
            deathConditions = new DeathConditions(transform, frog.currentCollisions);
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